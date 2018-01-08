using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using System.Timers;



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
            for(var i=0; i<figureTmp.X.Length;i++)
            {
                X[i] = figureTmp.X[i];
                Y[i] = figureTmp.Y[i];
            }
        }
    }

    class Program
    {
        const int FieldX = 24;
        const int FieldY = 15;

        public static void Main()
        {
            Field field = new Field(4, FieldX, FieldY);
            Figures[] FiguresArray = new Figures[7];
            Random rnd = new Random();

            ResetFigures(FiguresArray);
            int FigureNumber = rnd.Next(0, FiguresArray.Length);

            field.PasteFigureInField(FiguresArray[FigureNumber]);
            Game(FiguresArray, field, FigureNumber);
            
        }

        static public void Game(Figures[] FiguresArray, Field field, int figureNumber)
        {
            ConsoleKeyInfo _key;
            Console.CursorVisible = false;
            while (true)
            {
                //_key = Console.ReadKey(true);
                //while (Console.KeyAvailable == false)
                //{


                    PrintingField(field);

                    PrintingTechnicalInformation(FiguresArray[figureNumber]);

               


                _key = Console.ReadKey(true);

                    if (_key.Key == ConsoleKey.UpArrow)
                    {
                        Rotate(field, FiguresArray[figureNumber]);
                        continue;
                    }

                    if (_key.Key == ConsoleKey.LeftArrow)
                    {
                        Left(field, FiguresArray[figureNumber]);
                        continue;
                    }

                    if (_key.Key == ConsoleKey.RightArrow)
                    {
                        Right(field, FiguresArray[figureNumber]);
                        continue;
                    }

                    if (_key.Key == ConsoleKey.DownArrow)
                    {
                        Down(field, FiguresArray, ref figureNumber);
                    }
                ////}
            }
        }

        static public void ResetFigures(Figures[] FiguresArray)
        {
            FiguresArray[0] = new FigureI(6, FieldX, FieldY);
            FiguresArray[1] = new FigureL(6, FieldX, FieldY);
            FiguresArray[2] = new FigureJ(6, FieldX, FieldY);
            FiguresArray[3] = new FigureT(6, FieldX, FieldY);
            FiguresArray[4] = new FigureB(6, FieldX, FieldY);
            FiguresArray[5] = new FigureS(6, FieldX, FieldY);
            FiguresArray[6] = new FigureZ(6, FieldX, FieldY);
        }

        static public void Rotate(Field field, Figures figure)
        {
            FigureTemp figureTmp = new FigureTemp(figure);
            field.DeleteFigureFromField(figure);
            figure.Rotate();
            if (field.TestRotationInField(figure)) field.PasteFigureInField(figure);
            else
            {
                figure.FigureRestore(figureTmp);
                field.PasteFigureInField(figure);
            }
        }

        static public void Left(Field field, Figures figure)
        {
            if (field.TestMoveLeft(figure))
            {
                field.DeleteFigureFromField(figure);
                figure.Left();
                field.PasteFigureInField(figure);
            }
        }

        static public void Right(Field field, Figures figure)
        {
            if (field.TestMoveRight(figure))
            {
                field.DeleteFigureFromField(figure);
                figure.Right();
                field.PasteFigureInField(figure);
            }
        }

        static public void Down(Field field, Figures[] figuresArray, ref int figureNumber)
        {
            Random rnd = new Random();
            //do
            //{
                if (field.TestBottoming(figuresArray[figureNumber]))
                {
                    field.FillFieldWithBlocks(figuresArray[figureNumber]);
                    ResetFigures(figuresArray);
                    figureNumber = rnd.Next(0, figuresArray.Length);
                    field.PasteFigureInField(figuresArray[figureNumber]);
                    //break;
                }
                else
                {
                    field.DeleteFigureFromField(figuresArray[figureNumber]);
                    figuresArray[figureNumber].Down();
                    field.PasteFigureInField(figuresArray[figureNumber]);
                }
                PrintingField(field);
            //} while (true);
        }

        static public void PrintingField(Field field)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("┌");
            for (var i = 0; i < FieldY * 2 - 1; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine("┐");
            for (var x = 0; x < FieldX; x++)
            {
                Console.Write("│");
                for (var y = 0; y < FieldY; y++)
                {
                    switch (field.field[x, y])
                    {
                        case 0:
                            {
                                Console.Write(" ");
                                break;
                            }
                        case 1:
                            {
                                Console.Write("■");
                                break;
                            }
                        case 2:
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("■");
                                Console.ResetColor();
                                break;
                            }
                    }
                    if (y != FieldY - 1) Console.Write(" ");
                }
                Console.Write("│");
                Console.WriteLine();
            }
            Console.Write("└");
            for (var i = 0; i < FieldY * 2 - 1; i++)
            {
                Console.Write("─");
            }
            Console.Write("┘");
        }

        static void PrintingTechnicalInformation(Figures figure)
        {
            Console.SetCursorPosition(50, 0);
            Console.Write("                                                       ");
            Console.SetCursorPosition(50, 1);
            Console.WriteLine("                                                       ");
            Console.SetCursorPosition(50, 0);
            for (var i = 0; i < figure.X.Length; i++)
            {
                Console.Write(figure.X[i] + " ");
            }
            Console.SetCursorPosition(50, 1);
            for (var i = 0; i < figure.Y.Length; i++)
            {
                Console.Write(figure.Y[i] + " ");
            }
            Console.SetCursorPosition(50, 3);
            Console.Write(figure);
            Console.SetCursorPosition(0, 0);
        }
    }
}
