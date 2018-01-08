using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class FigureZ : Figures
    {
        public FigureZ(int startY, int fieldX, int fieldY)
            : base(fieldX, fieldY)
        {

            X = new int[4];
            Y = new int[4];

            X[0] = 0;
            Y[0] = startY;
            X[1] = 0;
            Y[1] = startY + 1;
            X[2] = 1;
            Y[2] = startY + 1;
            X[3] = 1;
            Y[3] = startY + 2;
        }

        private bool TestRotate()
        {
            //Проверка правого края
            if (Y[0] > FieldY - 3 && Y[0] == Y[1] && Y[3] == Y[0] - 1)
            {
                return true;
            }
            //Проверка верхнего края
            if (X[2] == X[3] && X[0] < 2)
            {
                return true;
            }
            //Проверка левого края
            if (Y[0] < 2 && Y[0] == Y[1] && Y[3] == Y[0] + 1)
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

            if (X[0] == X[1] && X[0] == X[3] - 1)
            {

                X[1] -= 1;
                Y[1] -= 1;
                X[2] -= 2;
                X[3] -= 3;
                Y[3] -= 1;
            }
            else if (Y[0] == Y[1] && Y[0] == Y[3] - 1)
            {
                X[1] += 1;
                Y[1] -= 1;
                Y[2] -= 2;
                X[3] += 1;
                Y[3] -= 3;
            }
            else if (X[0] == X[1] && X[0] == X[3] + 1)
            {
                X[1] += 1;
                Y[1] += 1;
                X[2] += 2;
                X[3] += 3;
                Y[3] += 1;
            }
            else if (Y[0] == Y[1] && Y[0] == Y[3] + 1)
            {
                X[1] -= 1;
                Y[1] += 1;
                Y[2] += 2;
                X[3] -= 1;
                Y[3] += 3;
            }
        }
    }
}
