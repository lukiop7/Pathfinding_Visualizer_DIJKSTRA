using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DijkstraNET;
using PathFinderDijkstra.Algorithm;
using PathFinderDijkstra.Grid;
using PathFinderDijkstra.GridDrawer;

namespace PathFinderDijkstra
{
    public partial class Form1 : Form
    {
        private GridDrawer.GridDrawer gridDrawer;
        private CellType clickType = CellType.Empty;

        public unsafe class NETProxy
        {
            [DllImport("Asm.dll")]
            public static extern int sumArray(int* distances, bool* visits, int* previous, int source, int destination, int len);


            public int callsumArray(int[] distances, bool[] visits, int[] previous, int source, int destination, int len)
            {
                unsafe
                {
                    fixed (int* dist = distances)
                    {
                        fixed(bool* visited = visits)
                        {
                            fixed(int* prev = previous)
                            {
                                return sumArray(dist, visited, prev,source,destination,len);
                            }
                        }
                      
                    }
                }

            }

        }
        public Form1()
        {
            InitializeComponent();
            InitializeGrid();

        }

        private void InitializeGrid()
        {
            gridDrawer = new GridDrawer.GridDrawer(mainGrid);
            gridDrawer.Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridDrawer.Reset();
        }

        private void mainGrid_MouseClick(object sender, MouseEventArgs e)
        {
            gridDrawer.gridClicked(e.X,e.Y,clickType);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            clickType = CellType.A;
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            clickType = CellType.B;
        }

        private void wallButton_Click(object sender, EventArgs e)
        {
            clickType = CellType.Solid;
        }

        private void eraseButton_Click(object sender, EventArgs e)
        {
            //clickType = CellType.Empty;
            NETProxy asm = new NETProxy();
            int[] distances = new int[3] { 1, 2, 3 };
            int[] previous = new int[3] { 1, 2, 3 };
            bool[] visited = new bool[3] {true, true, false};
            int source = 4;
            int destination = 9;
            int len = 3;
            eraseButton.Text = asm.callsumArray(distances, visited, previous, source, destination, len).ToString();
        }

        private void runAlgoButton_Click(object sender, EventArgs e)
        {
            if (gridDrawer.startCell.type != CellType.A)
                gridDrawer.ClearSolution();
            performAlgo();
        }
        private void performAlgo()
        {
            int source = gridDrawer.GetIndex(gridDrawer.startCell);
            int destination = gridDrawer.GetIndex(gridDrawer.endCell);
            int current = source;

            int[] distances = new int[800];
            int[] previous = new int[800];
            bool[] visits = new bool[800];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
                // jesli bedzie sciana to od razu zaznaczac jako visited zeby nie bylo problemu
                if (gridDrawer.GetCell(i).type == CellType.Solid)
                    visits[i] = true;
                else
                    visits[i] = false;
                previous[i] = -1;
            }
            distances[source] = 0;
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
            while (current != -1)
            {
                var cell = gridDrawer.GetCell(current);
                cell.type = CellType.Path;
                current = previous[current];
                gridDrawer.Draw();
            }
        }
    }
}
