using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PathFinderDijkstra.GridDrawer;

namespace PathFinderDijkstra
{
    public partial class Form1 : Form
    {
        private Graphics Graphics;
        private GridDrawer.GridDrawer gridDrawer;
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeGrid()
        {
            gridDrawer = new GridDrawer.GridDrawer(mainGrid);
            gridDrawer.Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeGrid();
        }

        private void mainGrid_MouseClick(object sender, MouseEventArgs e)
        {
            gridDrawer.gridClicked(e.X,e.Y);
        }
    }
}
