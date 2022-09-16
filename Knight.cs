using System;

namespace ChessBoard
{
    public class Knight : Piece
    {
        public Knight(string c, string p, char k)
        {
            Color = c;
            Position = p;
            Kind = k;
        }

        public override bool isLegalMove(char TargetX, int TargetY, ChessBoard chessboard)
        {
            //current position
            char CurrentX = Position[0];
            //parsing string and converting second coordinate appropriately
            int CurrentY = Convert.ToInt32(Position[1] - 48);

            Piece pieceAtTargetPosition = chessboard.getPieceAt(TargetX, TargetY);

            if (Math.Abs(CurrentX - TargetX) == 1 && Math.Abs(CurrentY - TargetY) == 2)
                return true;
            if (pieceAtTargetPosition != null)
                if (pieceAtTargetPosition.Color == Color)
                    return false;
            return false;
        }
    }
}
