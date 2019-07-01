using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CML
{
    public enum PieceType
    {
        Pawn,         Rook,         Knight,         Bishop,         Queen,         King
    }

    public class Piece
    {
        public PieceType Type { get; set; }
        public Point[] MoveToPoints { get; set; }
    }
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public static class MoveToPointsSerializer
    {
        // Directions
        // 7 0 1
        // 6 x 2
        // 5 4 3
        //
        // Format
        //
        // KNIGHT
        // F0T7;F0T1;...
        // F means starting sequence of false/hidden moves
        // T means starting seqeunce of true moves
        // 0,7,... means direction
        // RESULT: KNIGHT move F0T7 means  must go UP one step and next can sit at UP-LEFT postion. siplafied it from: 2 steps UP and 1 step LEFT
        // RESULT: ROOK move T00..0 means can sit on each step going UP
        //
        private static Tuple<int, int> CharToCord(char c)
        {
            switch (c)
            {
                case '0': return Tuple.Create(0, -1);
                case '1': return Tuple.Create(1, -1);
                case '2': return Tuple.Create(1, 0);
                case '3': return Tuple.Create(1, 1);
                case '4': return Tuple.Create(0, 1);
                case '5': return Tuple.Create(-1, 1);
                case '6': return Tuple.Create(-1, 0);
                case '7': return Tuple.Create(-1, -1);

                default: return null;
            }
        }
        public static Point[] FromString(string code)
        {
            var moveToPoints = new List<Point>();
            
            foreach (var s in code.Split(';'))
            {
                var fm = false; //false/hidden moves
                var tm = false; //true moves
                var x = 0;
                var y = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    var c = s[i];
                    if (c == 'F')
                    {
                        fm = true;
                        tm = false;
                        continue;
                    } else 
                    if (c == 'T')
                    {
                        fm = false;
                        tm = true;
                        continue;
                    }

                    var cord = CharToCord(c);
                    if (cord == null) continue; //should never be null
                    x = x + cord.Item1;
                    y = y + cord.Item2;

                    if (tm)
                    {
                        moveToPoints.Add(new Point(x, y));
                    }
                }
            }

            return moveToPoints.ToArray();
        }
    }
    //public class Knight
    //{
    //    public Move[] Moves { get; set; } = new[] { new Move{ X = 2, Y = 2 } };
    //}
    public class Tile
    {
        public int Value { get; set; }
        public Tile()
        {
            Value = 0;
        }
    }

    public static class InputExtensions
    {
        public static int LimitToRange(this int v, int iMin, int iMax)
        {
            if (v < iMin) { return iMin; }
            if (v > iMax) { return iMax; }
            return v;
        }
        public static bool InRange(this int v, int iMin, int iMax)
        {
            return v >= iMin && v <= iMax;
        }
    }

    class Program
    {

        

        static void Main(string[] args)
        {
            // Directions
            // 7 0 1
            // 6 x 2
            // 5 4 3
            
            var pcs = new Piece[]
            {
                new Piece
                {
                    Type = PieceType.Pawn,
                    MoveToPoints = MoveToPointsSerializer.FromString("T4")
                },
                new Piece
                {
                    Type = PieceType.Rook,
                    MoveToPoints = MoveToPointsSerializer.FromString("T0000000;T2222222;T4444444;T6666666")
                },
                new Piece
                {
                    Type = PieceType.Knight,
                    MoveToPoints = MoveToPointsSerializer.FromString("F0T7;F0T1;F2T1;F2T3;F4T3;F4T5;F6T7;F6T5")
                },
                new Piece
                {
                    Type = PieceType.Bishop,
                    MoveToPoints = MoveToPointsSerializer.FromString("T1111111;T3333333;T5555555;T7777777")
                },
                new Piece
                {
                    Type = PieceType.Queen,
                    MoveToPoints = MoveToPointsSerializer.FromString("T1111111;T3333333;T5555555;T7777777;T0000000;T2222222;T4444444;T6666666")
                },
                new Piece
                {
                    Type = PieceType.King,
                    MoveToPoints = MoveToPointsSerializer.FromString("T1;T3;T5;T7;T0;T2;T4;T6")
                },
            };

            
            //var p = new Point(0, 0);
            var board = new Tile[8, 8];
            for (int y = 0; y < 8; y++) for (int x = 0; x < 8; x++)
                {
                    board[x, y] = new Tile();
                }
            Action<int> resetBoard = (int val) =>
            {
                for (int y = 0; y < 8; y++) for (int x = 0; x < 8; x++)
                    {
                        board[x, y].Value = val;
                    }
                board[4, 4].Value = 5;
            };
            resetBoard(0);

            //var ks = new[,] { { -2, 1 }, { -2, -1 } };
            //
            //st.Value = 5;

            Console.WriteLine("CLEAR");
            Console.WriteLine(RenderDim(board));

            foreach (var p in pcs)
            {
                
                foreach (var item in p.MoveToPoints)
                {
                    var x = (4 + item.X);
                    var y = (4 + item.Y);
                    if (x.InRange(0,7) && y.InRange(0, 7))
                    {
                        board[x, y].Value = 1;
                    }
                }
                Console.WriteLine(p.Type.ToString().ToUpper());
                Console.WriteLine(RenderDim(board));
                resetBoard(0);
            }
            
            //board[].Value = 2;
            //MoveK(board, ks);
            //board[2, 4].Value = 1;
            


            Console.ReadKey();
        }

        private static void MoveK(int[,] board, int[] moves, Tile sPos )
        {
            foreach (var x in moves)
            {
                //var c = board
            }
            board[4 + 1, 4 + 2] = 1;
        }

        private static string RenderDim(Tile[,] board)
        {
            var r = "";

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    r = r + " " + board[x, y].Value + " ";
                }
                r = r + "\n";
            }
            r += "\n";

            return r;
        }
    }
}
