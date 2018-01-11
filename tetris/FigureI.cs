using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class FigureI : Figures
    {
        public FigureI(int startY, int fieldX, int fieldY)
             : base(fieldX, fieldY)
        {
            X = new int[4];
            Y = new int[4];
            for (var i = 0; i < X.Length; i++)
            {
                X[i] = i;
                Y[i] = startY;
            }
        }

        private bool TestRotate()
        {
            //Проверка вращения у верхней границы
            if (Y[0] == Y[1] && Y[0] > FieldY - 4)
            {
                return true;
            }
            //Проверка вращения у нижней границы
            if (X[0] == X[1] && X[0]>FieldX-4)
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

            if (Y[0] == Y[1])
            {
                for (var i = 0; i < X.Length; i++)
                {
                    X[i] = x;
                    Y[i] = y;
                    y++;
                }
            }
            else
            {
                for (var i = 0; i < X.Length; i++)
                {
                    X[i] = x;
                    Y[i] = y;
                    x++;
                }
            }
        }
    }
}
