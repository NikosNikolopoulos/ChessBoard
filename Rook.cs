using System;
using System.Windows.Markup;

enum RookDisplacementType
{
    Horizontal,
    Vertical
}

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
            char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

            char currentX = Position[0];                                                                                                                        //current position
            int currentY = Convert.ToInt32(Position[1] - 48);                                                                                                   //parsing string and converting second coordinate appropriately

            int horizontalJump = Math.Abs(currentX - targetX);
            int verticalJump = Math.Abs(currentY - targetY);

            switch (displacementType)
            {
                case (int) RookDisplacementType.Horizontal:
                    for (int radius = 1; radius < horizontalJump; radius++)
                    {
                        if (targetX > currentX)
                        {
                            if (chessboard.getPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) + radius), currentY) != null)
                                return true;
                        }
                        else if (targetX < currentX)
                            if (chessboard.getPieceAt(Utilities.Int2Char(Utilities.Char2Int(currentX) - radius), currentY) != null)
                                return true;
                    }
                    break;
                case (int)RookDisplacementType.Vertical:
                    for (int radius = 1; radius < verticalJump; radius++)
                    {
                        if (targetY > currentY)
                        {
                            if (chessboard.getPieceAt(Position[0], currentY + radius) != null)
                                return true;
                        }
                        else if (targetY < currentY)
                            if (chessboard.getPieceAt(Position[0], currentY - radius) != null)
                                return true;
                    }
                    break;
            }
            return false;
        }

        public override bool IsLegalMove(char targetX, int targetY, ChessBoard chessboard)
        {
            char currentX = Position[0];                                                                                                                         //current position
            int currentY = Convert.ToInt32(Position[1] - 48);                                                                                                    //parsing string and converting second coordinate appropriately

            Piece pieceAtTargetPosition = chessboard.getPieceAt(targetX, targetY);

            if ((targetX != currentX && targetY != currentY) || (targetX == currentX && targetY == currentY))
                return false;
            if (IsPieceInBetween(targetX, targetY,targetX == currentX ? (int) RookDisplacementType.Vertical : (int)RookDisplacementType.Horizontal, chessboard))
                return false;
            if (pieceAtTargetPosition == null)
                return true;
            if (pieceAtTargetPosition.Color == Color)
                return false;
            return true;
        }
    }
}
