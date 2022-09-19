enum DisplacementType
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

namespace ChessBoard
{
    public abstract class Piece                                                                                                                     //abstract class that all chessboard pieces should inherit
    {
        public string Color;
        public char Kind;
        public string Position;

        public string getColor()                                                                                                                    //returns color
        {
            return Color;
        }

        public string getPosition()                                                                                                                 //returns position
        {
            return Position;
        }

        public char getKind()                                                                                                                       //returns type
        {
            return Kind;
        }

        public abstract bool IsLegalMove(char xPos, int yPos, ChessBoard b);                                                                        //stores "YES" = 1 if the move is legal and "NO" = 0 otherwise
    }
}
