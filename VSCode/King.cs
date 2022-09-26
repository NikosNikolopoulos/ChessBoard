using System;

namespace ChessBoard
{
    public class King : Piece
    {
        public King(string c, string p, char k)
        {
            Color = c;
            Position = p;
            Kind = k;
        }

        public override bool IsLegalMove(char xPos, int yPos, ChessBoard b)
        {
            int intCurrentX = Utilities.Char2Int(Position[0]);                                                                                                      //current position
            int intCurrentY = Convert.ToInt32(Position[1]) - 48;                                                                                                    //parsing string and converting second coordinate appropriately

            int xPosInt = Utilities.Char2Int(xPos);                                                                                                                 //target position
            int yPosInt = yPos;

            Piece pieceAtTargetPosition = b.getPieceAt(xPos, yPos);

            if (Math.Abs(yPosInt - intCurrentY) != 1 && Math.Abs(intCurrentX - xPosInt) != 1)
                return false;
            if (Math.Abs(yPosInt - intCurrentY) > 1 || Math.Abs(intCurrentX - xPosInt) > 1)
                return false;
            if (yPosInt > 7 || yPosInt < 1)
                return false;
            if (pieceAtTargetPosition == null)
                if (Math.Abs(intCurrentX - xPosInt) > 1 || Math.Abs(intCurrentX - xPosInt) > 1)
                    return false;
                else
                    return true;
            if (Color == pieceAtTargetPosition.getColor())
                return false;
            return true;
        }
    }
}
