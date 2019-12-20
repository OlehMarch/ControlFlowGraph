using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlFlowGraph
{
    public class CFGraph
    {
        #region Constructors
        private CFGraph()
        {
            try
            {
                PenWidth = 4f;
                FontSize = 20f;
                NodeSize = new SizeF(40, 40);
                Font = new Font(FontFamily.GenericSansSerif, FontSize, FontStyle.Regular);
                nodeBrushes = new NodeBrushes(Brushes.White, Brushes.Black, Brushes.Black);
                Item = new Dictionary<string, CFGNode>();
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize draw area\nError message:\n" + e.Message, e);
            }
        }

        public CFGraph(PictureBox pictureBox) : this()
        {
            try
            {
                DrawArea = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
                pictureBox.Image = DrawArea;
                g = Graphics.FromImage(DrawArea);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize draw area\nError message:\n" + e.Message, e);
            }
        }

        public CFGraph(PictureBox pictureBox, Single NodeSize) : this()
        {
            try
            {
                DrawArea = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
                pictureBox.Image = DrawArea;
                g = Graphics.FromImage(DrawArea);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize draw area\nError message:\n" + e.Message, e);
            }

            try
            {
                this.NodeSize = new SizeF(NodeSize, NodeSize);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to reassign data\nError message:\n" + e.Message, e);
            }

        }

        public CFGraph(PictureBox pictureBox, Single PenWidth, Single FontSize) : this()
        {
            try
            {
                DrawArea = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
                pictureBox.Image = DrawArea;
                g = Graphics.FromImage(DrawArea);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize draw area\nError message:\n" + e.Message, e);
            }

            try
            {
                this.PenWidth = PenWidth;
                this.FontSize = FontSize;
                Font = new Font(FontFamily.GenericSansSerif, FontSize, FontStyle.Regular);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to reassign data\nError message:\n" + e.Message, e);
            }
        }

        public CFGraph(PictureBox pictureBox, Single PenWidth, Single FontSize, Single NodeSize) : this()
        {
            try
            {
                DrawArea = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
                pictureBox.Image = DrawArea;
                g = Graphics.FromImage(DrawArea);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize draw area\nError message:\n" + e.Message, e);
            }

            try
            {
                this.PenWidth = PenWidth;
                this.FontSize = FontSize;
                this.NodeSize = new SizeF(NodeSize, NodeSize);
                Font = new Font(FontFamily.GenericSansSerif, FontSize, FontStyle.Regular);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to reassign data\nError message:\n" + e.Message, e);
            }
        }
        #endregion


        #region Defines
        private Bitmap DrawArea;
        private Graphics g;
        private Font Font;
        private SizeF NodeSize;
        private Single FontSize;
        private Single PenWidth;
        public NodeBrushes nodeBrushes { get; set; }
        public Dictionary<string, CFGNode> Item { get; private set; }
        #endregion


        #region Main Functions
        public void AddNode(string Text, PointF NodeLocation)
        {
            try
            {
                g.DrawEllipse(
                new Pen(nodeBrushes.Outline, PenWidth),
                new RectangleF(
                    NodeLocation.X, NodeLocation.Y,
                    NodeSize.Width, NodeSize.Height
                )
            );
                g.FillEllipse(
                    nodeBrushes.Fill,
                    new RectangleF(
                        NodeLocation.X + PenWidth / 2, NodeLocation.Y + PenWidth / 2,
                        NodeSize.Width - PenWidth, NodeSize.Height - PenWidth
                    )
                );
                g.DrawString(
                    Text,
                    Font,
                    nodeBrushes.Pen,
                    new PointF(
                        NodeLocation.X + ((NodeSize.Width - GetTextSize(Text).Width) / 2),
                        NodeLocation.Y + ((NodeSize.Height - GetTextSize(Text).Height) / 2) + 1
                    )
                );
#if DEBUG
                //g.DrawRectangle(new Pen(Brushes.Red, 1f), new Rectangle(
                //    (int)(NodeLocation.X + ((NodeSize.Width - GetTextSize(Text).Width) / 2)),
                //    (int)(NodeLocation.Y + ((NodeSize.Height - GetTextSize(Text).Height) / 2)) + 1,
                //    (int)GetTextSize(Text).Width,
                //    (int)GetTextSize(Text).Height)
                //);
#endif
                Item.Add(Text, new CFGNode(Text, NodeLocation, NodeSize));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to create the node " + Text, e);
            }
        }

        public void AddConnectionLine(string FirstNodeText, string SecondNodeText)
        {
            bool _alreadyConnected = false;

            try
            {
                if (Item[SecondNodeText].Location.Y - Item[FirstNodeText].Location.Y >= NodeSize.Height * 1.33)
                {
                    g.DrawLines(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        new PointF[]
                        {
                            Item[FirstNodeText].ConnectionPoint.Bottom,
                            new PointF(Item[SecondNodeText].ConnectionPoint.Top.X, Item[SecondNodeText].ConnectionPoint.Top.Y - 12),
                            Item[SecondNodeText].ConnectionPoint.Top
                        }
                    );
                    DrawArrow(Item[SecondNodeText].ConnectionPoint.Top, 'T');
                    _alreadyConnected = true;
                }
                else if (Item[FirstNodeText].Location.Y - Item[SecondNodeText].Location.Y >= NodeSize.Height * 1.33)
                {
                    g.DrawLines(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        new PointF[]
                        {
                            Item[FirstNodeText].ConnectionPoint.Top,
                            new PointF(Item[SecondNodeText].ConnectionPoint.Bottom.X, Item[SecondNodeText].ConnectionPoint.Bottom.Y + 12),
                            Item[SecondNodeText].ConnectionPoint.Bottom
                        }
                    );
                    DrawArrow(Item[SecondNodeText].ConnectionPoint.Bottom, 'B');
                    _alreadyConnected = true;
                }

                if (!_alreadyConnected)
                {
                    if (Item[SecondNodeText].Location.X - Item[FirstNodeText].Location.X >= NodeSize.Height * 1.33)
                    {
                        g.DrawLines(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        new PointF[]
                        {
                            Item[FirstNodeText].ConnectionPoint.Right,
                            new PointF(Item[SecondNodeText].ConnectionPoint.Left.X - 12, Item[SecondNodeText].ConnectionPoint.Left.Y),
                            Item[SecondNodeText].ConnectionPoint.Left
                        }
                    );
                        DrawArrow(Item[SecondNodeText].ConnectionPoint.Left, 'L');
                    }
                    else if (Item[FirstNodeText].Location.X - Item[SecondNodeText].Location.X >= NodeSize.Height * 1.33)
                    {
                        g.DrawLines(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        new PointF[]
                        {
                            Item[FirstNodeText].ConnectionPoint.Left,
                            new PointF(Item[SecondNodeText].ConnectionPoint.Right.X + 12, Item[SecondNodeText].ConnectionPoint.Right.Y),
                            Item[SecondNodeText].ConnectionPoint.Right
                        }
                    );
                        DrawArrow(Item[SecondNodeText].ConnectionPoint.Right, 'R');
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to establish a connection between " + FirstNodeText + " and " + SecondNodeText, e);
            }
        }

        public void AddConnectionCurve(string FirstNodeText, string SecondNodeText, ConnectionSide Side = ConnectionSide.Left)
        {
            try
            {
                if (Side == ConnectionSide.Left)
                {
                    #region Left
                    PointF CurveStartPoint = new PointF
                    (
                        Item[FirstNodeText].ConnectionPoint.Left.X - 12,
                        Item[FirstNodeText].ConnectionPoint.Left.Y
                    );
                    PointF CurveEndPoint = new PointF
                    (
                        Item[SecondNodeText].ConnectionPoint.Left.X - 12,
                        Item[SecondNodeText].ConnectionPoint.Left.Y
                    );

                    SizeF Size = new SizeF(
                        (Math.Abs(Item[SecondNodeText].Location.Y - Item[FirstNodeText].Location.Y) < (NodeSize.Height * 3))
                            ? new SizeF(0.2f, 0.8f)
                            : new SizeF(0, 1)
                    );
                    float middlePointOffsetX = ((Math.Abs(Item[SecondNodeText].Location.Y - Item[FirstNodeText].Location.Y) < (NodeSize.Height * 3))
                        ? (NodeSize.Width / 4) + 8 : (NodeSize.Width / 2) + 4);
                    float middlePointX = 0;
                    float middlePointY = 0;

                    if (Item[FirstNodeText].Location.X == Item[SecondNodeText].Location.X)
                    {
                        middlePointX = Item[FirstNodeText].Location.X - middlePointOffsetX;
                        middlePointY = ((Item[SecondNodeText].Location.Y + Item[FirstNodeText].Location.Y) / 2) + (NodeSize.Height / 2);
                    }
                    else if (Item[FirstNodeText].Location.X < Item[SecondNodeText].Location.X)
                    {
                        if (Item[FirstNodeText].Location.Y < Item[SecondNodeText].Location.Y)
                        {
                            middlePointX = ((Item[SecondNodeText].Location.X + Item[FirstNodeText].Location.X) / 2) - (NodeSize.Width / 4);
                            middlePointY = Item[SecondNodeText].Location.Y
                                + MiddlePointOffsetY(FirstNodeText, SecondNodeText, Side, Size);
                        }
                        else
                        {
                            middlePointX = ((Item[SecondNodeText].Location.X + Item[FirstNodeText].Location.X) / 2) - (NodeSize.Width / 4);
                            middlePointY = Item[SecondNodeText].Location.Y
                                + MiddlePointOffsetY(FirstNodeText, SecondNodeText, Side, Size);
                        }
                    }
                    else
                    {
                        if (Item[FirstNodeText].Location.Y < Item[SecondNodeText].Location.Y)
                        {
                            middlePointX = ((Item[SecondNodeText].Location.X + Item[FirstNodeText].Location.X) / 2) - (NodeSize.Width / 4);
                            middlePointY = Item[FirstNodeText].Location.Y
                                + MiddlePointOffsetY(FirstNodeText, SecondNodeText, Side, Size);
                        }
                        else
                        {
                            middlePointX = ((Item[SecondNodeText].Location.X + Item[FirstNodeText].Location.X) / 2) - (NodeSize.Width / 4);
                            middlePointY = Item[FirstNodeText].Location.Y
                                + MiddlePointOffsetY(FirstNodeText, SecondNodeText, Side, Size);
                        }
                    }

                    PointF CurveMiddlePoint = new PointF(
                        middlePointX,
                        middlePointY
                    );

                    g.DrawLine(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        Item[FirstNodeText].ConnectionPoint.Left,
                        CurveStartPoint
                    );
                    g.DrawCurve(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        new PointF[]
                        {
                        CurveStartPoint,
                        CurveMiddlePoint,
                        CurveEndPoint
                        }
                    );
                    g.DrawLine(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        Item[SecondNodeText].ConnectionPoint.Left,
                        CurveEndPoint
                    );
                    DrawArrow(Item[SecondNodeText].ConnectionPoint.Left, 'L');
                    #endregion
                }
                else
                {
                    #region Right
                    PointF CurveStartPoint = new PointF
                    (
                        Item[FirstNodeText].ConnectionPoint.Right.X + 12,
                        Item[FirstNodeText].ConnectionPoint.Right.Y
                    );
                    PointF CurveEndPoint = new PointF
                    (
                        Item[SecondNodeText].ConnectionPoint.Right.X + 12,
                        Item[SecondNodeText].ConnectionPoint.Right.Y
                    );

                    SizeF Size = new SizeF(
                        (Math.Abs(Item[SecondNodeText].Location.Y - Item[FirstNodeText].Location.Y) < (NodeSize.Height * 3))
                            ? new SizeF(0.2f, 0.8f)
                            : new SizeF(0, 1)
                    );
                    float middlePointOffsetX = ((Math.Abs(Item[SecondNodeText].Location.Y - Item[FirstNodeText].Location.Y) < (NodeSize.Height * 3))
                        ? (NodeSize.Width * 5 / 4) + 8 : (NodeSize.Width * 3 / 2) + 4);
                    float middlePointX = 0;
                    float middlePointY = 0;

                    if (Item[FirstNodeText].Location.X == Item[SecondNodeText].Location.X)
                    {
                        middlePointX = Item[FirstNodeText].Location.X + middlePointOffsetX;
                        middlePointY = ((Item[SecondNodeText].Location.Y + Item[FirstNodeText].Location.Y) / 2) + (NodeSize.Height / 2);
                    }
                    else if (Item[FirstNodeText].Location.X < Item[SecondNodeText].Location.X)
                    {
                        if (Item[FirstNodeText].Location.Y < Item[SecondNodeText].Location.Y)
                        {
                            middlePointX = ((Item[SecondNodeText].Location.X + Item[FirstNodeText].Location.X) / 2) + (NodeSize.Width * 5 / 4);
                            middlePointY = Item[FirstNodeText].Location.Y
                                + MiddlePointOffsetY(FirstNodeText, SecondNodeText, Side, Size);
                        }
                        else
                        {
                            middlePointX = ((Item[SecondNodeText].Location.X + Item[FirstNodeText].Location.X) / 2) + (NodeSize.Width * 5 / 4);
                            middlePointY = Item[FirstNodeText].Location.Y
                                + MiddlePointOffsetY(FirstNodeText, SecondNodeText, Side, Size);
                        }
                    }
                    else
                    {
                        if (Item[FirstNodeText].Location.Y < Item[SecondNodeText].Location.Y)
                        {
                            middlePointX = ((Item[SecondNodeText].Location.X + Item[FirstNodeText].Location.X) / 2) + (NodeSize.Width * 5 / 4);
                            middlePointY = Item[SecondNodeText].Location.Y
                                + MiddlePointOffsetY(FirstNodeText, SecondNodeText, Side, Size);
                        }
                        else
                        {
                            middlePointX = ((Item[SecondNodeText].Location.X + Item[FirstNodeText].Location.X) / 2) + (NodeSize.Width * 5 / 4);
                            middlePointY = Item[SecondNodeText].Location.Y
                                + MiddlePointOffsetY(FirstNodeText, SecondNodeText, Side, Size);
                        }
                    }

                    PointF CurveMiddlePoint = new PointF(
                        middlePointX,
                        middlePointY
                    );

                    g.DrawLine(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        Item[FirstNodeText].ConnectionPoint.Right,
                        CurveStartPoint
                    );
                    g.DrawCurve(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        new PointF[]
                        {
                        CurveStartPoint,
                        CurveMiddlePoint,
                        CurveEndPoint
                        }
                    );
                    g.DrawLine(
                        new Pen(nodeBrushes.Pen, PenWidth),
                        Item[SecondNodeText].ConnectionPoint.Right,
                        CurveEndPoint
                    );
                    DrawArrow(Item[SecondNodeText].ConnectionPoint.Right, 'R');
                    #endregion
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to establish a connection between " + FirstNodeText + " and " + SecondNodeText, e);
            }
        }

        public void SaveToBitmap(string pathToSave, string fileName)
        {
            DrawArea.Save(pathToSave + "\\" + fileName + ".jpg", ImageFormat.Jpeg);
        }

        public void EndOfDraw()
        {
            try
            {
                g.Dispose();
            }
            catch { }
        }

        public void Dispose()
        {
            try
            { 
                NodeSize = Size.Empty;
                FontSize = 0;
                PenWidth = 0;
                Item.Clear();
                Font.Dispose();
                DrawArea.Dispose();
                EndOfDraw();
            }
            catch (Exception e)
            {
                throw new Exception("Unable to dispose data", e);
            }
        }
        #endregion

        #region Additional Functions
        private SizeF GetTextSize(string Text)
        {
            return g.MeasureString(Text, Font);
        }

        private void DrawArrow(PointF EndPoint, char ConnectionPoint)
        {
            PointF[] arrow = new PointF[3];

            if (ConnectionPoint == 'T')
            {
                arrow = new PointF[]
                {
                    new PointF(0, 0),
                    new PointF(3, 5),
                    new PointF(6, 0)
                };
                for (uint i = 0; i < 3; ++i)
                {
                    arrow[i] = PointF.Add(arrow[i], new SizeF(EndPoint.X - 3, EndPoint.Y - 6));
                }
            }
            else if (ConnectionPoint == 'B')
            {
                arrow = new PointF[]
                {
                    new PointF(0, 5),
                    new PointF(3, 0),
                    new PointF(6, 5)
                };
                for (uint i = 0; i < 3; ++i)
                {
                    arrow[i] = PointF.Add(arrow[i], new SizeF(EndPoint.X - 3, EndPoint.Y + 1));
                }
            }
            else if (ConnectionPoint == 'L')
            {
                arrow = new PointF[]
                {
                    new PointF(0, 0),
                    new PointF(5, 3),
                    new PointF(0, 6)
                };
                for (uint i = 0; i < 3; ++i)
                {
                    arrow[i] = PointF.Add(arrow[i], new SizeF(EndPoint.X - 6, EndPoint.Y - 3));
                }
            }
            else if (ConnectionPoint == 'R')
            {
                arrow = new PointF[]
                {
                    new PointF(5, 0),
                    new PointF(0, 3),
                    new PointF(5, 6)
                };
                for (uint i = 0; i < 3; ++i)
                {
                    arrow[i] = PointF.Add(arrow[i], new SizeF(EndPoint.X + 1, EndPoint.Y - 3));
                }
            }
            else
            {
                throw new Exception("Incorrect connection point");
            }

            try
            {
                g.DrawPolygon(
                    new Pen(nodeBrushes.Pen, PenWidth),
                    arrow
                );
                g.FillPolygon(
                    nodeBrushes.Pen,
                    arrow
                );
            }
            catch (Exception e)
            {
                throw new Exception("Unable to draw arrow\nError message:\n" + e.Message, e);
            }
        }

        private float MiddlePointOffsetY(string FirstNodeText, string SecondNodeText, ConnectionSide Side, SizeF Size)
        {
            float midPointY = 0;

            if (Side == ConnectionSide.Left)
            {
                if (
                    ((Item[FirstNodeText].Location.Y < Item[SecondNodeText].Location.Y) && ((Item[FirstNodeText].Location.X < Item[SecondNodeText].Location.X)))
                    || ((Item[FirstNodeText].Location.Y > Item[SecondNodeText].Location.Y) && ((Item[FirstNodeText].Location.X > Item[SecondNodeText].Location.X))))
                {
                    midPointY = NodeSize.Height * Size.Width;
                }
                else if (((Item[FirstNodeText].Location.Y > Item[SecondNodeText].Location.Y) && ((Item[FirstNodeText].Location.X < Item[SecondNodeText].Location.X)))
                    || ((Item[FirstNodeText].Location.Y < Item[SecondNodeText].Location.Y) && ((Item[FirstNodeText].Location.X > Item[SecondNodeText].Location.X))))
                {
                    midPointY = NodeSize.Height * Size.Height;
                }
            }
            else
            {
                if (
                    ((Item[FirstNodeText].Location.Y < Item[SecondNodeText].Location.Y) && ((Item[FirstNodeText].Location.X < Item[SecondNodeText].Location.X)))
                    || ((Item[FirstNodeText].Location.Y > Item[SecondNodeText].Location.Y) && ((Item[FirstNodeText].Location.X > Item[SecondNodeText].Location.X))))
                {
                    midPointY = NodeSize.Height * Size.Height;
                }
                else if (((Item[FirstNodeText].Location.Y > Item[SecondNodeText].Location.Y) && ((Item[FirstNodeText].Location.X < Item[SecondNodeText].Location.X)))
                    || ((Item[FirstNodeText].Location.Y < Item[SecondNodeText].Location.Y) && ((Item[FirstNodeText].Location.X > Item[SecondNodeText].Location.X))))
                {
                    midPointY = NodeSize.Height * Size.Width;
                }
            }

            return midPointY;
        }
        #endregion

    }
}
