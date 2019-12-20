using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlFlowGraph
{
    public class ProgramTextMaster
    {
        #region Constructors
        /// <summary>
        /// Формирование изображения с текстом программы.
        /// </summary>
        /// <param name="pictureBox">Елемент формы PictureBox на который будет выполнена отрисовка</param>
        public ProgramTextMaster(PictureBox pictureBox)
        {
            try
            {
                progText = new ProgramText(pictureBox);
                pt_PictureBox = pictureBox;
                pictureBox.SizeChanged += PictureBox_SizeChanged;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize program text", e);
            }
        }

        /// <summary>
        /// Формирование изображения с текстом программы.
        /// </summary>
        /// <param name="pictureBox">Елемент формы PictureBox на который будет выполнена отрисовка</param>
        /// <param name="programCode">Текст программы для отрисовки</param>
        /// <param name="brushes">Набор цветов для отрисовки</param>
        public ProgramTextMaster(PictureBox pictureBox, string programCode, ProgramTextBrushes brushes)
        {
            try
            {
                progText = new ProgramText(pictureBox, programCode, brushes);
                pt_PictureBox = pictureBox;
                pt_ProgramCode = programCode;
                pt_Brushes = brushes;
                pictureBox.SizeChanged += PictureBox_SizeChanged;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize program text", e);
            }
        }

        /// <summary>
        /// Формирование изображения с текстом программы.
        /// </summary>
        /// <param name="pictureBox">Елемент формы PictureBox на который будет выполнена отрисовка</param>
        /// /// <param name="programCode">Текст программы для отрисовки</param>
        /// /// <param name="penWidth">Ширина линии отрисовка</param>
        /// /// <param name="fontSize">Размер шрифта текста</param>
        /// <param name="brushes">Набор цветов для отрисовки</param>
        public ProgramTextMaster(PictureBox pictureBox, string programCode, Single penWidth, Single fontSize, ProgramTextBrushes brushes)
        {
            try
            {
                progText = new ProgramText(pictureBox, programCode, penWidth, fontSize, brushes);
                pt_PictureBox = pictureBox;
                pt_ProgramCode = programCode;
                pt_PenWidth = penWidth;
                pt_FontSize = fontSize;
                pt_Brushes = brushes;
                pictureBox.SizeChanged += PictureBox_SizeChanged;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to initialize program text", e);
            }
        }
        #endregion


        #region Defines
        private ProgramText progText;
        private float verticalOffset;

        private PictureBox pt_PictureBox;
        private string pt_ProgramCode;
        private ProgramTextBrushes pt_Brushes;
        private float pt_PenWidth;
        private float pt_FontSize;
        #endregion


        #region Main functions
        /// <summary>
        /// Загрузка текста программы для дальнейшей обработки.
        /// </summary>
        /// <param name="code">Текст программы для отрисовки</param>
        public void SetProgramCode(string code)
        {
            pt_ProgramCode = code;
            if (pt_Brushes.Id != null)
            {
                progText = new ProgramText(pt_PictureBox, code, pt_Brushes);
            }
            else
            {
                progText = new ProgramText(pt_PictureBox, code);
            }
        }

        /// <summary>
        /// Установление набора цветов для отрисовки.
        /// </summary>
        /// <param name="brushes">Набор цветов для отрисовки</param>
        public void SetBrushes(ProgramTextBrushes brushes)
        {
            pt_Brushes = brushes;
            progText = new ProgramText(pt_PictureBox, pt_ProgramCode, brushes);
        }

        /// <summary>
        /// Установление набора цветов для отрисовки.
        /// </summary>
        /// <param name="Id">Цвет для отрисовки идентификатора</param>
        /// <param name="Text">Цвет для отрисовки текста программы</param>
        /// <param name="Line">Цвет для отрисовки линий</param>
        public void SetBrushes(Brush Id, Brush Text, Brush Line)
        {
            pt_Brushes = new ProgramTextBrushes(Text, Id, Line);
            progText = new ProgramText(pt_PictureBox, pt_ProgramCode, pt_Brushes);
        }

        /// <summary>
        /// Создание изображения с текстом программы.
        /// </summary>
        /// <param name="verticalOffset">Вертикальный отступ (с левого края) для ID'ов</param>
        public void CreateProgramText(float verticalOffset)
        {
            try
            {
                this.verticalOffset = verticalOffset;

                progText.FillBackground(Brushes.White);
                // Draw vertical line which separates Id's from Code
                progText.DrawLine(LineDirection.Vertical, verticalOffset);

                // Main loop which generates text and lines
                for (int i = 0; i < progText.Lines.Length; ++i)
                {
                    progText.DrawText(i, new PointF(verticalOffset / 4, 20 * (i + 1)));
                    progText.DrawLine(LineDirection.Horizontal, 20 * (i + 1));
                }

                // Stop drawing
                progText.EndOfDraw();
            }
            catch (Exception e)
            {
                throw new Exception("Unable to draw program text", e);
            }
        }

        /// <summary>
        /// Сохранение изображения с текстом программы.
        /// </summary>
        /// <param name="pathToSave">Путь для сохранения (куда сохранять)</param>
        /// <param name="fileName">Название сохраняемого файла (без расширения)</param>
        public void SaveToBitmap(string pathToSave, string fileName)
        {
            try
            {
                progText.SaveToBitmap(pathToSave, fileName);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to save image", e);
            }
        }

        /// <summary>
        /// Освобождение ресурсов.
        /// </summary>
        public void Dispose()
        {
            try
            {
                verticalOffset = 0;
                pt_PenWidth = 0;
                pt_FontSize = 0;
                pt_ProgramCode = String.Empty;
                pt_PictureBox.Dispose();
                progText.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Unable to dispose data", e);
            }
        }
        #endregion

        #region Resize Event
        private void PictureBox_SizeChanged(object sender, EventArgs e)
        {
            if ((pt_PictureBox != null)
                && (pt_ProgramCode == String.Empty)
                && (pt_Brushes.Id == null)
                && (pt_PenWidth == 0)
                && (pt_FontSize == 0))
            {
                progText = new ProgramText(pt_PictureBox);
            }
            else if((pt_PictureBox != null)
                && (pt_ProgramCode != String.Empty)
                && (pt_Brushes.Id == null)
                && (pt_PenWidth == 0)
                && (pt_FontSize == 0))
            {
                progText = new ProgramText(pt_PictureBox, pt_ProgramCode);
            }
            else if ((pt_PictureBox != null)
                && (pt_ProgramCode != String.Empty)
                && (pt_Brushes.Id != null)
                && (pt_PenWidth == 0)
                && (pt_FontSize == 0))
            {
                progText = new ProgramText(pt_PictureBox, pt_ProgramCode, pt_Brushes);
            }
            else if ((pt_PictureBox != null)
                && (pt_ProgramCode != String.Empty)
                && (pt_Brushes.Id != null)
                && (pt_PenWidth != 0)
                && (pt_FontSize != 0))
            {
                progText = new ProgramText(pt_PictureBox, pt_ProgramCode, pt_PenWidth, pt_FontSize, pt_Brushes);
            }
            else
            {
                throw new ArgumentNullException();
            }

            CreateProgramText(verticalOffset);
        }
        #endregion

    }
}
