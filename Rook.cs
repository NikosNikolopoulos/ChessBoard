using System;

namespace ChessBoard
{
    public class Rook : Piece
    {
        public Rook(string c, string p, char k)
        {
            Color = c;
            Position = p;
            Kind = k;
        }

        public bool IsPieceInBetween(char targetX, int targetY, int displacementType, ChessBoard chessboard)
        {
            char currentX = Position[0];                                                                                                                        //current position
            int currentY = Convert.ToInt32(Position[1] - 48);                                                                                                   //parsing string and converting second coordinate appropriately

            int jump = targetY==currentY ? Math.Abs(currentX - targetX) : Math.Abs(currentY - targetY);

            for (int radius = 1; radius < jump; radius++)
            {
                switch (displacementType)
                {
                    case (int)DisplacementType.East:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) + radius), currentY) != null)
                            return true;
                        break;
                    case (int)DisplacementType.West:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) - radius), currentY) != null)
                            return true;
                        break;
                    case (int)DisplacementType.North:
                        if (chessboard.GetPieceAt(Position[0], currentY + radius) != null)
                            return true;
                        break;
                    case (int)DisplacementType.South:
                        if (chessboard.GetPieceAt(Position[0], currentY - radius) != null)
                            return true;
                        break;
                }
            }
            return false;
        }

        public override bool IsLegalMove(char targetX, int targetY, ChessBoard chessboard)
        {
            char currentX = Position[0];                                                                                                                         //current position
            int currentY = Convert.ToInt32(Position[1] - 48);                                                                                                    //parsing string and converting second coordinate appropriately

            Piece pieceAtTargetPosition = chessboard.GetPieceAt(targetX, targetY);

            if ((targetX != currentX && targetY != currentY) || (targetX == currentX && targetY == currentY))
                return false;
            if (targetY == currentY)
                if (IsPieceInBetween(targetX, targetY,targetX > currentX ? (int) DisplacementType.East : (int) DisplacementType.West, chessboard))
                    return false;
            if (targetX == currentX)
                if (IsPieceInBetween(targetX, targetY, targetY > currentY ? (int)DisplacementType.North : (int)DisplacementType.South, chessboard))
                    return false;
            if (pieceAtTargetPosition == null)
                return true;
            if (pieceAtTargetPosition.Color == Color)
                return false;
            return true;
        }
    }
}
