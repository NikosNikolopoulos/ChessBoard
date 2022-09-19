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

        public bool IsPieceInBetween(char targetX, int targetY, int displacementType, ChessBoard chessboard)
        {
            char currentX = Position[0];                                                                                                                        //current position
            int currentY = Convert.ToInt32(Position[1] - 48);                                                                                                   //parsing string and converting second coordinate appropriately

            int jump = Math.Abs(currentX - targetX);

            for (int radius = 1; radius < jump; radius++)
            {
                switch (displacementType)
                {
                    case (int)DisplacementType.NorthEast:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) + radius), currentY + radius) != null)
                            return true;
                        break;
                    case (int)DisplacementType.NorthWest:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) + radius), currentY - radius) != null)
                            return true;
                        break;
                    case (int)DisplacementType.SouthEast:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) - radius), currentY + radius) != null)
                            return true;
                        break;
                    case (int)DisplacementType.SouthWest:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) - radius), currentY - radius) != null)
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

            if (Math.Abs(currentX - targetX) != Math.Abs(currentY - targetY) || Math.Abs(currentX - targetX) == 0)
                return false;
            if (targetX > currentX)
                if (IsPieceInBetween(targetX, targetY,
                        targetY > currentY
                            ? (int)DisplacementType.NorthEast
                            : (int)DisplacementType.NorthWest, chessboard))
                    return false;
            if (targetX < currentX)
                if (IsPieceInBetween(targetX, targetY,
                        targetY > currentY
                            ? (int)DisplacementType.SouthEast
                            : (int)DisplacementType.SouthWest, chessboard))
                    return false;
            if (pieceAtTargetPosition == null)
                return true;
            if (pieceAtTargetPosition.Color == Color)
                return false;
            return true;
        }
    }
}
