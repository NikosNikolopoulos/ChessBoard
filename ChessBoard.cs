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
    //ilustrates a chess Board
    public class ChessBoard
    {
        private Dictionary<string, Piece> Board = new Dictionary<string, Piece>();

        public void placePieceAt(Piece p, char xPos, int yPos)
        {
            Board[xPos + "" + yPos] = p;
        }

        public Piece getPieceAt(char xPos, int yPos)
        {
            return Board[xPos + "" + yPos];
        }

        public void movePieceAt(char xOrig, int yOrig, char xDest, int yDest)
        {
            Piece movingPiece = Board[xOrig + "" + yOrig];
            movingPiece.Position = xDest + "" + yDest;

            Board[xOrig + "" + yOrig] = null;
            Board[xDest + "" + yDest] = movingPiece;
        }

        //responsible for initialising the chess Board with every piece in position
        public void initialiseBoard()
        {
            char[] Letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

            foreach (int Number in Numbers)
            {
                foreach (char Letter in Letters)
                {
                    if (Number == 1)
                        if (Letter == 'A' || Letter == 'H')
                            Board.Add(Letter + "" + Number, new Rook("White", Letter + "" + Number, 'R'));
                        else if (Letter == 'B' || Letter == 'G')
                            Board.Add(Letter + "" + Number, new Knight("White", Letter + "" + Number, 'H'));
                        else if (Letter == 'C' || Letter == 'F')
                            Board.Add(Letter + "" + Number, new Bishop("White", Letter + "" + Number, 'B'));
                        else if (Letter == 'D')
                            Board.Add(Letter + "" + Number, new Queen("White", Letter + "" + Number, 'Q'));
                        else
                            Board.Add(Letter + "" + Number, new King("White", Letter + "" + Number, 'K'));
                    else if(Number == 2)
                        Board.Add(Letter + "" + Number, new Pawn("White", Letter + "" + Number,'P'));
                    else if(Number == 7)
                        Board.Add(Letter + "" + Number, new Pawn("Black", Letter + "" + Number, 'p'));
                    else if(Number == 8)
                        if (Letter == 'A' || Letter == 'H')
                            Board.Add(Letter + "" + Number, new Rook("Black", Letter + "" + Number, 'r'));
                        else if (Letter == 'B' || Letter == 'G')
                            Board.Add(Letter + "" + Number, new Knight("Black", Letter + "" + Number, 'h'));
                        else if (Letter == 'C' || Letter == 'F')
                            Board.Add(Letter + "" + Number, new Bishop("Black", Letter + "" + Number, 'b'));
                        else if (Letter == 'D')
                            Board.Add(Letter + "" + Number, new Queen("Black", Letter + "" + Number, 'q'));
                        else
                            Board.Add(Letter + "" + Number, new Knight("Black", Letter + "" + Number, 'k'));
                }
            }

            //fill the rest of the slots with null objects
            string[] Coordinates = Enum.GetNames(typeof(BoardCoordinates));

            foreach (string Coordinate in Coordinates)
                Board.TryAdd(Coordinate, null);
        }

        public void printBoard()
        {
            char[] Letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

            Console.WriteLine("     A   B   C   D   E   F   G   H\n    ___ ___ ___ ___ ___ ___ ___ ___");
            foreach (int Number in Numbers)
            {
                foreach (char Letter in Letters)
                {
                    if (Letter == 'A')
                        Console.Write(Number + "  ");

                    if (Board[Letter + "" + Number] != null)
                        Console.Write("|_" + Board[Letter + "" + Number].getKind() + "_");
                    else
                        Console.Write("|___");

                    if (Letter == 'H')
                        Console.WriteLine("|  " + Number);
                }
            }
            Console.WriteLine("\n     A   B   C   D   E   F   G   H");
        }
    }
}
