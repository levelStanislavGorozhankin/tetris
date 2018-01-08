using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class FigureTemp

    {
        public int[] X;
        public int[] Y;

        public FigureTemp(Figures figure)
            
        {
            X = new int[4];
            Y = new int[4];
            
            for (var i=0; i< figure.X.Length;i++)
            {
                X[i] = figure.X[i];
                Y[i] = figure.Y[i];
            }
        }

    }
}
