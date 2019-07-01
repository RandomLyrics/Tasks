using System;
using System.Collections.Generic;

namespace CML.Db
{
    public class ChessDataProvider
    {
        // persist
        private static readonly Piece[] Pieces = new Piece[]
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

        // methods
        public Piece[] GetPieces()
        {
            return Pieces;
        }
    }

    internal static class MoveToPointsSerializer
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
        // RESULT: KNIGHT move F0T7 means  must go UP one step and next can sit at UP-LEFT postion.
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
                    }
                    else
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
}
