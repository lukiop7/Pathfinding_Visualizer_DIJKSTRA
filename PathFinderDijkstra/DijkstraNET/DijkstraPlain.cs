
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraNET
{
    public static class DijkstraPlain
    {
        private static int MinimalDistance(int[] distances, bool[] visits)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < 800; ++v)
            {
                if (visits[v] == false )
                {
                    if(distances[v] <= min)
                    {
                        min = distances[v];
                        minIndex = v;
                    }
                }
            }

            return minIndex;
        }

        public static int[] GetNeighbours(bool[] visits, int current)
        {
            int[] cells = new int[4];
            if (current % 40 == 0)
                cells[0] = -1;
            else
                cells[0] = current - 1;
            if (current % 40 == 39)
                cells[1] = -1;
            else
                cells[1] = current + 1;
            if (current + 40 >= 800)
                cells[2] = -1;
            else
                cells[2] = current + 40;
            if (current - 40 < 0)
                cells[3] = -1;
            else
                cells[3] = current - 40;
            for (int i = 0; i < 4; i++)
            {
                if (cells[i] != -1)
                {
                   if( visits[cells[i]])
                    cells[i] = -1;
                }
            }
            return cells;
        }

        public static void First(int source)
        {
            // to bedzie w gui robione, tam ustawie cale tabele i wszystko
            int[] distances = new int[800];
            int[] previous = new int[800];
            bool[] visits = new bool[800];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
                // jesli bedzie sciana to od razu zaznaczac jako visited zeby nie bylo problemu
                visits[i] = false;
                previous[i] = -1;
            }
            distances[source] = 0;

        }

        public static int Run(int[] distances, bool[] visits, int[] previous, int source, int destination, int current)
        {
            current = MinimalDistance(distances, visits);
            if (current != destination)
            {
                visits[current] = true;
                int[] neighbours = GetNeighbours(visits, current);
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
            }
            return current;
        }

        public static int fullAlgorithm(int[] distances, bool[] visits, int[] previous, int source, int destination, int current)
        {
            while (current != destination)
            {
                current = MinimalDistance(distances, visits);
                if (current != destination)
                {
                    visits[current] = true;
                    int[] neighbours = GetNeighbours(visits, current);
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
                }
            }
            return current;
        }
    }
}
