using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Field
    {
        public int Score;
        private int FieldX;
        private int FieldY;
        public int[,] field;
        private int CountLines;
        public int Level;

        public Field(int startX, int fieldX, int fieldY)
        {
            CountLines = 0;
            Level = 1;
            FieldX = fieldX;
            FieldY = fieldY;
            field = new int[FieldX, FieldY];
            for (var x = 0; x < fieldX; x++)
            {
                for (var y = 0; y < fieldY; y++)
                {
                    field[x, y] = 0;
                }
            }
        }

        public bool TestRotationInField(Figures figure)
        {
            for (var i = 0; i < figure.X.Length; i++)
            {
                if (field[figure.X[i], figure.Y[i]] == 1) return false;
            }
            return true;
        }

        public void PasteFigureInField(Figures figure)
        {
            for (var i = 0; i < figure.X.Length; i++)
            {
                field[figure.X[i], figure.Y[i]] = 2;
            }
        }

        public void DeleteFigureFromField(Figures figure)
        {
            for (var i = 0; i < figure.X.Length; i++)
            {
                if (field[figure.X[i], figure.Y[i]] == 2)
                {
                    field[figure.X[i], figure.Y[i]] = 0;
                }
            }
        }

        public void FillFieldWithBlocks(Figures figure)
        {
            for (var i = 0; i < figure.X.Length; i++)
            {
                field[figure.X[i], figure.Y[i]] = 1;
            }
        }

        public bool TestBottoming(Figures figure)
        {
            for (var i = 0; i < figure.X.Length; i++)
            {
                if (figure.X[i] + 1 == FieldX || field[figure.X[i] + 1, figure.Y[i]] == 1) return true;
            }
            return false;
        }

        public bool TestMoveLeft(Figures figure)
        {
            for (var i = 0; i < figure.X.Length; i++)
            {
                if (figure.Y[i] == 0 || field[figure.X[i], figure.Y[i] - 1] == 1) return false;
            }
            return true;
        }

        public bool TestMoveRight(Figures figure)
        {
            for (var i = 0; i < figure.X.Length; i++)
            {
                if (figure.Y[i] == FieldY - 1 || field[figure.X[i], figure.Y[i] + 1] == 1) return false;
            }
            return true;
        }

        public void ClearLine()
        {
            int CountLinesScore = 0;
            int CountBlocks;
            for (var x = FieldX - 1; x > 0; x--)
            {
                CountBlocks = 0;

                for (var y = 0; y < FieldY; y++)
                {
                    if (field[x, y] == 1) CountBlocks++;
                }

                if (CountBlocks == FieldY)
                {
                    CountLines++;
                    CountLinesScore++;
                    for (var y = 0; y < FieldY; y++)
                    {
                        field[x, y] = 0;
                    }

                    for (var x1 = x; x1 > 0; x1--)
                    {
                        for (var y = 0; y < FieldY; y++)
                        {
                            if (field[x1 - 1, y] == 1)
                            {
                                field[x1, y] = 1;
                                field[x1 - 1, y] = 0;
                            }
                        }
                    }
                    x = FieldX;
                    if (CountLines == 1)
                    {
                        Level++;
                        CountLines = 0;
                    }
                }
            }
            Score = Score + 50 * CountLinesScore * 4;
        }
    }
}
