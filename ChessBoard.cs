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
            //Create 8 x White Pawns in their spawning positions
            Board.Add("A2", new Pawn("White", "A2", 'P'));
            Board.Add("B2", new Pawn("White", "B2", 'P'));
            Board.Add("C2", new Pawn("White", "C2", 'P'));
            Board.Add("D2", new Pawn("White", "D2", 'P'));
            Board.Add("E2", new Pawn("White", "E2", 'P'));
            Board.Add("F2", new Pawn("White", "F2", 'P'));
            Board.Add("G2", new Pawn("White", "G2", 'P'));
            Board.Add("H2", new Pawn("White", "H2", 'P'));

            //Create 2 x Black Rooks in their spawning positions
            Board.Add("A1", new Rook("White", "A1", 'R'));
            Board.Add("H1", new Rook("White", "H1", 'R'));

            //Create 2 x Black Knights in their spawning positions
            Board.Add("B1", new Knight("White", "B1", 'H'));
            Board.Add("G1", new Knight("White", "G1", 'H'));

            //Create 2 x Black Bishops in their spawning positions
            Board.Add("C1", new Bishop("White", "C1", 'B'));
            Board.Add("F1", new Bishop("White", "F1", 'B'));

            //Create 1 x Black King in his spawning position
            Board.Add("E1", new King("White", "E1", 'K'));

            //Create 1 x Black Queen in her spawning position
            Board.Add("D1", new Queen("White", "D1", 'Q'));

            /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

            //Create 8 x Black Pawns in their spawning positions
            Board.Add("A7", new Pawn("Black", "A7", 'p'));
            Board.Add("B7", new Pawn("Black", "B7", 'p'));
            Board.Add("C7", new Pawn("Black", "C7", 'p'));
            Board.Add("D7", new Pawn("Black", "D7", 'p'));

            Board.Add("E7", new Pawn("Black", "E7", 'p'));
            Board.Add("F7", new Pawn("Black", "F7", 'p'));
            Board.Add("G7", new Pawn("Black", "G7", 'p'));
            Board.Add("H7", new Pawn("Black", "H7", 'p'));

            //Create 2 x White Rooks in their spawning positions
            Board.Add("A8", new Rook("Black", "A8", 'r'));
            Board.Add("H8", new Rook("Black", "H8", 'r'));

            //Create 2 x White Knights in their spawning positions
            Board.Add("B8", new Knight("Black", "B8", 'h'));
            Board.Add("G8", new Knight("Black", "G8", 'h'));

            //Create 2 x White Bishops in their spawning positions
            Board.Add("C8", new Bishop("Black", "C8", 'b'));
            Board.Add("F8", new Bishop("Black", "F8", 'b'));

            //Create 1 x White King in his spawning position
            Board.Add("E8", new King("Black", "E8", 'k'));

            //Create 1 x White Queen in her spawning position
            Board.Add("D8", new Queen("Black", "D8", 'q'));

            //fill the rest of the slots with null object
            string[] Coordinates = Enum.GetNames(typeof(BoardCoordinates));

            foreach (string Coordinate in Coordinates)
                Board.TryAdd(Coordinate, null);
        }

        public void printBoard()
        {
            char[] Letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            int[] Numbers = { 1, 2, 3, 4, 5, 6, 7, 8 };

            Console.WriteLine("   A   B   C   D   E   F   G   H");
            Console.WriteLine("  ___ ___ ___ ___ ___ ___ ___ ___");
            foreach (int Number in Numbers)
            {
                foreach (char Letter in Letters)
                {
                    if (Letter == 'A')
                        Console.Write(Number);

                    if (Board[Letter + "" + Number] != null)
                        Console.Write("|_" + Board[Letter + "" + Number].getKind() + "_");
                    else
                        Console.Write("|___");

                    if (Letter == 'H')
                        Console.WriteLine("|");
                }
            }
        }
    }
}
