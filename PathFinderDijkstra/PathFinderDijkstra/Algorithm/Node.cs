using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinderDijkstra.Grid;

namespace PathFinderDijkstra.Algorithm
{
    class Node
    {
        public Cell cell { get; set; }
        public Node? previousCell { get; set; }
        public int distance { get; set; }
        public bool visited { get; set; }

        public Node(Cell cell)
        {
            this.cell = cell;
            previousCell = null;
            distance = Int32.MaxValue;
            visited = false;
        }
    }
}
