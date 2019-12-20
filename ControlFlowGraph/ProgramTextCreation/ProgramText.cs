using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlFlowGraph
{
    public class ProgramText
    {
        #region Constructors
        private ProgramText()
        {
            try
            {
                code = String.Empty;
                PenWidth = 2f;
                FontSize = 12f;
                Font = new Font(FontFamily.GenericMonospace, FontSize, FontStyle.Italic);
                regex = new Regex("(/\\*\\s[0-9]+\\s\\*/)");
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize data", e);
            }
        }

        public ProgramText(PictureBox pictureBox) : this()
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
                throw new Exception("Unable to initialize drawing area", e);
            }
        }

        public ProgramText(PictureBox pictureBox, string programCode) : this(pictureBox)
        {
            try
            {
                this.code = programCode;
                this.Lines = code.Split('\n');
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize program text", e);
            }
        }

        public ProgramText(PictureBox pictureBox, string programCode, ProgramTextBrushes brushes) : this(pictureBox, programCode)
        {
            try
            {
                this.Brushes = brushes;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize brushes", e);
            }
        }

        public ProgramText(PictureBox pictureBox, string programCode, Single PenWidth, Single FontSize) : this(pictureBox, programCode)
        {
            try
            {
                this.PenWidth = PenWidth;
                this.FontSize = FontSize;
                Font = new Font(FontFamily.GenericMonospace, FontSize, FontStyle.Italic);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize font or/and pen width", e);
            }
        }

        public ProgramText(PictureBox pictureBox, string programCode, Single PenWidth, Single FontSize, ProgramTextBrushes brushes)
            : this(pictureBox, programCode, PenWidth, FontSize)
        {
            try
            {
                this.Brushes = brushes;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize brushes", e);
            }
        }
        #endregion


        #region Defines
        private string code;
        private float offsetV;
        private Bitmap DrawArea;
        private Graphics g;
        private Font Font;
        private Single FontSize;
        private Single PenWidth;
        private Regex regex;
        private ProgramTextBrushes Brushes { get; set; }
        public string[] Lines { get; private set; }
        #endregion


        #region Main functions
        public void DrawLine(LineDirection direction, float offset)
        {
            try
            {
                float heigth = GetTextSize("1").Height;

                if (direction == LineDirection.Horizontal)
                {
                    g.DrawLine(
                        new Pen(Brushes.Line, PenWidth),
                        new PointF(0, heigth + offset),
                        new PointF(DrawArea.Width, heigth + offset)
                    );
                }
                else if (direction == LineDirection.Vertical)
                {
                    this.offsetV = offset;
                    g.DrawLine(
                        new Pen(Brushes.Line, PenWidth),
                        new PointF(offset, 0),
                        new PointF(offset, DrawArea.Height)
                    );
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DrawText(int lineIndex, PointF iniPosition, bool recursion = false)
        {
            try
            {
                TextType type = TextType.Code;
                string id = "";
                bool stringEmpty = false;

                if (regex.IsMatch(Lines[lineIndex]))
                {
                    type = TextType.Id;

                    id = regex.Match(Lines[lineIndex]).Value;
                    Lines[lineIndex] = regex.Replace(Lines[lineIndex], "");
                    id = id.Replace("/* ", "").Replace(" */", "");

                    if (Lines[lineIndex].Trim() == "")
                    {
                        stringEmpty = true;
                    }
                }
                Lines[lineIndex] = Lines[lineIndex].Replace("\t", "    ");

                if (!recursion)
                {
                    iniPosition.Y += PenWidth * 1.5f;
                }


                if (type == TextType.Code)
                {
                    iniPosition.X += offsetV;

                    g.DrawString(
                        Lines[lineIndex],
                        Font,
                        Brushes.Text,
                        iniPosition
                    );
                }
                else if (type == TextType.Id)
                {
                    if (!stringEmpty)
                    {
                        g.DrawString(
                            id,
                            Font,
                            Brushes.Id,
                            iniPosition
                        );
                    
                        DrawText(lineIndex, iniPosition, true);
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void FillBackground(Brush brush)
        {
            try
            {
                g.FillRectangle(brush, new Rectangle(0, 0, DrawArea.Width, DrawArea.Height));
            }
            catch (Exception e)
            {
                throw new Exception("Unable to fill background", e);
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
                code = String.Empty;
                offsetV = 0;
                FontSize = 0;
                PenWidth = 0;
                regex = null;
                Lines = null;
                DrawArea.Dispose();
                Font.Dispose();
                EndOfDraw();
            }
            catch (Exception e)
            {
                throw new Exception("Unable to dispose data", e);
            }
        }
        #endregion

        #region Additional functions
        private SizeF GetTextSize(string Text)
        {
            return g.MeasureString(Text, Font);
        }
        #endregion

    }
}
