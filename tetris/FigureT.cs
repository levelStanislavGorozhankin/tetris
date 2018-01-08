using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class FigureT : Figures
    {
        public FigureT(int startY, int fieldX, int fieldY)
            : base(fieldX, fieldY)
        {

            X = new int[4];
            Y = new int[4];

            for (var i = 0; i < X.Length - 1; i++)
            {
                X[i] = i;
                Y[i] = startY;
            }
            X[3] = 1;
            Y[3] = startY + 1;
        }

        private bool TestRotate()
        {
            //Проверка правого края
            if (Y[0] == Y[1] && Y[3] == FieldY - 1 || X[0] == X[1] && Y[3] == FieldY - 2 && X[3] == X[2] + 1)
            {
                return true;
            }
            //Проверка верхнего края
            if (X[0] == 0 || X[0] == X[1] && X[3] == 0)
            {
                return true;
            }
            //Проверка левого края
            if (X[0] == X[3] + 1 && Y[0] == 0 || Y[3] == 0 && Y[0] == Y[1])
            {
                return true;
            }
            return false;
        }

        public override void Rotate()
        {

            int x = X[0];
            int y = Y[0];

            if (TestRotate()) return;

            if (Y[0] == Y[1] && Y[3] == Y[2] + 1)
            {
                for (var i = 0; i < X.Length - 1; i++)
                {
                    X[i] = x;
                    Y[i] = y;
                    y++;
                }
                X[3] = x - 1;
                Y[3] = y - 2;
            }
            else if (X[0] == X[1] && X[3] == X[0] - 1)
            {
                for (var i = 0; i < X.Length - 1; i++)
                {
                    X[i] = x;
                    Y[i] = y;
                    x--;
                }
                X[3] = x + 2;
                Y[3] = y - 1;
            }
            else if (Y[0] == Y[1] && Y[3] == Y[2] - 1)
            {
                for (var i = 0; i < X.Length - 1; i++)
                {
                    X[i] = x;
                    Y[i] = y;
                    y--;
                }
                X[3] = x + 1;
                Y[3] = y + 2;
            }
            else if (X[0] == X[1] && X[3] == X[0] + 1)
            {
                for (var i = 0; i < X.Length - 1; i++)
                {
                    X[i] = x;
                    Y[i] = y;
                    x++;
                }
                X[3] = x - 2;
                Y[3] = y + 1;
            }
        }
    }
}
