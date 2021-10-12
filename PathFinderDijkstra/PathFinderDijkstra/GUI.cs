// Finding the shortest path using Dijkstra algorithm

// Algorithm finds the shortest path in a maze

// 3.11.2020
// Winter Semester, 2020/2021
// Lukasz Kwiecien Informatics

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DijkstraNET;
using Newtonsoft.Json;
using PathFinderDijkstra.Grid;


namespace PathFinderDijkstra
{
    public partial class GUI : Form
    {
        /// <summary>
        /// Draws on the grid.
        /// </summary>
        private GridDrawer.GridDrawer gridDrawer;
        /// <summary>
        /// Used for drawing on the grid.
        /// </summary>
        private CellType clickType = CellType.Empty;
        /// <summary>
        /// Measures the execution time.
        /// </summary>
        private Stopwatch timer;
        /// <summary>
        /// Allows to clear the solution.
        /// </summary>
        private PathFinderDijkstra.Grid.Grid inputGrid;
        /// <summary>
        /// Is solved?
        /// </summary>
        private bool solved = false;

        /// <summary>
        /// Proxy class for importing the ASM dll
        /// </summary>
        public unsafe class ASMProxy
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

        /// <summary>
        /// Constructor.
        /// </summary>
        public GUI()
        {
            InitializeComponent();
            InitializeGrid();
            languageComboBox.DataSource = Enum.GetValues(typeof(dllEnum));
            timer = new Stopwatch();
        }

        /// <summary>
        /// Initializes grid.
        /// </summary>
        private void InitializeGrid()
        {
            gridDrawer = new GridDrawer.GridDrawer(mainGrid);
            gridDrawer.Draw();
        }

        /// <summary>
        /// Handles clearButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            gridDrawer.Reset();
            inputGrid = null;
        }

        /// <summary>
        /// Handles mainGrid click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (solved)
            {
                SetInputGrid();
            }
            gridDrawer.gridClicked(e.X, e.Y, clickType);
        }

        /// <summary>
        /// Handles startButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            SetInputGrid();

            clickType = CellType.A;
        }

        /// <summary>
        /// Handles endButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endButton_Click(object sender, EventArgs e)
        {
            SetInputGrid();
            clickType = CellType.B;
        }

        /// <summary>
        /// Handles wallButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wallButton_Click(object sender, EventArgs e)
        {
            SetInputGrid();
            clickType = CellType.Solid;
        }

        /// <summary>
        /// Handles eraseButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eraseButton_Click(object sender, EventArgs e)
        {
            SetInputGrid();
            clickType = CellType.Empty;
        }

        /// <summary>
        /// Handles runAlgoButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Calls the algorithm function from ASM.dll and draws the solution.
        /// </summary>
        private void asmRun()
        {
            ASMProxy asm = new ASMProxy();
            int len = 800;
            int source = gridDrawer.GetIndex(gridDrawer.startCell);
            int destination = gridDrawer.GetIndex(gridDrawer.endCell);
            int current = source;

            int[] distances = new int[800];
            int[] previous = new int[800];
            int[] visited = new int[800];

            for (int i = 0; i < distances.Length; i++)
            {
                // if current cell is a wall => mark is as visited
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

        /// <summary>
        /// Calls the algorithm function from DijkstraNET.dll and draws the solution.
        /// </summary>
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

            current = DijkstraPlain.fullAlgorithm(distances, visits, previous, source, destination, current);
            timer.Stop();
            netTimeLabel.Text = timer.ElapsedTicks.ToString();

            draw_solution(previous, current, source);
        }

        /// <summary>
        /// Draws the solution on the grid
        /// </summary>
        /// <param name="previous">array with predecessors</param>
        /// <param name="current">index of the current cell</param>
        /// <param name="source">index of the source</param>
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

        /// <summary>
        /// Resets the input grid.
        /// </summary>
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
                inputGrid = null;
                gridDrawer.Draw();
            }
        }

        /// <summary>
        /// Handles loadDataButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadDataBtn_Click(object sender, EventArgs e)
        {
            clearButton_Click(null,null);
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                   string  filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string fileContent = reader.ReadToEnd();

                        int[,] values = JsonConvert.DeserializeObject<int[,]>(fileContent);

                        var rows = values.GetLength(0);
                        var cols = values.GetLength(1);

                        if(rows>20 || cols >40)
                            {
                                MessageBox.Show("Maze in the file is bigger than 40x20", "Warning wrong input file");
                                return;
                            }
                        for (var y = 0; y < cols; y++)
                        {
                            for (var x = 0; x < rows; x++)
                            {
                                gridDrawer.Grid.SetCell(y, x, (CellType)values[x, y]);
                                if (values[x, y] == 3)
                                    gridDrawer.startCell = gridDrawer.Grid.GetCell(y, x);
                                else if (values[x, y] == 4)
                                    gridDrawer.endCell = gridDrawer.Grid.GetCell(y, x);
                            }
                        }
                        gridDrawer.Draw();
                        inputGrid = null;
                    }
                }
            }
        }

        /// <summary>
        /// Handles saveDataButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveDataBtn_Click(object sender, EventArgs e)
        {

            Cell[,] grid = gridDrawer.Grid._grid;

            var width = grid.GetLength(0);
            var height = grid.GetLength(1);

            int[,] exportArray = new int[height, width];

            for (var y = 0; y < height; y++)
            {

                for (var x = 0; x < width; x++)
                {
                    exportArray[y, x] = (int)grid[x, y].type;

                }
            }

            string json = JsonConvert.SerializeObject(exportArray, Formatting.Indented);

            Console.WriteLine($"JSON: \n");
            Console.WriteLine($"{json}");


            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "Maze.txt";
            save.Filter = "Text File | *.txt";

            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.OpenFile());

                writer.WriteLine(json);
                writer.Dispose();
                writer.Close();

            }
        }

        /// <summary>
        /// Handles clearSolutionButton click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearSolutionButton_Click(object sender, EventArgs e)
        {
            if (inputGrid != null)
            {
                gridDrawer.Grid = inputGrid.DeepClone();
                gridDrawer.Draw();
            }
        }
    }
}
