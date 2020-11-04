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

namespace DijkstraNET
{
    public static class DijkstraPlain
    {
        /// <summary>
        /// Finds the element with the smallest distance and returns its index.
        /// </summary>
        /// <param name="distances">Array with distances</param>
        /// <param name="visits">Array with informations if the cells were visited</param>
        /// <returns>Index of the element with smallest distance</returns>
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

        /// <summary>
        /// Calculate the indexes of the neighbours of the current element and returns array containing them.
        /// </summary>
        /// <param name="visits">Array with informations if the cells were visited</param>
        /// <param name="current">Index of the current element</param>
        /// <returns>Array of neighbours</returns>
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

        /// <summary>
        /// Performs the Dijkstra algorithm. Fills the arrays with values calculated during the execution of the algorithm.
        /// </summary>
        /// <param name="distances">Array with distances</param>
        /// <param name="visits">Array with informations if the cells were visited</param>
        /// <param name="previous">Array containing informations about the previous cell on the way to the current cell</param>
        /// <param name="source">Start cell index</param>
        /// <param name="destination">End cell index</param>
        /// <param name="current">Current cell index</param>
        /// <returns>Index of the destination cell</returns>
        public static int fullAlgorithm(int[] distances, bool[] visits, int[] previous, int source, int destination, int current)
        {
            distances[source] = 0;
            visits[source] = true;
            int[] neighbours_first = DijkstraPlain.GetNeighbours(visits, current);
            for (int i = 0; i < 4; i++)
            {
                int index = neighbours_first[i];
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
