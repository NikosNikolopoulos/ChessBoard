using System;

namespace ChessBoard
{
    public class Queen : Piece
    {
        public Queen(string c, string p, char k)
        {
            Color = c;
            Position = p;
            Kind = k;
        }

        public override bool IsLegalMove(char targetX, int targetY, ChessBoard chessboard) 
        {
            Rook dummyRook = new Rook(Color, Position, Color == "White" ? 'R' : 'r');
            Bishop dummyBishop = new Bishop(Color, Position, Color == "White" ? 'B' : 'b');

            if (!dummyRook.IsLegalMove(targetX, targetY, chessboard) && !dummyBishop.IsLegalMove(targetX, targetY, chessboard))
                return false;
            return true;
        }
    }
}
