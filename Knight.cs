namespace ChessBoard
{
    public class Knight : Piece
    {
        public Knight(string c, string p, char k)
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
