using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderDijkstra.GridDrawer
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Grid;

    public class GridDrawer
    {
        private readonly PictureBox _pb;
        public Grid Grid { get; }

        private const int HorizontalCells = 40;
        private const int VerticalCells = 20;

        private int _cellWidth;
        private int _cellHeight;

        public GridDrawer(PictureBox pb)
        {
            _pb = pb;
            Grid = new Grid(HorizontalCells, VerticalCells);
        }

        public void Draw()
        {
            _cellWidth = _pb.Width / HorizontalCells;
            _cellHeight = _pb.Height / VerticalCells;

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
                                switch (cell.weight)
                                {
                                    case 2: g.FillRectangle(Brushes.LightGray, GetRectangle(x, y)); break;
                                    case 3: g.FillRectangle(Brushes.Silver, GetRectangle(x, y)); break;
                                }
                                break;
                            case CellType.Solid:
                                g.FillRectangle(Brushes.Black, GetRectangle(x, y));
                                break;
                            case CellType.Path:
                                g.FillRectangle(Brushes.Purple, GetRectangle(x, y));
                                break;
                            case CellType.Open:
                                g.FillRectangle(Brushes.LightSkyBlue, GetRectangle(x, y));
                                break;
                            case CellType.Closed:
                                g.FillRectangle(Brushes.LightSeaGreen, GetRectangle(x, y));
                                break;
                            case CellType.Current:
                                g.FillRectangle(Brushes.Crimson, GetRectangle(x, y));
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
            }
        }

        public void gridClicked(int x, int y)
        {
            int xCell = x / _cellWidth;
            int yCell = y / _cellHeight;
            var cell = Grid.GetCell(xCell, yCell);
            cell.type = CellType.Solid;
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
    }
}
