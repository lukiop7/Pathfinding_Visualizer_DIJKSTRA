using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DijkstraNET;
using PathFinderDijkstra.Grid;
using PathFinderDijkstra.GridDrawer;

namespace PathFinderDijkstra
{
    public partial class GUI : Form
    {
        private GridDrawer.GridDrawer gridDrawer;
        private CellType clickType = CellType.Empty;
        private Stopwatch timer;
        private PathFinderDijkstra.Grid.Grid inputGrid;
        private bool solved = false;

        public unsafe class NETProxy
        {
            [DllImport("Asm.dll")]
            public static extern void dijkstraASM(int* distances, int* visits, int* previous, int source, int destination, int len);


            public void calldijkstraASM(int[] distances, int[] visits, int[] previous, int source, int destination, int len)
            {
                unsafe
                {
                    fixed (int* prev = previous, visited = visits, dist = distances)
                    {
                        dijkstraASM(dist, visited, prev, source, destination, len);
                    }
                }

            }

        }
        public GUI()
        {
            InitializeComponent();
            InitializeGrid();
            languageComboBox.DataSource = Enum.GetValues(typeof(dllEnum));
            timer = new Stopwatch();
        }

        private void InitializeGrid()
        {
            gridDrawer = new GridDrawer.GridDrawer(mainGrid);
            gridDrawer.Draw();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            gridDrawer.Reset();
            inputGrid = null;
        }

        private void mainGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (solved)
            {
                SetInputGrid();
            }
            gridDrawer.gridClicked(e.X, e.Y, clickType);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            SetInputGrid();

            clickType = CellType.A;
            inputGrid = null;
        }


        private void endButton_Click(object sender, EventArgs e)
        {
            SetInputGrid();
            clickType = CellType.B;
            inputGrid = null;
        }

        private void wallButton_Click(object sender, EventArgs e)
        {
            SetInputGrid();
            clickType = CellType.Solid;
            inputGrid = null;
        }

        private void eraseButton_Click(object sender, EventArgs e)
        {
            SetInputGrid();
            clickType = CellType.Empty;
            inputGrid = null;
        }

        private void runAlgoButton_Click(object sender, EventArgs e)
        {
            if (gridDrawer.startCell == null || gridDrawer.endCell == null)
            {
                MessageBox.Show("Set start and end before running the algorithm!", "Warning no start and end");
                return;
            }
            else
            {
                if (inputGrid != null)
                {
                    gridDrawer.Grid = inputGrid.DeepClone();
                    gridDrawer.Draw();
                }
                else
                {
                    inputGrid = gridDrawer.Grid.DeepClone();
                }
                if ((dllEnum)languageComboBox.SelectedItem == dllEnum.dotNET)
                    dotNETRun();
                else
                    asmRun();
            }
        }

        private void asmRun()
        {
            NETProxy asm = new NETProxy();
            int len = 800;
            int source = gridDrawer.GetIndex(gridDrawer.startCell);
            int destination = gridDrawer.GetIndex(gridDrawer.endCell);
            int current = source;

            int[] distances = new int[800];
            int[] previous = new int[800];
            int[] visited = new int[800];
            for (int i = 0; i < distances.Length; i++)
            {
                if (gridDrawer.GetCell(i).type == CellType.Solid)
                    visited[i] = 1;
                else
                    visited[i] = 0;
            }

            timer.Reset();
            timer.Start();
            asm.calldijkstraASM(distances, visited, previous, source, destination, len);
            timer.Stop();

            asmTimeLabel.Text = timer.ElapsedTicks.ToString();
            current = destination;
            draw_solution(previous, current, source);

        }


        private void dotNETRun()
        {
            int source = gridDrawer.GetIndex(gridDrawer.startCell);
            int destination = gridDrawer.GetIndex(gridDrawer.endCell);
            int current = source;

            int[] distances = new int[800];
            int[] previous = new int[800];
            bool[] visits = new bool[800];
            timer.Reset();
            timer.Start();
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
                previous[i] = -1;
                // if current cell is a wall => mark is as visited
                if (gridDrawer.GetCell(i).type == CellType.Solid)
                    visits[i] = true;
                else
                    visits[i] = false;
            }
            distances[source] = 0;
            visits[source] = true;
            int[] neighbours = DijkstraPlain.GetNeighbours(visits, current);
            for (int i = 0; i < 4; i++)
            {
                int index = neighbours[i];
                if (index != -1)
                {
                    int dist = distances[current] + 1;
                    if (dist < distances[index])
                    {
                        distances[index] = dist;
                        previous[index] = current;
                    }
                }
            }
            Cell previousCell = gridDrawer.startCell;

            current = DijkstraPlain.fullAlgorithm(distances, visits, previous, source, destination, current);
            timer.Stop();
            netTimeLabel.Text = timer.ElapsedTicks.ToString();
            //while (current != destination)
            //{
            //    var cell =gridDrawer.GetCell(current);
            //    if (cell != gridDrawer.endCell)
            //    {
            //        previousCell.type = CellType.Visited;
            //        cell.type = CellType.Current;
            //        current = DijkstraPlain.Run(distances, visits, previous, source, destination, current);
            //        previousCell = cell;
            //    }
            //    gridDrawer.Draw();
            //}
            draw_solution(previous, current, source);
        }

        private void draw_solution(int[] previous, int current, int source)
        {
            var cell = gridDrawer.GetCell(current);
            cell.type = CellType.Visited;
            current = previous[current];
            gridDrawer.Draw();

            while (current != source)
            {
                cell = gridDrawer.GetCell(current);
                cell.type = CellType.Path;
                current = previous[current];

                gridDrawer.Draw();
            }

            cell = gridDrawer.GetCell(current);
            cell.type = CellType.Visited;
            gridDrawer.Draw();

            solved = true;
        }


        private void SetInputGrid()
        {
            if (inputGrid != null)
            {
                gridDrawer.Grid = inputGrid.DeepClone();
                if (gridDrawer.startCell != null)
                {
                    gridDrawer.startCell = gridDrawer.Grid.GetCell(gridDrawer.startCell.coords.x, gridDrawer.startCell.coords.y);
                }
                if (gridDrawer.endCell != null)
                {
                    gridDrawer.endCell = gridDrawer.Grid.GetCell(gridDrawer.endCell.coords.x, gridDrawer.endCell.coords.y);
                }
                solved = false;
                gridDrawer.Draw();
            }
        }
    }
}
