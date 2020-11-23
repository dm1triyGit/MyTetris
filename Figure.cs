using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    public enum FiguresNames
    {
        J,
        I,
        O,
        L,
        Z,
        T,
        S
    }

    public enum FigureColors
    {
        Blue,
        Green,
        Indigo,
        Maroon,
        Red,
        Orange,
        Purple,
        Null
    }

    enum FigurePositions
    {
        First = 1,
        Second,
        Third,
        Fourth
    }

    class Figure
    {
        const int FIGURES_COUNT = 7;
        const int COLORS_COUNT = 7;
        const int START_MAIN_COORD_Y = 1;
        public const int INDEX_MAIN_CELL = 0;
        public const int COORD_COUNT = 4;

        public FiguresNames FigureName { get; set; }
        public FigureColors FigureColor { get; set; }
        private FigurePositions Position { get; set; }
        public Coordinates[] CoordinatesArr { get; set; }

        private static Random rand = new Random();

        public Figure(FiguresNames figureName, FigureColors figureColor, Coordinates[] coordinates)
        {
            FigureName = figureName;
            FigureColor = figureColor;
            Position = FigurePositions.First;
            CoordinatesArr = coordinates;
        }

        /// <summary>
        /// Координаты текущей фигуры
        /// </summary>
        public struct Coordinates
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// Координаты клеток соседних фигур
        /// </summary>
        public struct NeighboringCells
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// Создать фигуру
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Figure CreateFigure(FiguresNames name)
        {
            Coordinates[] coordinatesArr = new Coordinates[COORD_COUNT];
            FigureColors color = FigureColors.Null;

            int coordX = rand.Next(2, 8); // (2 - 7) Диапазон генерации начальной основной координаты оси Х 

            switch (name)
            {
                case FiguresNames.J:
                    coordinatesArr = CreateJ(coordinatesArr, coordX, START_MAIN_COORD_Y);
                    color = FigureColors.Blue;
                    break;
                case FiguresNames.I:
                    coordinatesArr = CreateI(coordinatesArr, coordX, START_MAIN_COORD_Y);
                    color = FigureColors.Green;
                    break;
                case FiguresNames.O:
                    coordinatesArr = CreateO(coordinatesArr, coordX, START_MAIN_COORD_Y);
                    color = FigureColors.Indigo;
                    break;
                case FiguresNames.L:
                    coordinatesArr = CreateL(coordinatesArr, coordX, START_MAIN_COORD_Y);
                    color = FigureColors.Maroon;
                    break;
                case FiguresNames.Z:
                    coordinatesArr = CreateZ(coordinatesArr, coordX, START_MAIN_COORD_Y);
                    color = FigureColors.Red;
                    break;
                case FiguresNames.T:
                    coordinatesArr = CreateT(coordinatesArr, coordX, START_MAIN_COORD_Y);
                    color = FigureColors.Orange;
                    break;
                case FiguresNames.S:
                    coordinatesArr = CreateS(coordinatesArr, coordX, START_MAIN_COORD_Y);
                    color = FigureColors.Purple;
                    break;
            }

            Figure figure = new Figure(name, color, coordinatesArr);

            return figure;
        }

        /// <summary>
        /// Создать координаты следующей фигуры
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Coordinates[] CreateCoordinatesForPlNextFigure(FiguresNames name, out FigureColors color)
        {
            Coordinates[] coordinatesArr = new Coordinates[COORD_COUNT];
            color = FigureColors.Null;

            const int START_COORD_X = 1;
            const int START_COORD_Y = 1;

            switch (name)
            {
                case FiguresNames.J:
                    coordinatesArr = CreateJ(coordinatesArr, START_COORD_X, START_COORD_Y);
                    color = FigureColors.Blue;
                    break;
                case FiguresNames.I:
                    coordinatesArr = CreateI(coordinatesArr, START_COORD_X, START_COORD_Y);
                    color = FigureColors.Green;
                    break;
                case FiguresNames.O:
                    coordinatesArr = CreateO(coordinatesArr, START_COORD_X, START_COORD_Y);
                    color = FigureColors.Indigo;
                    break;
                case FiguresNames.L:
                    coordinatesArr = CreateL(coordinatesArr, START_COORD_X, START_COORD_Y);
                    color = FigureColors.Maroon;
                    break;
                case FiguresNames.Z:
                    coordinatesArr = CreateZ(coordinatesArr, START_COORD_X, START_COORD_Y);
                    color = FigureColors.Red;
                    break;
                case FiguresNames.T:
                    coordinatesArr = CreateT(coordinatesArr, START_COORD_X, START_COORD_Y);
                    color = FigureColors.Orange;
                    break;
                case FiguresNames.S:
                    coordinatesArr = CreateS(coordinatesArr, START_COORD_X, START_COORD_Y);
                    color = FigureColors.Purple;
                    break;
            }

            return coordinatesArr;
        }

        public static FiguresNames GenerateName()
        {
            FiguresNames name = (FiguresNames)rand.Next(FIGURES_COUNT);
            return name;
        }

        public static FigureColors GenerateColor()
        {
            FigureColors color = (FigureColors)rand.Next(COLORS_COUNT);
            return color;
        }

        public void Fall()
        {
            for (int i = 0; i < CoordinatesArr.Length; i++)
            {
                CoordinatesArr[i].y += 1;
            }
        }

        public void MoveLeft()
        {
            for (int i = 0; i < CoordinatesArr.Length; i++)
            {
                CoordinatesArr[i].x -= 1;
            }
        }

        public void MoveRight()
        {
            for (int i = 0; i < CoordinatesArr.Length; i++)
            {
                CoordinatesArr[i].x += 1;

            }
        }

        public void Turn()
        {
            switch (FigureName)
            {
                case FiguresNames.J:
                    TurnJ();
                    break;
                case FiguresNames.I:
                    TurnI();
                    break;
                case FiguresNames.O:
                    // Квадрат не поворачивается
                    break;
                case FiguresNames.L:
                    TurnL();
                    break;
                case FiguresNames.Z:
                    TurnZ();
                    break;
                case FiguresNames.T:
                    TurnT();
                    break;
                case FiguresNames.S:
                    TurnS();
                    break;
            }
        }

        public bool CanFall()
        {
            bool canFall = true;

            for (int i = 0; i < CoordinatesArr.Length; i++)
            {
                if (CoordinatesArr[i].y + 1 >= MyGraphics.ROWS_COUNT || Field.cellStates[CoordinatesArr[i].x, CoordinatesArr[i].y + 1] == CellStates.Figure)
                    canFall = false;
            }

            return canFall;
        }

        public bool CanMoveLeft()
        {
            bool canMoveLeft = true;

            for (int i = 0; i < CoordinatesArr.Length; i++)
            {
                if (CoordinatesArr[i].x - 1 <= -1 || Field.cellStates[CoordinatesArr[i].x - 1, CoordinatesArr[i].y]
                    == CellStates.Figure) // -1 - точка, находящаяся левее крайней левой координаты оси Х
                    canMoveLeft = false;
            }

            return canMoveLeft;
        }

        public bool CanMoveRight()
        {
            bool canMoveRight = true;

            for (int i = 0; i < CoordinatesArr.Length; i++)
            {
                if (CoordinatesArr[i].x + 1 >= MyGraphics.COLUMNS_COUNT || Field.cellStates[CoordinatesArr[i].x + 1, CoordinatesArr[i].y]
                    == CellStates.Figure)
                    canMoveRight = false;
            }

            return canMoveRight;
        }

        private bool CanTurn()
        {
            if (CoordinatesArr[INDEX_MAIN_CELL].x + 1 < MyGraphics.COLUMNS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].x - 1 >= 0
               && CoordinatesArr[INDEX_MAIN_CELL].y + 1 < MyGraphics.ROWS_COUNT
               && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
               && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
               && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure
               && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
               && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
               && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
               && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure
               && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure)
                return true;
            else
                return false;
        }

        private bool CanTurnI()
        {
            switch (Position)
            {
                case FigurePositions.First:
                    if (CoordinatesArr[INDEX_MAIN_CELL].x + 1 < MyGraphics.COLUMNS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].x - 1 >= 0
              && CoordinatesArr[INDEX_MAIN_CELL].y + 2 < MyGraphics.ROWS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].y - 1 >= 0
              && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
              && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
              && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y + 2] != CellStates.Figure
              && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
              && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
              && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
              && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y + 2] != CellStates.Figure
              && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y + 2] != CellStates.Figure)
                        return true;
                    else
                        return false;

                case FigurePositions.Second:
                    if (CoordinatesArr[INDEX_MAIN_CELL].x + 1 < MyGraphics.COLUMNS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].x - 2 >= 0
             && CoordinatesArr[INDEX_MAIN_CELL].y + 1 < MyGraphics.ROWS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].y - 1 >= 0
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 2, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 2, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 2, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure)
                        return true;
                    else
                        return false;

                case FigurePositions.Third:
                    if (CoordinatesArr[INDEX_MAIN_CELL].x + 1 < MyGraphics.COLUMNS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].x - 1 >= 0
             && CoordinatesArr[INDEX_MAIN_CELL].y + 1 < MyGraphics.ROWS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].y - 2 >= 0
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y - 2] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y - 2] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y - 2] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 1, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure)
                        return true;
                    else
                        return false;

                case FigurePositions.Fourth:
                    if (CoordinatesArr[INDEX_MAIN_CELL].x + 2 < MyGraphics.COLUMNS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].x - 1 >= 0
             && CoordinatesArr[INDEX_MAIN_CELL].y + 1 < MyGraphics.ROWS_COUNT && CoordinatesArr[INDEX_MAIN_CELL].y - 1 >= 0
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 2, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 2, CoordinatesArr[INDEX_MAIN_CELL].y - 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x + 2, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure
             && Field.cellStates[CoordinatesArr[INDEX_MAIN_CELL].x - 1, CoordinatesArr[INDEX_MAIN_CELL].y + 1] != CellStates.Figure)
                        return true;
                    else
                        return false;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Провериить какие клетки находятся рядом с фигурой, чтобы потом отрисовать грани этой клетки
        /// </summary>
        public List<NeighboringCells> CheckNeighboringCells()
        {
            List<NeighboringCells> neighborsList = new List<NeighboringCells>();

            for (int i = 0; i < CoordinatesArr.Length; i++)
            {
                if (CoordinatesArr[i].x - 1 >= 0)
                    if (Field.cellStates[CoordinatesArr[i].x - 1, CoordinatesArr[i].y] == CellStates.Figure)
                    {
                        NeighboringCells neighboringCells;
                        neighboringCells.x = CoordinatesArr[i].x - 1;
                        neighboringCells.y = CoordinatesArr[i].y;
                        neighborsList.Add(neighboringCells);
                    }

                if (CoordinatesArr[i].x + 1 < MyGraphics.COLUMNS_COUNT)
                    if (Field.cellStates[CoordinatesArr[i].x + 1, CoordinatesArr[i].y] == CellStates.Figure)
                    {
                        NeighboringCells neighboringCells;
                        neighboringCells.x = CoordinatesArr[i].x + 1;
                        neighboringCells.y = CoordinatesArr[i].y;
                        neighborsList.Add(neighboringCells);
                    }

                if (CoordinatesArr[i].y - 1 >= 0)
                    if (Field.cellStates[CoordinatesArr[i].x, CoordinatesArr[i].y - 1] == CellStates.Figure)
                    {
                        NeighboringCells neighboringCells;
                        neighboringCells.x = CoordinatesArr[i].x;
                        neighboringCells.y = CoordinatesArr[i].y - 1;
                        neighborsList.Add(neighboringCells);
                    }

                if (CoordinatesArr[i].y + 1 < MyGraphics.ROWS_COUNT)
                    if (Field.cellStates[CoordinatesArr[i].x, CoordinatesArr[i].y + 1] == CellStates.Figure)
                    {
                        NeighboringCells neighboringCells;
                        neighboringCells.x = CoordinatesArr[i].x;
                        neighboringCells.y = CoordinatesArr[i].y + 1;
                        neighborsList.Add(neighboringCells);
                    }

                if (CoordinatesArr[i].x - 1 >= 0 && CoordinatesArr[i].y - 1 >= 0)
                    if (Field.cellStates[CoordinatesArr[i].x - 1, CoordinatesArr[i].y - 1] == CellStates.Figure)
                    {
                        NeighboringCells neighboringCells;
                        neighboringCells.x = CoordinatesArr[i].x - 1;
                        neighboringCells.y = CoordinatesArr[i].y - 1;
                        neighborsList.Add(neighboringCells);
                    }

                if (CoordinatesArr[i].x + 1 < MyGraphics.COLUMNS_COUNT && CoordinatesArr[i].y - 1 >= 0)
                    if (Field.cellStates[CoordinatesArr[i].x + 1, CoordinatesArr[i].y - 1] == CellStates.Figure)
                    {
                        NeighboringCells neighboringCells;
                        neighboringCells.x = CoordinatesArr[i].x + 1;
                        neighboringCells.y = CoordinatesArr[i].y - 1;
                        neighborsList.Add(neighboringCells);
                    }

                if (CoordinatesArr[i].x + 1 < MyGraphics.COLUMNS_COUNT && CoordinatesArr[i].y + 1 < MyGraphics.ROWS_COUNT)
                    if (Field.cellStates[CoordinatesArr[i].x + 1, CoordinatesArr[i].y + 1] == CellStates.Figure)
                    {
                        NeighboringCells neighboringCells;
                        neighboringCells.x = CoordinatesArr[i].x + 1;
                        neighboringCells.y = CoordinatesArr[i].y + 1;
                        neighborsList.Add(neighboringCells);
                    }

                if (CoordinatesArr[i].x - 1 >= 0 && CoordinatesArr[i].y + 1 < MyGraphics.ROWS_COUNT)
                    if (Field.cellStates[CoordinatesArr[i].x - 1, CoordinatesArr[i].y + 1] == CellStates.Figure)
                    {
                        NeighboringCells neighboringCells;
                        neighboringCells.x = CoordinatesArr[i].x - 1;
                        neighboringCells.y = CoordinatesArr[i].y + 1;
                        neighborsList.Add(neighboringCells);
                    }
            }

            return neighborsList;
        }        

        #region Создание фигур

        private static Coordinates[] CreateJ(Coordinates[] coordinates, int mainCoordX, int mainCoordY)
        {
            coordinates[INDEX_MAIN_CELL].x = mainCoordX;
            coordinates[INDEX_MAIN_CELL].y = mainCoordY;

            coordinates[1].x = coordinates[INDEX_MAIN_CELL].x;
            coordinates[1].y = coordinates[INDEX_MAIN_CELL].y - 1;

            coordinates[2].x = coordinates[INDEX_MAIN_CELL].x;
            coordinates[2].y = coordinates[INDEX_MAIN_CELL].y + 1;

            coordinates[3].x = coordinates[INDEX_MAIN_CELL].x - 1;
            coordinates[3].y = coordinates[INDEX_MAIN_CELL].y + 1;

            return coordinates;
        }

        private static Coordinates[] CreateI(Coordinates[] coordinates, int mainCoordX, int mainCoordY)
        {
            coordinates[INDEX_MAIN_CELL].x = mainCoordX;
            coordinates[INDEX_MAIN_CELL].y = mainCoordY;

            coordinates[1].x = coordinates[INDEX_MAIN_CELL].x - 1;
            coordinates[1].y = coordinates[INDEX_MAIN_CELL].y;

            coordinates[2].x = coordinates[INDEX_MAIN_CELL].x + 1;
            coordinates[2].y = coordinates[INDEX_MAIN_CELL].y;

            coordinates[3].x = coordinates[INDEX_MAIN_CELL].x + 2;
            coordinates[3].y = coordinates[INDEX_MAIN_CELL].y;

            return coordinates;
        }

        private static Coordinates[] CreateO(Coordinates[] coordinates, int mainCoordX, int mainCoordY)
        {
            coordinates[INDEX_MAIN_CELL].x = mainCoordX;
            coordinates[INDEX_MAIN_CELL].y = mainCoordY;

            coordinates[1].x = coordinates[INDEX_MAIN_CELL].x + 1;
            coordinates[1].y = coordinates[INDEX_MAIN_CELL].y;

            coordinates[2].x = coordinates[INDEX_MAIN_CELL].x + 1;
            coordinates[2].y = coordinates[INDEX_MAIN_CELL].y + 1;

            coordinates[3].x = coordinates[INDEX_MAIN_CELL].x;
            coordinates[3].y = coordinates[INDEX_MAIN_CELL].y + 1;

            return coordinates;
        }

        private static Coordinates[] CreateL(Coordinates[] coordinates, int mainCoordX, int mainCoordY)
        {
            coordinates[INDEX_MAIN_CELL].x = mainCoordX;
            coordinates[INDEX_MAIN_CELL].y = mainCoordY;

            coordinates[1].x = coordinates[INDEX_MAIN_CELL].x;
            coordinates[1].y = coordinates[INDEX_MAIN_CELL].y - 1;

            coordinates[2].x = coordinates[INDEX_MAIN_CELL].x;
            coordinates[2].y = coordinates[INDEX_MAIN_CELL].y + 1;

            coordinates[3].x = coordinates[INDEX_MAIN_CELL].x + 1;
            coordinates[3].y = coordinates[INDEX_MAIN_CELL].y + 1;

            return coordinates;
        }

        private static Coordinates[] CreateZ(Coordinates[] coordinates, int mainCoordX, int mainCoordY)
        {
            coordinates[INDEX_MAIN_CELL].x = mainCoordX;
            coordinates[INDEX_MAIN_CELL].y = mainCoordY;

            coordinates[1].x = coordinates[INDEX_MAIN_CELL].x - 1;
            coordinates[1].y = coordinates[INDEX_MAIN_CELL].y;

            coordinates[2].x = coordinates[INDEX_MAIN_CELL].x;
            coordinates[2].y = coordinates[INDEX_MAIN_CELL].y + 1;

            coordinates[3].x = coordinates[INDEX_MAIN_CELL].x + 1;
            coordinates[3].y = coordinates[INDEX_MAIN_CELL].y + 1;

            return coordinates;
        }

        private static Coordinates[] CreateT(Coordinates[] coordinates, int mainCoordX, int mainCoordY)
        {
            coordinates[INDEX_MAIN_CELL].x = mainCoordX;
            coordinates[INDEX_MAIN_CELL].y = mainCoordY;

            coordinates[1].x = coordinates[INDEX_MAIN_CELL].x - 1;
            coordinates[1].y = coordinates[INDEX_MAIN_CELL].y;

            coordinates[2].x = coordinates[INDEX_MAIN_CELL].x + 1;
            coordinates[2].y = coordinates[INDEX_MAIN_CELL].y;

            coordinates[3].x = coordinates[INDEX_MAIN_CELL].x;
            coordinates[3].y = coordinates[INDEX_MAIN_CELL].y + 1;

            return coordinates;
        }

        private static Coordinates[] CreateS(Coordinates[] coordinates, int mainCoordX, int mainCoordY)
        {
            coordinates[INDEX_MAIN_CELL].x = mainCoordX;
            coordinates[INDEX_MAIN_CELL].y = mainCoordY;

            coordinates[1].x = coordinates[INDEX_MAIN_CELL].x + 1;
            coordinates[1].y = coordinates[INDEX_MAIN_CELL].y;

            coordinates[2].x = coordinates[INDEX_MAIN_CELL].x;
            coordinates[2].y = coordinates[INDEX_MAIN_CELL].y + 1;

            coordinates[3].x = coordinates[INDEX_MAIN_CELL].x - 1;
            coordinates[3].y = coordinates[INDEX_MAIN_CELL].y + 1;

            return coordinates;
        }

        #endregion

        #region Реализация поврота фигур

        private void TurnJ()
        {
            if (CanTurn())
                switch (Position)
                {
                    case FigurePositions.First:
                        TurnPositionToSecondJ();
                        break;
                    case FigurePositions.Second:
                        TurnPositionToThirdJ();
                        break;
                    case FigurePositions.Third:
                        TurnPositionToFourthJ();
                        break;
                    case FigurePositions.Fourth:
                        TurnPositionToFirstJ();
                        break;
                }
        }

        private void TurnPositionToSecondJ()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            Position = FigurePositions.Second;
        }

        private void TurnPositionToThirdJ()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            Position = FigurePositions.Third;
        }

        private void TurnPositionToFourthJ()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.Fourth;
        }

        private void TurnPositionToFirstJ()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            Position = FigurePositions.First;
        }

        private void TurnI()
        {
            if (CanTurnI())
                switch (Position)
                {
                    case FigurePositions.First:
                        TurnPositionToSecondI();
                        break;
                    case FigurePositions.Second:
                        TurnPositionToThirdI();
                        break;
                    case FigurePositions.Third:
                        TurnPositionToFourthI();
                        break;
                    case FigurePositions.Fourth:
                        TurnPositionToFirstI();
                        break;
                }
        }

        private void TurnPositionToSecondI()
        {
            CoordinatesArr[INDEX_MAIN_CELL].x += 1;

            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 2;

            Position = FigurePositions.Second;
        }

        private void TurnPositionToThirdI()
        {
            CoordinatesArr[INDEX_MAIN_CELL].y += 1;

            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 2;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.Third;
        }

        private void TurnPositionToFourthI()
        {
            CoordinatesArr[INDEX_MAIN_CELL].x -= 1;
            CoordinatesArr[INDEX_MAIN_CELL].y -= 1;

            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 2;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            Position = FigurePositions.Fourth;
        }

        private void TurnPositionToFirstI()
        {

            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 2;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.First;
        }

        private void TurnL()
        {
            if (CanTurn())
                switch (Position)
                {
                    case FigurePositions.First:
                        TurnPositionToSecondL();
                        break;
                    case FigurePositions.Second:
                        TurnPositionToThirdL();
                        break;
                    case FigurePositions.Third:
                        TurnPositionToFourthL();
                        break;
                    case FigurePositions.Fourth:
                        TurnPositionToFirstL();
                        break;
                }
        }

        private void TurnPositionToSecondL()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            Position = FigurePositions.Second;
        }

        private void TurnPositionToThirdL()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            Position = FigurePositions.Third;
        }

        private void TurnPositionToFourthL()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.Fourth;
        }

        private void TurnPositionToFirstL()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            Position = FigurePositions.First;
        }

        private void TurnZ()
        {
            if (CanTurn())
                switch (Position)
                {
                    case FigurePositions.First:
                        TurnPositionToSecondZ();
                        break;
                    case FigurePositions.Second:
                        TurnPositionToThirdZ();
                        break;
                    case FigurePositions.Third:
                        TurnPositionToFourthZ();
                        break;
                    case FigurePositions.Fourth:
                        TurnPositionToFirstZ();
                        break;
                }
        }

        private void TurnPositionToSecondZ()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.Second;
        }

        private void TurnPositionToThirdZ()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            Position = FigurePositions.Third;
        }

        private void TurnPositionToFourthZ()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            Position = FigurePositions.Fourth;
        }

        private void TurnPositionToFirstZ()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.First;
        }

        private void TurnT()
        {
            if (CanTurn())
                switch (Position)
                {
                    case FigurePositions.First:
                        TurnPositionToSecondT();
                        break;
                    case FigurePositions.Second:
                        TurnPositionToThirdT();
                        break;
                    case FigurePositions.Third:
                        TurnPositionToFourthT();
                        break;
                    case FigurePositions.Fourth:
                        TurnPositionToFirstT();
                        break;
                }
        }

        private void TurnPositionToSecondT()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.Second;
        }

        private void TurnPositionToThirdT()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.Third;
        }

        private void TurnPositionToFourthT()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            Position = FigurePositions.Fourth;
        }

        private void TurnPositionToFirstT()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.First;
        }

        private void TurnS()
        {
            if (CanTurn())
                switch (Position)
                {
                    case FigurePositions.First:
                        TurnPositionToSecondS();
                        break;
                    case FigurePositions.Second:
                        TurnPositionToThirdS();
                        break;
                    case FigurePositions.Third:
                        TurnPositionToFourthS();
                        break;
                    case FigurePositions.Fourth:
                        TurnPositionToFirstS();
                        break;
                }
        }

        private void TurnPositionToSecondS()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            Position = FigurePositions.Second;
        }

        private void TurnPositionToThirdS()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            Position = FigurePositions.Third;
        }

        private void TurnPositionToFourthS()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y - 1;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            Position = FigurePositions.Fourth;
        }

        private void TurnPositionToFirstS()
        {
            CoordinatesArr[1].x = CoordinatesArr[INDEX_MAIN_CELL].x + 1;
            CoordinatesArr[1].y = CoordinatesArr[INDEX_MAIN_CELL].y;

            CoordinatesArr[2].x = CoordinatesArr[INDEX_MAIN_CELL].x;
            CoordinatesArr[2].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            CoordinatesArr[3].x = CoordinatesArr[INDEX_MAIN_CELL].x - 1;
            CoordinatesArr[3].y = CoordinatesArr[INDEX_MAIN_CELL].y + 1;

            Position = FigurePositions.First;
        }

        #endregion
    }
}
