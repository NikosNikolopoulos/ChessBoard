using System;
using System.Collections.Generic;

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
                            Board.Add(Letter + "" + Number, new Pawn("White", Letter + "" + Number, 'R'));
                        else if (Letter == 'B' || Letter == 'G')
                            Board.Add(Letter + "" + Number, new Pawn("White", Letter + "" + Number, 'H'));
                        else if (Letter == 'C' || Letter == 'F')
                            Board.Add(Letter + "" + Number, new Pawn("White", Letter + "" + Number, 'B'));
                        else if (Letter == 'D')
                            Board.Add(Letter + "" + Number, new Pawn("White", Letter + "" + Number, 'Q'));
                        else
                            Board.Add(Letter + "" + Number, new Pawn("White", Letter + "" + Number, 'K'));
                    else if(Number == 2)
                        Board.Add(Letter + "" + Number, new Pawn("White", Letter + "" + Number,'P'));
                    else if(Number == 7)
                        Board.Add(Letter + "" + Number, new Pawn("Black", Letter + "" + Number, 'p'));
                    else if(Number == 8)
                        if (Letter == 'A' || Letter == 'H')
                            Board.Add(Letter + "" + Number, new Pawn("Black", Letter + "" + Number, 'r'));
                        else if (Letter == 'B' || Letter == 'G')
                            Board.Add(Letter + "" + Number, new Pawn("Black", Letter + "" + Number, 'h'));
                        else if (Letter == 'C' || Letter == 'F')
                            Board.Add(Letter + "" + Number, new Pawn("Black", Letter + "" + Number, 'b'));
                        else if (Letter == 'D')
                            Board.Add(Letter + "" + Number, new Pawn("Black", Letter + "" + Number, 'q'));
                        else
                            Board.Add(Letter + "" + Number, new Pawn("Black", Letter + "" + Number, 'k'));
                }
            }

            //fill the rest of the slots with null object
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
