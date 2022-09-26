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

        public override bool IsLegalMove(char targetX, int targetY, ChessBoard chessboard)
        {
            //current position
            char currentX = Position[0];
            //parsing string and converting second coordinate appropriately
            int currentY = Convert.ToInt32(Position[1]) - 48;

            Piece pieceAtTargetPosition = chessboard.getPieceAt(targetX, targetY);

            if (pieceAtTargetPosition != null)
                if (pieceAtTargetPosition.Color == Color)
                    return false;
            if (Math.Abs(currentX - targetX) == 1 && Math.Abs(currentY - targetY) == 2)
                return true;
            if (Math.Abs(currentX - targetX) == 2 && Math.Abs(currentY - targetY) == 1)
                return true;
            return false;
        }
    }
}
