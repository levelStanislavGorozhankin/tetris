using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Figures
    {
        public int[] X;
        public int[] Y;
        protected readonly int FieldX;
        protected readonly int FieldY;

        public Figures(int fieldX, int fieldY)
        {
            FieldX = fieldX;
            FieldY = fieldY;
        }

        public virtual void Rotate()
        {

        }

        public void Right()
        {
            if (Y[3] < FieldY - 1)
            {
                for (var i = 0; i < X.Length; i++)
                {
                    Y[i]++;
                }
            }
        }

        public void Left()
        {
            if (Y[0] > 0)
            {
                for (var i = 0; i < X.Length; i++)
                {
                    Y[i]--;
                }
            }
        }

        public void Down()
        {
            if (X[3] < FieldX - 1)
            {
                for (var i = 0; i < X.Length; i++)
                {
                    X[i]++;
                }
            }
        }

        public void FigureRestore(FigureTemp figureTmp)
        {
            for (var i = 0; i < figureTmp.X.Length; i++)
            {
                X[i] = figureTmp.X[i];
                Y[i] = figureTmp.Y[i];
            }
        }
    }

}
