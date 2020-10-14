using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderDijkstra.Grid
{
    public enum CellType
        {
            Invalid = -1,
            Empty = 0,
            Solid = 1,
            A = 3,
            B = 4,
            Path = 5,
            Unvisited = 6,
            Visited = 7,
            Current = 8
        }
}
