using CML.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CML
{
   
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
            var data = new ChessDataProvider();
            var cs = new ChessService(data);

            var valid = cs.IsMoveValid(PieceType.Pawn, null, new Point(0, -1));
            var valid2 = cs.IsMoveValid(PieceType.Pawn, null, new Point(0, 1));
            var valid3 = cs.IsMoveValid(PieceType.Pawn, null, null);


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

            //foreach (var p in pcs)
            //{
                
            //    foreach (var item in p.MoveToPoints)
            //    {
            //        var x = (4 + item.X);
            //        var y = (4 + item.Y);
            //        if (x.InRange(0,7) && y.InRange(0, 7))
            //        {
            //            board[x, y].Value = 1;
            //        }
            //    }
            //    Console.WriteLine(p.Type.ToString().ToUpper());
            //    Console.WriteLine(RenderDim(board));
            //    resetBoard(0);
            //}
            
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
