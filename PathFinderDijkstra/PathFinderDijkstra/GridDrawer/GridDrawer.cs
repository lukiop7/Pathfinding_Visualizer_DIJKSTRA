// Finding the shortest path using Dijkstra algorithm

// Algorithm finds the shortest path in a maze

// 3.11.2020
// Winter Semester, 2020/2021
// Lukasz Kwiecien Informatics

namespace PathFinderDijkstra.GridDrawer
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Grid;

    /// <summary>
    /// Class responsible for drawing on the grid.
    /// </summary>
    public class GridDrawer
    {
        private readonly PictureBox _pb;
        public Grid Grid { get; set; }
        public Cell? startCell { get; set; }
        public Cell? endCell { get; set; }

        public int HorizontalCells = 40;
        public int VerticalCells = 20;

        private int _cellWidth;
        private int _cellHeight;

        public GridDrawer(PictureBox pb)
        {
            _pb = pb;
            Grid = new Grid(HorizontalCells, VerticalCells);
        }

        public void Reset()
        {
            Grid.ResetGrid();
            startCell = null;
            endCell = null;
            Draw();
        }

        public void Draw()
        {
            _cellWidth = _pb.Width / HorizontalCells;
            _cellHeight = _pb.Height / VerticalCells;

            if(_pb.Image!=null)
                _pb.Image.Dispose();

            var image = new Bitmap(_pb.Width, _pb.Height);

            using (var g = Graphics.FromImage(image))
            {
                var background = new Rectangle(0, 0, image.Width, image.Height);
                g.FillRectangle(new SolidBrush(Color.White), background);

                for (var x = 0; x < HorizontalCells; x++)
                {
                    for (var y = 0; y < VerticalCells; y++)
                    {
                        var cell = Grid.GetCell(x, y);
                        switch (cell.type)
                        {
                            case CellType.Empty:
                                break;
                            case CellType.Solid:
                                g.FillRectangle(Brushes.Black, GetRectangle(x, y));
                                break;
                            case CellType.Path:
                                g.FillRectangle(Brushes.Purple, GetRectangle(x, y));
                                break;
                            case CellType.Visited:
                                g.FillRectangle(Brushes.LightSeaGreen, GetRectangle(x, y));
                                break;
                            case CellType.A:
                                g.DrawString("A", GetFont(), Brushes.Red, GetPoint(x, y));
                                break;
                            case CellType.B:
                                g.DrawString("B", GetFont(), Brushes.Blue, GetPoint(x, y));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("Unknown cell type: " + cell);
                        }

                        g.DrawRectangle(Pens.Black, GetRectangle(x, y));
                    }
                }

                _pb.Image = image;
                _pb.Update();
            }
        }

        public void gridClicked(int x, int y,CellType clickType)
        {
            int xCell = x / _cellWidth;
            int yCell = y / _cellHeight;
            var cell = Grid.GetCell(xCell, yCell);
            cell.type = clickType;
            if (clickType == CellType.A)
            {
                if (startCell != null)
                {
                    if (cell != startCell)
                    {
                        startCell.type = CellType.Empty;
                        startCell = null;
                    }
                }
                startCell = cell;
            }
            else if (clickType == CellType.B)
            {
                if (endCell != null)
                {
                    if (cell != endCell)
                    {
                        endCell.type = CellType.Empty;
                        endCell = null;
                    }
                }
                endCell = cell;
            }
            Draw();
        }
        private Rectangle GetRectangle(int x, int y)
        {
            return new Rectangle(x * _cellWidth, y * _cellHeight, _cellWidth, _cellHeight);
        }

        private PointF GetPoint(int x, int y)
        {
            return new PointF(x * _cellWidth, y * _cellHeight);
        }

        private Font GetFont()
        {
            return new Font(FontFamily.GenericMonospace, Math.Min(_cellWidth, _cellHeight) / 1.3f, FontStyle.Bold);
        }

        public Cell GetCell(int index)
        {
            int x, y;
            y = index / 40;
            x = index % 40;
            return Grid.GetCell(x, y);
        }
        public int GetIndex(Cell cell)
        {
            return (cell.coords.y * 40 + cell.coords.x);
        }

        public void ClearSolution()
        {
            Grid.ResetGrid();
            startCell.type = CellType.A;
            endCell.type = CellType.B;
        }
    }
}
