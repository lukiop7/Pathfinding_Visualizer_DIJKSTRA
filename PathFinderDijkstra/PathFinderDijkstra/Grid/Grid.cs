// Finding the shortest path using Dijkstra algorithm

// Algorithm finds the shortest path in a maze

// 3.11.2020
// Winter Semester, 2020/2021
// Lukasz Kwiecien Informatics

using System;

namespace PathFinderDijkstra.Grid
{
    /// <summary>
    /// Grid clas..
    /// </summary>
    [Serializable]
    public class Grid
    {
        public Cell[,] _grid;

        public Grid(int horizontalCells, int verticalCells)
        {
            _grid = new Cell[horizontalCells, verticalCells];
            for (var x = 0; x < _grid.GetLength(0); x++)
            {
                for (var y = 0; y < _grid.GetLength(1); y++)
                {
                    SetCell(x, y, CellType.Empty);
                }
            }
        }

        public void ResetGrid()
        {
            for (var x = 0; x < _grid.GetLength(0); x++)
            {
                for (var y = 0; y < _grid.GetLength(1); y++)
                {
                    SetCell(x, y, CellType.Empty);
                }
            }
        }
    
        public Cell GetCell(int x, int y)
        {
            if (x > _grid.GetLength(0) - 1 || x < 0 || y > _grid.GetLength(1) - 1 || y < 0)
                return new Cell {coords=new Coords(x,y), type = CellType.Invalid};

            return _grid[x, y];
        }

        public void SetCell(int x, int y, CellType type)
        {
            if (_grid[x, y] != null)
            {
                _grid[x, y].type = type;
            }
            else
            {
                _grid[x, y] = new Cell
                {
                    coords=new Coords(x,y),
                    type = type
                };
            }
        }
    }
}