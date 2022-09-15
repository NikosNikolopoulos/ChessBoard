namespace ChessBoard
{
    //abstract class that all chessboard pieces should inherit
    public abstract class Piece
    {
        public string Color;
        public char Kind;
        public string Position;

        //returns color
        public string getColor()
        {
            return Color;
        }

        //returns position
        public string getPosition()
        {
            return Position;
        }

        //returns type
        public char getKind()
        {
            return Kind;
        }

        //stores "YES"=1 if the move is legal and "NO"=0 otherwise
        public abstract bool isLegalMove(char xPos, int yPos, ChessBoard b);
    }
}
