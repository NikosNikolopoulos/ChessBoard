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

        public bool isPieceInbetween(char TargetX, int TargetY, int DisplacementType, ChessBoard chessboard)
        {
            char[] Letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

            //current position
            char CurrentX = Position[0];
            //parsing string and converting second coordinate appropriately
            int CurrentY = Convert.ToInt32(Position[1] - 48);

            int HorizontalJump = Math.Abs(CurrentX - TargetX);
            int VerticalJump = Math.Abs(CurrentX - TargetY);

            switch (DisplacementType)
            {
                case (int) RookDisplacementType.Horizontal:
                    for (int radius = 1; radius<HorizontalJump; radius++)
                    {
                        if (TargetX > CurrentX)
                            chessboard.getPieceAt(Utilities.int2Char(CurrentX + radius), CurrentY);
                    }
                    break;
            }

            return true;
        }

        public override bool isLegalMove(char TargetX, int TargetY, ChessBoard chessboard)
        {
            //current position
            char CurrentX = Position[0];
            //parsing string and converting second coordinate appropriately
            int CurrentY = Convert.ToInt32(Position[1] - 48);

            Piece pieceAtTargetPosition = chessboard.getPieceAt(TargetX, TargetY);

            if ((TargetX != CurrentX && TargetY != CurrentY) || (TargetX == CurrentX && TargetY == CurrentY))
                return false;
            return true;
        }
    }
}
