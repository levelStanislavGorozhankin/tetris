using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class FigureB : Figures
    {
        public FigureB(int startY, int fieldX, int fieldY)
             : base(fieldX, fieldY)
        {
            X = new int[4];
            Y = new int[4];

            X[0] = 0;
            Y[0] = startY;
            X[1] = 1;
            Y[1] = startY;
            X[2] = 0;
            Y[2] = startY + 1;
            X[3] = 1;
            Y[3] = startY + 1;
        }
        public override void Rotate()
        {
        }

    }
}
