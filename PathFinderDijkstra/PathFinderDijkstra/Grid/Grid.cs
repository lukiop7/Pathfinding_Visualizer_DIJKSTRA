using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderDijkstra.Grid
{
    public class Grid
    {
        private readonly Cell[,] _grid;


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

           // SetStartAndEnd();
        }

        public void Randomize()
        {
            Randomize((int) DateTime.Now.Ticks);
        }

        public void Randomize(int seed)
        {
            var rand = new Random(seed);

            // Iterate through the whole grid
            for (var x = 0; x < _grid.GetLength(0); x++)
            {
                for (var y = 0; y < _grid.GetLength(1); y++)
                {
                    // Make each cell either solid or empty at random
                    _grid[x, y].type = rand.Next(0, 10) > 5 ? CellType.Solid : CellType.Empty;
                    if (_grid[x, y].type != CellType.Empty) continue;

                    // If it's empty, randomly give the path a weight
                    var weightSpread = rand.Next(0, 10);
                    if (weightSpread > 8)
                        _grid[x, y].weight = 3;
                    else if (weightSpread > 6)
                        _grid[x, y].weight = 2;
                    else
                        _grid[x, y].weight = 1;
                }
            }

            SetStartAndEnd();
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

        public Cell GetStart()
        {
            return _grid.Cast<Cell>().FirstOrDefault(cell => cell.type == CellType.A);
        }

        public Cell GetEnd()
        {
            return _grid.Cast<Cell>().FirstOrDefault(cell => cell.type == CellType.B);
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
                    type = type,
                    weight = GetCell(x, y)?.weight ?? 0
                };
            }

            // SetStartAndEnd();
        }


        public int GetCountOfType(CellType type)
        {
            var total = 0;
            foreach (var cell in _grid)
            {
                total += cell.type == type ? 1 : 0;
            }

            return total;
        }

        public int GetTraversableCells()
        {
            return GetCountOfType(CellType.Unvisited) + GetCountOfType(CellType.A) + GetCountOfType(CellType.B);
        }

        private void SetStartAndEnd()
        {
            _grid[0, 0] = new Cell
            {
                coords = new Coords(0,0),
                type = CellType.A
            };
            _grid[_grid.GetLength(0) - 1, _grid.GetLength(1) - 1] = new Cell
            {
                coords= new Coords(_grid.GetLength(0) - 1, _grid.GetLength(1) - 1),
                type = CellType.B
            };
        }
    }
}