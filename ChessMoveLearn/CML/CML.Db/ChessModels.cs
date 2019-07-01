using System;
using System.Collections.Generic;
using System.Text;

namespace CML.Db
{
    class ChessModels
    {
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

    public enum PieceType
    {
        Pawn,         Rook,         Knight,         Bishop,         Queen,         King
    }

    public class Piece
    {
        public PieceType Type { get; set; }
        public Point[] MoveToPoints { get; set; }
    }

    
    
}
