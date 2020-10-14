using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderDijkstra.Grid
{
    public class Cell
    {
        public Coords coords { get; set; }
        public CellType type { get; set; }
        public int weight { get; set; }
    }

    
}
