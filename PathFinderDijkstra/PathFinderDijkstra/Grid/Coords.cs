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
    /// Coordinates.
    /// </summary>
    [Serializable]
    public class Coords
    {
        public int x { get; set; }
        public int y { get; set; }
        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Coords(Coords coords)
        {
            this.x = coords.x;
            this.y = coords.y;
        }

    }
}
