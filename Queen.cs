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
                    case (int)DisplacementType.North:
                        if (chessboard.GetPieceAt(currentX, currentY + radius) != null)
                            return true;
                        break;
                    case (int)DisplacementType.East:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) + radius), currentY) != null)
                            return true;
                        break;
                    case (int)DisplacementType.South:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) - radius), currentY) != null)
                            return true;
                        break;
                    case (int)DisplacementType.West:
                        if (chessboard.GetPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) - radius), currentY) != null)
                            return true;
                        break;
                }
            }
            return false;
        }

        public override bool IsLegalMove(char targetX, int targetY, ChessBoard chessboard) //  TODO DEBUG!!!!
        {
            char currentX = Position[0];                                                                                                                         //current position
            int currentY = Convert.ToInt32(Position[1] - 48);                                                                                                    //parsing string and converting second coordinate appropriately

            Rook dummyRook = new Rook(Color, Position, Color == "White" ? 'R' : 'r');
            Bishop dummyBishop = new Bishop(Color, Position, Color == "White" ? 'B' : 'b');

            Piece pieceAtTargetPosition = chessboard.GetPieceAt(targetX, targetY);
            if (!dummyRook.IsLegalMove(targetX, targetY, chessboard) && !dummyBishop.IsLegalMove(targetX, targetY, chessboard))
                return false;
            //if (targetY > currentY)
            //{
            //    if (IsPieceInBetween(targetX, targetY, (int)DisplacementType.North, chessboard))
            //        return false;
            //    if (IsPieceInBetween(targetX, targetY,
            //            targetX > currentX
            //                ? (int)DisplacementType.NorthEast
            //                : (int)DisplacementType.NorthWest, chessboard))
            //        return false;
            //}
            //if (targetY == currentY)
            //{
            //    if (IsPieceInBetween(targetX, targetY, (int)DisplacementType.East, chessboard))
            //        return false;
            //    if (IsPieceInBetween(targetX, targetY, (int)DisplacementType.West, chessboard))
            //        return false;
            //}
            //if (targetY < currentY)
            //{
            //    if (IsPieceInBetween(targetX, targetY, (int)DisplacementType.South, chessboard))
            //        return false;
            //    if (IsPieceInBetween(targetX, targetY,
            //            targetX > currentX
            //                ? (int)DisplacementType.SouthEast
            //                : (int)DisplacementType.SouthWest, chessboard))
            //        return false;
            //}
            //if (pieceAtTargetPosition.Color == Color)
            //    return false;
            return true;
        }
    }
}
