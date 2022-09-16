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

        public override bool isLegalMove(char TargetX, int TargetY, ChessBoard chessboard)
        {
            //current position
            char CurrentX = Position[0];
            //parsing string and converting second coordinate appropriately
            int CurrentY = Convert.ToInt32(Position[1] - 48);

            Piece pieceAtTargetPosition = chessboard.getPieceAt(TargetX, TargetY);

            //allow for starting positon "jump" for white pawns but not over other pawns!!
            if (Color == "Black" && CurrentY == 7 && Math.Abs(TargetY - CurrentY) == 2)
                if (pieceAtTargetPosition == null && chessboard.getPieceAt(TargetX, TargetY + 1) == null)
                    return true;
                else
                    return false;
            //allow for starting positon "jump" for black pawns but not over other pawns!!
            if (Color == "White" && CurrentY == 2 && Math.Abs(TargetY - CurrentY) == 2)
                if (pieceAtTargetPosition == null && chessboard.getPieceAt(TargetX, TargetY - 1) == null)
                    return true;
                else
                    return false;
            //there should always be a unitary move in the vertical direction
            if (Math.Abs(TargetY - CurrentY) != 1)
                return false;
            //out of bounds
            if (TargetY > 8 || TargetY < 0)
                return false;
            //filter illegal moves towards the wrong direction
            if (Color == "Black" && TargetY - CurrentY >= 0)
                return false;
            if (Color == "White" && TargetY - CurrentY <= 0)
                return false;
            if (pieceAtTargetPosition == null)
                //no horizontal movement allowed, diagonal movement allowed only for !=null target
                if (Math.Abs(CurrentX - TargetX) != 0)
                    return false;
                else
                    return true;
            //not allow horizontal jumps > 1 or movements to already occupied cells of the same color
            if (Math.Abs(CurrentX - TargetX) != 1 || Color == pieceAtTargetPosition.getColor())
                return false;
            return true;
        }
    }
}
