using System;

namespace ChessBoard
{
    public class Pawn : Piece
    {
        public Pawn(string c, string p, char k)
        {
            Color = c;
            Position = p;
            Kind = k;
        }

        public override bool IsLegalMove(char targetX, int targetY, ChessBoard chessboard)
        {
            char currentX = Position[0];                                                                                                                                //current position
            int currentY = Convert.ToInt32(Position[1] - 48);                                                                                                           //parsing string and converting second coordinate appropriately

            Piece pieceAtTargetPosition = chessboard.getPieceAt(targetX, targetY);

            if (Color == "Black" && currentY == 7 && Math.Abs(targetY - currentY) == 2)                                                                                 //allow for starting position "jump" for white pawns but not over other pawns!!
                if (pieceAtTargetPosition == null && chessboard.getPieceAt(targetX, targetY + 1) == null)
                    return true;
                else
                    return false;
            if (Color == "White" && currentY == 2 && Math.Abs(targetY - currentY) == 2)                                                                                 //allow for starting position "jump" for black pawns but not over other pawns!!
                if (pieceAtTargetPosition == null && chessboard.getPieceAt(targetX, targetY - 1) == null)
                    return true;
                else
                    return false;
            if (Math.Abs(targetY - currentY) != 1)                                                                                                                      //there should always be a unitary move in the vertical direction
                return false;
            if (targetY > 8 || targetY < 0)                                                                                                                             //out of bounds
                return false;
            if (Color == "Black" && targetY - currentY >= 0)                                                                                                            //filter illegal moves towards the wrong direction
                return false;
            if (Color == "White" && targetY - currentY <= 0)
                return false;
            if (pieceAtTargetPosition == null)
                if (Math.Abs(currentX - targetX) != 0)                                                                                                                  //no horizontal movement allowed, diagonal movement allowed only for !=null target
                    return false;
                else
                    return true;
            if (Math.Abs(currentX - targetX) != 1 || Color == pieceAtTargetPosition.getColor())                                                                         //not allow horizontal jumps > 1 or movements to already occupied cells of the same color
                return false;
            return true;
        }
    }
}
