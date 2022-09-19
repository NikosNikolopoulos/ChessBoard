using System;
using System.Collections.Generic;

public enum Letters
{
    A = 'A',
    B = 'B',
    C = 'C',
    D = 'D',
    E = 'E',
    F = 'F',
    G = 'G',
    H = 'H'
}

namespace ChessBoard
{
    public class ChessBoard                                                                                                                                     //illustrates a chess Board
    {
        private readonly Dictionary<string, Piece> _board = new Dictionary<string, Piece>();

        public void PlacePieceAt(Piece p, char xPos, int yPos)
        {
            _board[xPos + "" + yPos] = p;
        }

        public Piece GetPieceAt(char xPos, int yPos)
        {
            return _board[xPos + "" + yPos];
        }

        public void MovePieceAt(char xOrig, int yOrig, char xDest, int yDest)
        {
            Piece movingPiece = _board[xOrig + "" + yOrig];
            movingPiece.Position = xDest + "" + yDest;

            _board[xOrig + "" + yOrig] = null;
            _board[xDest + "" + yDest] = movingPiece;
        }

        public void InitialiseBoard()                                                                                                                           //responsible for initializing the chess Board with every piece in position
        {
            for (int number = 1; number <=8; number++)
            {
                foreach (Letters letter in Enum.GetValues(typeof(Letters)))
                {
                    if (number == 1 || number == 8)
                        if ((char) letter == 'A' || (char) letter == 'H')
                            _board.Add(letter + "" + number,
                                new Rook(number == 1 ? "White" : "Black",
                                    letter + "" + number,
                                    number == 1 ? 'R' : 'r'));
                        else if ((char) letter == 'B' || (char) letter == 'G')
                            _board.Add(letter + "" + number,
                                new Knight(number == 1 ? "White" : "Black",
                                    letter + "" + number,
                                    number == 1 ? 'H' : 'h'));
                        else if ((char) letter == 'C' || (char) letter == 'F')
                            _board.Add(letter + "" + number,
                                new Bishop(number == 1 ? "White" : "Black",
                                    letter + "" + number,
                                    number == 1 ? 'B' : 'b'));
                        else if ((char) letter == 'D')
                            _board.Add(letter + "" + number,
                                new Queen(number == 1 ? "White" : "Black",
                                    letter + "" + number,
                                    number == 1 ? 'Q' : 'q'));
                        else
                            _board.Add(letter + "" + number,
                                new King(number == 1 ? "White" : "Black",
                                    letter + "" + number,
                                    number == 1 ? 'K' : 'k'));
                    else if (number == 2 || number == 7)
                        _board.Add(letter + "" + number,
                            new Pawn(number == 2 ? "White" : "Black",
                                letter + "" + number, number == 2 ? 'P' : 'p'));
                    else
                        _board.Add(letter + "" + number, null);
                }
            }
        }

        public void PrintBoard()
        {
            ApplicationMessage.PrintMessage(Messages.ColumnsTop);
            for (int number = 1; number <= 8; number++)
            {
                foreach (Letters letter in Enum.GetValues(typeof(Letters)))
                {
                    if ((char) letter == 'A')
                        ApplicationMessage.PrintMessage(Messages.RowsLeft,null,number);

                    if (_board[letter + "" + number] != null)
                        ApplicationMessage.PrintMessage(Messages.PieceInCell,null,0, _board[letter + "" + number].getKind());
                    else
                        ApplicationMessage.PrintMessage(Messages.EmptyCell);

                    if ((char) letter == 'H')
                        ApplicationMessage.PrintMessage(Messages.RowsRight,null,number);
                }
            }
            ApplicationMessage.PrintMessage(Messages.ColumnsBot);
        }
    }
}
