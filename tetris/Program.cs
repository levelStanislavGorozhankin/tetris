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
        private static Mutex mut = new Mutex();
        static Figures[] FiguresArray = new Figures[7];
        static Field field = new Field(4, FieldX, FieldY);
        static int FigureNumber;

        public static void Main()
        {
            Random rnd = new Random();
            FigureNumber = rnd.Next(0, FiguresArray.Length);
        //    FigureNumber = 6;
            ResetFigures();
            //for (var j = 23; j > 17; j--)
            //{
            //    for (var i = 0; i < FieldY - 1; i++)
            //    {
            //        field.field[j, i] = 1;
            //    }
            //}
            field.PasteFigureInField(FiguresArray[FigureNumber]);
            Game();

            //test 12.01.2018
            //test 12.01.2018 - 2
            //test 12.01.2018 - 3
            //test 12.01.2018 - 4
            //test 12.01.2018 - 5
        }

        static public void Game()
        {
            
            ConsoleKeyInfo _key;
            Console.CursorVisible = false;

            TimerCallback TFD = new TimerCallback(Down);
            TimerCallback TPF = new TimerCallback(PrintingField);
            Timer TimerFigureDown = new Timer(TFD, null, 0, 1000);
            Timer TimerPrintingField = new Timer(TPF, null, 0, 100);

            while (true)
            {
                 
                _key = Console.ReadKey(true);

                switch (_key.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            Rotate(FiguresArray[FigureNumber]);
                            break;
                        }

                    case ConsoleKey.LeftArrow:
                        {
                            Left(FiguresArray[FigureNumber]);
                            break;
                        }

                    case ConsoleKey.RightArrow:
                        {
                            Right(FiguresArray[FigureNumber]);
                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            Down();
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            TimerFigureDown.Dispose();
                            TimerPrintingField.Dispose();
                            Console.SetCursorPosition(0,26);
                            Environment.Exit(0);
                            break;
                        }
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
                Thread.Sleep(30);
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
            mut.WaitOne();
            PrintingTechnicalInformation(FiguresArray[FigureNumber]);
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
            mut.ReleaseMutex();
        }

        static void PrintingTechnicalInformation(Figures figure)
        {
            Console.SetCursorPosition(0, 26);
            Console.Write("                                                       ");
            Console.SetCursorPosition(0, 27);
            Console.WriteLine("                                                       ");
            Console.SetCursorPosition(0, 26);
            for (var i = 0; i < figure.X.Length; i++)
            {
                Console.Write(figure.X[i] + " ");
            }
            Console.SetCursorPosition(0, 27);
            for (var i = 0; i < figure.Y.Length; i++)
            {
                Console.Write(figure.Y[i] + " ");
            }
            Console.SetCursorPosition(0, 28);
            Console.Write(figure);
            Console.SetCursorPosition(0, 0);
        }
    }
}
