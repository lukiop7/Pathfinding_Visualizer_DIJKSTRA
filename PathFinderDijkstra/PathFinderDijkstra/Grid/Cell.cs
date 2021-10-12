// Finding the shortest path using Dijkstra algorithm

// Algorithm finds the shortest path in a maze

// 3.11.2020
// Winter Semester, 2020/2021
// Lukasz Kwiecien Informatics

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderDijkstra.Grid
{
    /// <summary>
    /// Cell.
    /// </summary>
    [Serializable]
    public class Cell
    {
        public Coords coords { get; set; }
        public CellType type { get; set; }
    }

    
}
