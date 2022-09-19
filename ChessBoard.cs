using System;
using System.Collections.Generic;

public enum BoardCoordinates
{
    A1, A2, A3, A4, A5, A6, A7, A8,    // 0,..., 7,
    B1, B2, B3, B4, B5, B6, B7, B8,    // 8,...,15,
    C1, C2, C3, C4, C5, C6, C7, C8,    //16,...,23,
    D1, D2, D3, D4, D5, D6, D7, D8,    //24,...,31,
    E1, E2, E3, E4, E5, E6, E7, E8,    //32,...,39,
    F1, F2, F3, F4, F5, F6, F7, F8,    //40,...,47,
    G1, G2, G3, G4, G5, G6, G7, G8,    //48,...,55,
    H1, H2, H3, H4, H5, H6, H7, H8     //56,...,63
}

namespace ChessBoard
{
    public class ChessBoard                                                                                                                                     //illustrates a chess Board
    {
        private Dictionary<string, Piece> _board = new Dictionary<string, Piece>();

        public void placePieceAt(Piece p, char xPos, int yPos)
        {
            _board[xPos + "" + yPos] = p;
        }

        public Piece getPieceAt(char xPos, int yPos)
        {
            return _board[xPos + "" + yPos];
        }

        public void movePieceAt(char xOrig, int yOrig, char xDest, int yDest)
        {
            Piece movingPiece = _board[xOrig + "" + yOrig];
            movingPiece.Position = xDest + "" + yDest;

            _board[xOrig + "" + yOrig] = null;
            _board[xDest + "" + yDest] = movingPiece;
        }

        public void initialiseBoard()                                                                                                                           //responsible for initializing the chess Board with every piece in position
        {
            char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

            foreach (int number in numbers)
            {
                foreach (char letter in letters)
                {
                    if (number == 1 || number == 8)
                        if (letter == 'A' || letter == 'H')
                            _board.Add(letter + "" + number, new Rook(number == 1 ? "White":"Black", letter + "" + number, number == 1 ? 'R':'r'));
                        else if (letter == 'B' || letter == 'G')
                            _board.Add(letter + "" + number, new Knight(number == 1 ? "White" : "Black", letter + "" + number, number == 1 ? 'H' : 'h'));
                        else if (letter == 'C' || letter == 'F')
                            _board.Add(letter + "" + number, new Bishop(number == 1 ? "White" : "Black", letter + "" + number, number == 1 ? 'B' : 'b'));
                        else if (letter == 'D')
                            _board.Add(letter + "" + number, new Queen(number == 1 ? "White" : "Black", letter + "" + number, number == 1 ? 'Q' : 'q'));
                        else
                            _board.Add(letter + "" + number, new King(number == 1 ? "White" : "Black", letter + "" + number, number == 1 ? 'K' : 'k'));
                    else if (number == 2 || number == 7)
                        _board.Add(letter + "" + number,
                            new Pawn(number == 2 ? "White" : "Black", letter + "" + number, number == 2 ? 'P' : 'p'));
                    else
                        _board.Add(letter + "" + number, null);
                }
            }
        }

        public void printBoard()
        {
            char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

            Console.WriteLine("     A   B   C   D   E   F   G   H\n" +
                              "    ___ ___ ___ ___ ___ ___ ___ ___");
            foreach (int number in numbers)
            {
                foreach (char letter in letters)
                {
                    if (letter == 'A')
                        Console.Write(number + "  ");

                    if (_board[letter + "" + number] != null)
                        Console.Write("|_" + _board[letter + "" + number].getKind() + "_");
                    else
                        Console.Write("|___");

                    if (letter == 'H')
                        Console.WriteLine("|  " + number);
                }
            }
            Console.WriteLine("\n     A   B   C   D   E   F   G   H");
        }
    }
}
