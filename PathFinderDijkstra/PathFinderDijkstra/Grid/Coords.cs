using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderDijkstra.Grid
{
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
