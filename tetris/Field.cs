using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Field
    {
        private int FieldX;
        private int FieldY;
        public int[,] field;

        public Field(int startX, int fieldX, int fieldY)
        {
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
            for (var i=0; i<figure.X.Length;i++)
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
    }
}
