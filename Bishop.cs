namespace ChessBoard
{
    public class Bishop : Piece
    {
        public Bishop(string c, string p, char k)
        {
            Color = c;
            Position = p;
            Kind = k;
        }

        public override bool isLegalMove(char xPos, int yPos, ChessBoard b)
        {
            return true;
        }
    }
}
