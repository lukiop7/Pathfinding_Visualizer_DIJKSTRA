using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PathFinderDijkstra.Grid;

namespace PathFinderDijkstra.Algorithm
{
    class Dijkstra
    {
        private Grid.Grid grid { get; set; } 
        private List<Node> allNodes { get; set; }
        private List<Node> openNodes { get; set; }
        private List<Node> closedNodes { get; set; }
        private List<Node> neighbours { get; set; }

        private const int HorizontalCells = 40;
        private const int VerticalCells = 20;
        private Node startCell;
        private Node endCell;
        private Node currentNode;
        private bool done = false;


      public  Dijkstra(Grid.Grid grid)
        {
            openNodes = new List<Node>();
            closedNodes = new List<Node>();
            allNodes = new List<Node>();
            neighbours = new List<Node>();
            this.grid = grid;
            for (var x = 0; x < HorizontalCells; x++)
            {
                for (var y = 0; y < VerticalCells; y++)
                {
                    var cell = grid.GetCell(x, y);
                    var node = new Node(cell);
                    if (cell.type == CellType.A)
                        startCell = node;
                    else if (cell.type == CellType.B)
                        endCell = node;


                    openNodes.Add(node);
                    allNodes.Add(node);
                }
            }
            currentNode = startCell;
        }

        private IEnumerable<Node> GetNeighbours(Node current)
        {
            var neighbours = new List<Cell>
            {
                grid.GetCell(current.cell.coords.x - 1, current.cell.coords.y),
                grid.GetCell(current.cell.coords.x + 1, current.cell.coords.y),
                grid.GetCell(current.cell.coords.x, current.cell.coords.y - 1),
                grid.GetCell(current.cell.coords.x, current.cell.coords.y + 1)
            };

            var n =  neighbours.Where(x => x.type != CellType.Invalid && x.type != CellType.Solid).ToArray();
            return allNodes.Where(x => n.Any(c => c.coords.x == x.cell.coords.x && c.coords.y == x.cell.coords.y && !x.visited)).ToList();
        }
        public void first()
        {
            if (currentNode == startCell)
            {
                currentNode.distance = 0;
                currentNode.visited = true;
               // currentNode.cell.type = CellType.Current;
                var neighbours = GetNeighbours(currentNode);
                foreach (var n in neighbours)
                {
                    var dist = currentNode.distance + 1;
                    if (dist < n.distance)
                    {
                        n.distance = dist;
                        n.previousCell = currentNode;
                    }
                }
            }
        }
        public void Run()
        {
            if (!done) 
            {
                if(currentNode!=startCell)
                currentNode.cell.type = CellType.Visited;
                currentNode = allNodes.Where(x => !x.visited).OrderBy(x => x.distance).FirstOrDefault();
            }
            if (currentNode != endCell && !done)
            {
                    currentNode.visited = true;
                    currentNode.cell.type = CellType.Current;
                    var neig = GetNeighbours(currentNode);
                    foreach (var n in neig)
                    {
                        var dist = currentNode.distance + 1;
                        if (dist < n.distance)
                        {
                            n.distance = dist;
                            n.previousCell = currentNode;
                        }
                    }                
            }
            else
            {
                done = true;
               if (currentNode != startCell)
                {
                    currentNode.cell.type = CellType.Path;
                    currentNode = currentNode.previousCell;
                    Thread.Sleep(50);
                    return;
                }
                currentNode.cell.type = CellType.Path;
            }
        }
        public void firstStep()
        {
            currentNode = startCell;
            startCell.distance = 0;
            startCell.visited = true;
            startCell.cell.type = CellType.Current;
            openNodes.Remove(startCell);
            closedNodes.Add(startCell);
            var neighbours = GetNeighbours(currentNode);
            foreach (var n in neighbours)
            {
                n.distance = currentNode.distance + 1;
            }
        }
        public bool calculateStep()
        {
            currentNode = openNodes.OrderBy(x => x.distance).FirstOrDefault();
            if (currentNode != null)
            {
                currentNode.visited = true;
                currentNode.cell.type = CellType.Current;
                openNodes.Remove(startCell);
                closedNodes.Add(startCell);
                var neighbours = GetNeighbours(currentNode);
                foreach (var n in neighbours)
                {
                    n.distance = currentNode.distance + 1;
                }

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
