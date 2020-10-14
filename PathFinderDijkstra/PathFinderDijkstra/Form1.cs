using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PathFinderDijkstra.Algorithm;
using PathFinderDijkstra.Grid;
using PathFinderDijkstra.GridDrawer;

namespace PathFinderDijkstra
{
    public partial class Form1 : Form
    {
        private GridDrawer.GridDrawer gridDrawer;
        private CellType clickType = CellType.Empty;
        private Dijkstra dijkstra;
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
            clickType = CellType.Empty;
        }

        private void runAlgoButton_Click(object sender, EventArgs e)
        {
            performAlgo();
        }
        private void performAlgo()
        {
            dijkstra = new Dijkstra(gridDrawer.Grid);
            dijkstra.first();
            gridDrawer.Draw();
            while (gridDrawer.startCell.type != CellType.Path)
            {
                dijkstra.Run();
                gridDrawer.Draw();
            }
        }
    }
}
