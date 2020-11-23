using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public enum CellStates
    {
        Empty,
        Figure,
    }
       
    class Field
    {
        private delegate void EventHandler(Events myEvent);
        private event EventHandler IncreaseScore;

        public static CellStates[,] cellStates;
        private static FigureColors[,] cellColor;

        private int score;
        public bool gameOver = true;

        private Figure figure;
        private FiguresNames figureName;
        private FigureColors figureColor;
        private FormTetris form;

        private List<Figure.NeighboringCells> neighborsList;
        private List<int> fullRowsList;

        private enum Events
        {
            Fall = 1,
            DeleteRows,
        }

        public Field(FormTetris formTetris)
        {
            form = formTetris;

            ClearField();
            
            cellStates = new CellStates[MyGraphics.COLUMNS_COUNT, MyGraphics.ROWS_COUNT];
            cellColor = new FigureColors[MyGraphics.COLUMNS_COUNT, MyGraphics.ROWS_COUNT];

            neighborsList = new List<Figure.NeighboringCells>();
            fullRowsList = new List<int>();

            GenerateFigureParameters();

            figure = Figure.CreateFigure(figureName);
            MyGraphics.DrawFigure(figure, form.splitContainer1.Panel2);

            GenerateFigureParameters();

            PrintFigureOnPlNextFigure();

            gameOver = false;
            score = 0;
            form.LblCurrentScore.Text = "Очки: " + score;

            SetEvents();
            form.LblCurrentScore.Visible = true;

            form.timer1.Start();
        }

        private void SetEvents()
        {
            IncreaseScore += Field_IncreaseScore;
        }        

        /// <summary>
        /// Обновить поле
        /// </summary>
        private void ClearField()
        {
            MyGraphics.ClearCells(form.splitContainer1.Panel2);
        }

        /// <summary>
        /// Сгенерировать название и цвет фигуры
        /// </summary>
        private void GenerateFigureParameters()
        {
            figureName = Figure.GenerateName();
        }

        /// <summary>
        /// Отобразить следуюущую фигуру
        /// </summary>
        private void PrintFigureOnPlNextFigure()
        {
            var coordinates = Figure.CreateCoordinatesForPlNextFigure(figureName, out figureColor);
            MyGraphics.DrawFigureOnPlNextFigure(coordinates,figureColor, form.PlNextFigure);
        }

        /// <summary>
        /// Заморозить фигуру на текущем месте, чтобы после этого создать новую
        /// </summary>
        private void FreezeFigure()
        {
            for (int i = 0; i < figure.CoordinatesArr.Length; i++)
            {
                cellStates[figure.CoordinatesArr[i].x, figure.CoordinatesArr[i].y] = CellStates.Figure;
                cellColor[figure.CoordinatesArr[i].x, figure.CoordinatesArr[i].y] = figure.FigureColor;
            }
        }

        /// <summary>
        /// Проверить, сможет ли созданная фигура падать
        /// </summary>
        private void CheckEndGame()
        {
            if (!figure.CanFall())
            {
                GameOver();
            }
            else
            {
                GenerateFigureParameters();
                PrintFigureOnPlNextFigure();
                MyGraphics.DrawFigure(figure, form.splitContainer1.Panel2);
            }
        }

        /// <summary>
        /// Проверить заполнение строк
        /// </summary>
        public List<int> CheckFilling()
        {
            fullRowsList.Clear();           

            for (int i = 0; i < figure.CoordinatesArr.Length; i++)
            {
                int count = 0;

                if (!fullRowsList.Contains(figure.CoordinatesArr[i].y))
                {
                    for (int j = 1; j < MyGraphics.COLUMNS_COUNT; j++)
                    {
                        if (figure.CoordinatesArr[i].x + j < MyGraphics.COLUMNS_COUNT)
                            if (cellStates[figure.CoordinatesArr[i].x + j, figure.CoordinatesArr[i].y] == CellStates.Figure)
                                count++;
                        if (figure.CoordinatesArr[i].x - j >= 0)
                            if (cellStates[figure.CoordinatesArr[i].x - j, figure.CoordinatesArr[i].y] == CellStates.Figure)
                                count++;
                    }

                    if (count == 9) //  Строка заполнена
                        fullRowsList.Add(figure.CoordinatesArr[i].y);
                }
            }

            fullRowsList.Sort();

            return fullRowsList;
        }

        private void GameOver()
        {
            form.timer1.Stop();           
            gameOver = true;
            MessageBox.Show("Проиграл!");
        }

        /// <summary>
        /// Получить цвет клетки
        /// </summary>
        public static Color GetCellCOlor(int x, int y)
        {
            Color color = Color.Empty;

            switch(cellColor[x,y])
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
        /// Осуществить смещение
        /// </summary>
        private void ChangeCoordinates(int y)
        {
            for (int i = 0; i < MyGraphics.COLUMNS_COUNT; i++)
            {
                cellStates[i, y] = CellStates.Empty;
            }

            for (int i = MyGraphics.COLUMNS_COUNT - 1; i >= 0; i--)
            {
                for (int j = MyGraphics.ROWS_COUNT - 1; j >= 0; j--)
                {
                    if (cellStates[i, j] == CellStates.Figure && j < y)
                    {
                        cellStates[i, j] = CellStates.Empty;

                        if (j + 1 < MyGraphics.ROWS_COUNT)
                        {
                            cellStates[i, j + 1] = CellStates.Figure;
                            cellColor[i, j + 1] = cellColor[i, j];
                        }
                    }
                }
            }
        }

        #region Движения

        /// <summary>
        /// Падение
        /// </summary>
        public void Fall()
        {
            if (!figure.CanFall())
            {
                FreezeFigure();

                int[] coordinatesForDel = CheckFilling().ToArray();

                if (coordinatesForDel.Length > 0)
                {
                    for (int i = 0; i < coordinatesForDel.Length; i++)
                    {                       
                        ChangeCoordinates(coordinatesForDel[i]);
                        IncreaseScore?.Invoke(Events.DeleteRows);
                    }

                    MyGraphics.ClearCells(form.splitContainer1.Panel2);
                    MyGraphics.DrawCells(form.splitContainer1.Panel2);
                    neighborsList.Clear();                    
                }    
                else
                {
                    IncreaseScore?.Invoke(Events.Fall);
                }

                figure = Figure.CreateFigure(figureName);

                CheckEndGame();
            }
            else
            {              
                MyGraphics.EraseFigure(figure, form.splitContainer1.Panel2);
                figure.Fall();
                MyGraphics.DrawCells(neighborsList, form.splitContainer1.Panel2);
                MyGraphics.DrawFigure(figure, form.splitContainer1.Panel2);

                neighborsList = figure.CheckNeighboringCells();
            }
        }

        /// <summary>
        /// Ход влево
        /// </summary>
        public void MoveLeft()
        {
            if (figure.CanMoveLeft())
            {
                MyGraphics.EraseFigure(figure, form.splitContainer1.Panel2);
                figure.MoveLeft();
                MyGraphics.DrawCells(neighborsList, form.splitContainer1.Panel2);
                MyGraphics.DrawFigure(figure, form.splitContainer1.Panel2);

                neighborsList = figure.CheckNeighboringCells();
            }
        }

        /// <summary>
        /// Ход вправо
        /// </summary>
        public void MoveRight()
        {
            if (figure.CanMoveRight())
            {
                MyGraphics.EraseFigure(figure, form.splitContainer1.Panel2);
                figure.MoveRight();
                MyGraphics.DrawCells(neighborsList, form.splitContainer1.Panel2);
                MyGraphics.DrawFigure(figure, form.splitContainer1.Panel2);

                neighborsList = figure.CheckNeighboringCells();
            }
        }

        /// <summary>
        /// Поворот фигуры
        /// </summary>
        public void Turn()
        {
            MyGraphics.EraseFigure(figure, form.splitContainer1.Panel2);
            figure.Turn();
            MyGraphics.DrawCells(neighborsList, form.splitContainer1.Panel2);
            MyGraphics.DrawFigure(figure, form.splitContainer1.Panel2);
            neighborsList = figure.CheckNeighboringCells();
        }

        #endregion

        private void Field_IncreaseScore(Events myEvent)
        {
            switch(myEvent)
            {
                case Events.Fall:
                    score += 1;
                    break;

                case Events.DeleteRows:
                    score += 10;
                    break;
            }

            form.LblCurrentScore.Text = "Очки: " + score;
        }
    }
}
