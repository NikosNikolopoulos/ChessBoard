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

        public override bool isLegalMove(char xPos, int yPos, ChessBoard b)
        {
            //current position
            int intCurrentX = Utilities.char2Int(Position[0]);
            //parsing string and converting second coordinate appropriately
            int intCurrentY = Convert.ToInt32(Position[1] - 49);

            //target position
            int xPosInt = Utilities.char2Int(xPos);
            int yPosInt = yPos - 1;

            Piece p_targ = b.getPieceAt(xPos, yPos);

            if (Math.Abs(yPosInt - intCurrentY) != 1 && Math.Abs(intCurrentX - xPosInt) != 1)
                return false;
            if (Math.Abs(yPosInt - intCurrentY) > 1 || Math.Abs(intCurrentX - xPosInt) > 1)
                return false;
            if (yPosInt > 7 || yPosInt < 1)
                return false;
            if (p_targ == null)
                if (Math.Abs(intCurrentX - xPosInt) > 1 || Math.Abs(intCurrentX - xPosInt) > 1)
                    return false;
                else
                    return true;
            if (Color == p_targ.getColor())
                return false;
            return true;
        }
    }
}
