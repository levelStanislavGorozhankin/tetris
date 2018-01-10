using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using System.Timers;



namespace Tetris
{
    class Program
    {
        const int FieldX = 24;
        const int FieldY = 15;
        static Figures[] FiguresArray = new Figures[7];
        static Field field = new Field(4, FieldX, FieldY);
        static int FigureNumber;

        public static void Main()
        {
            Random rnd = new Random();
            FigureNumber = rnd.Next(0, FiguresArray.Length);
            FigureNumber = 0;
            ResetFigures();
            for (var j = 23; j > 17; j--)
            {
                for (var i = 0; i < FieldY - 1; i++)
                {
                    field.field[j, i] = 1;
                }
            }
            field.PasteFigureInField(FiguresArray[FigureNumber]);
            Game();
        }

        static public void Game()
        {
            ConsoleKeyInfo _key;
            Console.CursorVisible = false;

            TimerCallback TFD = new TimerCallback(Down);
            TimerCallback TPF = new TimerCallback(PrintingField);
            Timer TimerFigureDown = new Timer(TFD, null, 0, 1000);
            Timer TimerPrintingField = new Timer(TPF, null, 0, 500);

            while (true)
            {
                // PrintingTechnicalInformation(FiguresArray[FigureNumber]);
                _key = Console.ReadKey(true);

                if (_key.Key == ConsoleKey.UpArrow)
                {
                    Rotate(FiguresArray[FigureNumber]);
                }

                if (_key.Key == ConsoleKey.LeftArrow)
                {
                    Left(FiguresArray[FigureNumber]);
                }

                if (_key.Key == ConsoleKey.RightArrow)
                {
                    Right(FiguresArray[FigureNumber]);
                }

                if (_key.Key == ConsoleKey.DownArrow)
                {
                    Down();
                }
            }
        }

        static public void ResetFigures()
        {
            FiguresArray[0] = new FigureI(6, FieldX, FieldY);
            FiguresArray[1] = new FigureL(6, FieldX, FieldY);
            FiguresArray[2] = new FigureJ(6, FieldX, FieldY);
            FiguresArray[3] = new FigureT(6, FieldX, FieldY);
            FiguresArray[4] = new FigureB(6, FieldX, FieldY);
            FiguresArray[5] = new FigureS(6, FieldX, FieldY);
            FiguresArray[6] = new FigureZ(6, FieldX, FieldY);
        }

        static public void Rotate(Figures figure)
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

        static public void Left(Figures figure)
        {
            if (field.TestMoveLeft(figure))
            {
                field.DeleteFigureFromField(figure);
                figure.Left();
                field.PasteFigureInField(figure);
            }
        }

        static public void Right(Figures figure)
        {
            if (field.TestMoveRight(figure))
            {
                field.DeleteFigureFromField(figure);
                figure.Right();
                field.PasteFigureInField(figure);
            }
        }

        static void Down(object obj)
        {
            Random rnd = new Random();
            if (field.TestBottoming(FiguresArray[FigureNumber]))
            {
                field.FillFieldWithBlocks(FiguresArray[FigureNumber]);
                field.ClearLine();
                ResetFigures();
                FigureNumber = rnd.Next(0, FiguresArray.Length);
                field.PasteFigureInField(FiguresArray[FigureNumber]);
            }
            else
            {
                field.DeleteFigureFromField(FiguresArray[FigureNumber]);
                FiguresArray[FigureNumber].Down();
                field.PasteFigureInField(FiguresArray[FigureNumber]);
            }
        }

        static void Down()
        {
            do
            {
                Random rnd = new Random();
                if (field.TestBottoming(FiguresArray[FigureNumber]))
                {
                    field.FillFieldWithBlocks(FiguresArray[FigureNumber]);
                    field.ClearLine();
                    ResetFigures();
                    FigureNumber = rnd.Next(0, FiguresArray.Length);
                    field.PasteFigureInField(FiguresArray[FigureNumber]);
                    break;
                }
                else
                {
                    field.DeleteFigureFromField(FiguresArray[FigureNumber]);
                    FiguresArray[FigureNumber].Down();
                    field.PasteFigureInField(FiguresArray[FigureNumber]);
                }
            } while (true);
        }

        static public void PrintingField(object obj)
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
