using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Tetris
{
    class MyGraphics
    {
        public const int ROWS_COUNT = 20;
        public const int COLUMNS_COUNT = 10;
        public const int CELL_SIZE = 25;

        private static Pen blackPen = new Pen(Color.Black);

        /// <summary>
        /// Отобразить фигуру на поле
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="panel"></param>
        public static void DrawFigure(Figure figure, Panel panel)
        {            
            Graphics graphics = panel.CreateGraphics();
            Color color = SwitchColor(figure.FigureColor);          

            Pen pen = new Pen(color);
            Brush brush = pen.Brush;                        

            for(int i =0; i < Figure.COORD_COUNT; i++)
            {
                graphics.FillRectangle(brush, figure.CoordinatesArr[i].x * CELL_SIZE + 1, figure.CoordinatesArr[i].y * CELL_SIZE + 1, CELL_SIZE - 1, CELL_SIZE - 1);
                graphics.DrawRectangle(blackPen, figure.CoordinatesArr[i].x * CELL_SIZE, figure.CoordinatesArr[i].y * CELL_SIZE, CELL_SIZE, CELL_SIZE);
            }
        }

        /// <summary>
        /// Отобразить следующую фигуру
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="color"></param>
        /// <param name="panel"></param>
        public static void DrawFigureOnPlNextFigure(Figure.Coordinates[] coordinates, FigureColors color, Panel panel)
        {
            if(coordinates.Length != 0)
            {
                Graphics graphics = panel.CreateGraphics();
                Color newColor = SwitchColor(color);

                graphics.Clear(SystemColors.Control);

                Pen pen = new Pen(newColor);
                Brush brush = pen.Brush;

                for (int i = 0; i < Figure.COORD_COUNT; i++)
                {
                    graphics.FillRectangle(brush, coordinates[i].x * CELL_SIZE + 1, coordinates[i].y * CELL_SIZE + 1, CELL_SIZE - 1, CELL_SIZE - 1);
                    graphics.DrawRectangle(blackPen, coordinates[i].x * CELL_SIZE, coordinates[i].y * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                }
            }
        }

        /// <summary>
        /// Стереть фигуру
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="panel"></param>
        public static void EraseFigure(Figure figure, Panel panel)
        {
            Graphics graphics = panel.CreateGraphics();

            Pen pen = new Pen(SystemColors.Control);
            Brush brush = pen.Brush;

            for (int i = 0; i < Figure.COORD_COUNT; i++)
            {
                graphics.FillRectangle(brush, figure.CoordinatesArr[i].x * CELL_SIZE, figure.CoordinatesArr[i].y * CELL_SIZE, CELL_SIZE + 1, CELL_SIZE + 1);
            }             
        }

        /// <summary>
        /// Вернуть стертые грани квадрата у фигуры
        /// </summary>
        /// <param name="neighborsList"></param>
        /// <param name="panel"></param>
        public static void DrawCells(List<Figure.NeighboringCells> neighborsList, Panel panel)
        {
            if(neighborsList.Count != 0)
            {
                Graphics graphics = panel.CreateGraphics();

                for(int i = 0; i < neighborsList.Count; i++)
                {
                    graphics.DrawRectangle(blackPen, neighborsList[i].x * CELL_SIZE, neighborsList[i].y * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                }
            }
        }

        /// <summary>
        /// Вернуть нужный цвет
        /// </summary>
        /// <param name="figureColor"></param>
        /// <returns></returns>
        private static Color SwitchColor(FigureColors figureColor)
        {
            Color color = Color.Empty;

            switch (figureColor)
            {
                case FigureColors.Blue:
                    color = Color.Blue;
                    break;
                case FigureColors.Green:
                    color = Color.Green;
                    break;
                case FigureColors.Indigo:
                    color = Color.Indigo;
                    break;
                case FigureColors.Maroon:
                    color = Color.Maroon;
                    break;
                case FigureColors.Red:
                    color = Color.Red;
                    break;
                case FigureColors.Orange:
                    color = Color.Orange;
                    break;
                case FigureColors.Purple:
                    color = Color.Purple;
                    break;
            }

            return color;
        }        

        /// <summary>
        /// Стереть все клетки
        /// </summary>
        public static void ClearCells(Panel panel)
        {
            Graphics graphics = panel.CreateGraphics();

            graphics.Clear(SystemColors.Control);
        }

        /// <summary>
        /// Отрисовать стертые клетки
        /// </summary>
        public static void DrawCells(Panel panel)
        {
            Graphics graphics = panel.CreateGraphics();
            Color color;
            Pen pen;
            Brush brush;

            for(int i = 0; i < COLUMNS_COUNT; i++)
            {
                for(int j = 0; j < ROWS_COUNT; j++)
                {
                    if(Field.cellStates[i,j] == CellStates.Figure)
                    {
                        color = Field.GetCellCOlor(i, j);
                        pen = new Pen(color);
                        brush = pen.Brush;

                        graphics.FillRectangle(brush, i * CELL_SIZE + 1, j * CELL_SIZE + 1, CELL_SIZE - 1, CELL_SIZE - 1);
                        graphics.DrawRectangle(blackPen, i * CELL_SIZE, j * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                    }
                }
            }
        }

        public static void ClearRow(int y, Panel panel)
        {
            Graphics graphics = panel.CreateGraphics();
           
            graphics.FillRectangle(SystemBrushes.Control, 0 , y * CELL_SIZE , CELL_SIZE * COLUMNS_COUNT, CELL_SIZE - 1);
        }
    }
}
