using System;
using System.Collections.Generic;

public enum Colors
{
    White, Black    //0, 1
}
public enum BoardCoordinates
{
    A1,A2,A3,A4,A5,A6,A7,A8,    // 0,..., 7,
    B1,B2,B3,B4,B5,B6,B7,B8,    // 8,...,15,
    C1,C2,C3,C4,C5,C6,C7,C8,    //16,...,23,
    D1,D2,D3,D4,D5,D6,D7,D8,    //24,...,31,
    E1,E2,E3,E4,E5,E6,E7,E8,    //32,...,39,
    F1,F2,F3,F4,F5,F6,F7,F8,    //40,...,47,
    G1,G2,G3,G4,G5,G6,G7,G8,    //48,...,55,
    H1,H2,H3,H4,H5,H6,H7,H8     //56,...,63
}

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

    public class Pawn : Piece
    {
        public Pawn(string c, string p, char k)
        {
            Color = c;
            Position = p;
            Kind = k;
        }

        public override bool isLegalMove(char TargetX, int TargetY, ChessBoard chessboard)
        {
            //current position
            char CurrentX = Position[0];
            //parsing string and converting second coordinate appropriately
            int CurrentY = Convert.ToInt32(Position[1] - 48);

            Piece pieceAtTargetPosition = chessboard.getPieceAt(TargetX, TargetY);

            //allow for starting positon "jump" for white pawns
            if (Color == "Black" && CurrentY == 7 && Math.Abs(TargetY - CurrentY) == 2)
                return true;
            //allow for starting positon "jump" for black pawns
            else if (Color == "White" && CurrentY == 2 && Math.Abs(TargetY - CurrentY) == 2)
                return true;
            //there should always be a unitary move in the vertical direction
            else if (Math.Abs(TargetY - CurrentY) != 1)
                return false;
            //out of bounds
            else if (TargetY > 7 || TargetY < 1)
                return false;
            //filter illegal moves towards the wrong direction
            else if (Color == "Black" && TargetY - CurrentY >= 0)
                return false;
            else if (Color == "White" && TargetY - CurrentY <= 0)
                return false;
            else if (pieceAtTargetPosition == null)
                //no horizontal movement allowed, diagonal movement allowed only for !=null target
                if (Math.Abs(CurrentX - TargetX) == 1)
                    return false;
                else
                    return true;
            //not allow horizontal jumps > 1 or movements to already occupied cells of the same color
            else if (Math.Abs(CurrentX - TargetX) != 1 || Color == pieceAtTargetPosition.getColor())
                return false;
            else
                return true;
        }
    }

    public class Rook : Piece
    {
        public Rook(string c, string p, char k)
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

    public class King : Piece
    {
        public King(string c, string p, char k)
        {
            Color = c;
            Position = p;
            Kind = k;
        }

        public override bool isLegalMove(char xPos, int yPos, ChessBoard b)
        {
            //current position
            int intCurrentX = Utilities.char2Int(Position[0]);
            //parsing string and converting second coordinate appropriately
            int intCurrentY = Convert.ToInt32(Position[1] - 49);

            //target position
            int xPosInt = Utilities.char2Int(xPos);
            int yPosInt = yPos - 1;

            Piece p_targ = b.getPieceAt(xPos, yPos);

            if (Math.Abs(yPosInt - intCurrentY) != 1 && Math.Abs(intCurrentX - xPosInt) != 1)
                return false;
            else if (Math.Abs(yPosInt - intCurrentY) > 1 || Math.Abs(intCurrentX - xPosInt) > 1)
                return false;
            else if (yPosInt > 7 || yPosInt < 1)
                return false;
            else if (p_targ == null)
                if (Math.Abs(intCurrentX - xPosInt) > 1 || Math.Abs(intCurrentX - xPosInt) > 1)
                    return false;
                else
                    return true;
            else if (Color == p_targ.getColor())
                return false;
            else
                return true;
        }
    }

    public class Queen : Piece
    {
        public Queen(string c, string p, char k)
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

        static void Main(string[] args)
        {
            ChessEngine.playChess();
        }

        public class ChessEngine
        {
            public static void playChess()
            {
                //this field flags if its the black or white players turn
                var turn = true;

                ChessBoard chessboard = new ChessBoard();

                ApplicationMessage.printNotation();
                chessboard.initialiseBoard();
                chessboard.printBoard();

                while (true)
                {
                    ChessEngine.nextMove(chessboard, turn);
                    turn = !turn;
                }

            }

            public static void nextMove(ChessBoard b, bool turn)
            {
                //variables for storing the inputs 
                string OrigInput = "";
                string DestInput = "";

                //boolean expression for inputs
                Boolean isOrigInputValid = false;
                Boolean isDestInputValid = false;

                //origin input coordinates as a string if the form "A4"
                string origIn;
                //variables for storing a char & an int after parsing the string
                char xOrig;
                int yOrig;

                //destination input coordinates as a string of the same form 
                string destIn;
                //variables for storing a char & an int after parsing the string
                char xDest;
                int yDest;

                //string responsible for storing who is playing next "Black" or "White"
                string name;
                if (turn == true)
                    name = "White";
                else
                    name = "Black";

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"{name}'s turn!");
                Console.WriteLine("__________________________________________");

                while (isOrigInputValid == false)
                {
                    Console.WriteLine("Please enter the coordinates of the piece");
                    Console.WriteLine("you would like to move (ex. of format A2):");
                    Console.WriteLine("__________________________________________");

                    origIn = Console.ReadLine();
                    //perform a basic input validity scan
                    if (origIn.Length != 2)
                    {
                        Console.WriteLine("Input should be 2 characters long.");
                        Console.WriteLine("__________________________________________");
                    }
                    else
                    {
                        xOrig = origIn[0];
                        //ranges from [1,..,8]
                        yOrig = Convert.ToInt32(origIn[1] - 48);
                        if (Utilities.char2Int(xOrig) < 0 || Utilities.char2Int(xOrig) > 7 || yOrig < 1 || yOrig > 8)
                        {
                            Console.WriteLine("Input is invalid.");
                            Console.WriteLine("__________________________________________");

                        }
                        else
                        {
                            if (b.getPieceAt(xOrig, yOrig) == null)
                            {
                                Console.WriteLine("The cell you have selected is empty.");
                                Console.WriteLine("__________________________________________");
                            }

                            else
                            {
                                if (b.getPieceAt(xOrig, yOrig).Color != name)
                                {
                                    Console.WriteLine($"Select a {name} piece.");
                                    Console.WriteLine("__________________________________________");
                                }
                                else
                                {
                                    isOrigInputValid = true;
                                    OrigInput = origIn;
                                }
                            }
                        }
                    }
                }

                xOrig = OrigInput[0];
                //ranges from [1,..,8]
                yOrig = Convert.ToInt32(OrigInput[1] - 48);

                Piece p_select = b.getPieceAt(xOrig, yOrig);

                while (isDestInputValid == false)
                {
                    Console.WriteLine("Please enter the coordinates of the cell");
                    Console.WriteLine("you would like to move your piece to:");
                    Console.WriteLine("__________________________________________");

                    destIn = Console.ReadLine();
                    //perform a basic input validity scan
                    if (destIn.Length != 2)
                    {
                        Console.WriteLine("Input should be 2 characters long.");
                        Console.WriteLine("__________________________________________");
                    }
                    else
                    {
                        xDest = destIn[0];
                        //ranges from [1,..,8]
                        yDest = Convert.ToInt32(destIn[1] - 48);
                        if (Utilities.char2Int(xDest) < 0 || Utilities.char2Int(xDest) > 7 || yDest < 1 || yDest > 8)
                        {
                            Console.WriteLine("Input is invalid.");
                            Console.WriteLine("__________________________________________");
                        }
                        else
                        {
                            if (p_select.isLegalMove(xDest, yDest, b))
                            {
                                isDestInputValid = true;
                                DestInput = destIn;
                            }
                        }
                    }
                }

                xDest = DestInput[0];
                //ranges from [1,..,8]
                yDest = Convert.ToInt32(DestInput[1] - 48);

                b.movePieceAt(xOrig, yOrig, xDest, yDest);
                b.printBoard();

                Console.WriteLine("");
                Console.WriteLine("Next player's turn!");
            }
        }
    }

    public class ApplicationMessage
    {
        public static void printNotation()
        {
            Console.WriteLine("<=========================================>");
            Console.WriteLine("<                 RULES                   >");
            Console.WriteLine("<                                         >");
            Console.WriteLine("<  1.1 CONVENTION                         >");
            Console.WriteLine("<  The white pieces are represented with  >");
            Console.WriteLine("< capital letters whereas the black ones  >");
            Console.WriteLine("< are represented with small letters.     >");
            Console.WriteLine("<                                         >");
            Console.WriteLine("< 1.2 NOTATION                            >");
            Console.WriteLine("<  P --> white pawn    p--> black pawn    >");
            Console.WriteLine("<  R --> white rook    r--> black rook    >");
            Console.WriteLine("<  H --> white knight  h--> black knight  >");
            Console.WriteLine("<  B --> white bishop  b--> black bishop  >");
            Console.WriteLine("<  Q --> white queen   q--> black queen   >");
            Console.WriteLine("<  K --> white king    k--> black king    >");
            Console.WriteLine("<                                         >");
            Console.WriteLine("<=========================================>");
        }
    }

    public class Utilities
        {
            public static char int2Char(int intPos)
            {
                //convert range {0,1,2,...,7} --> {A,B,C,...,H}
                char ch = Convert.ToChar(intPos + 65);
                //Console.WriteLine("{0} converts to '{1}'", intPos, ch_low);
                return ch;
            }

            public static int char2Int(char charPos)
            {
                //convert range {A,B,C,...,H} --> {0,1,2,...,7}
                int i = Convert.ToInt32(charPos) - 65;
                //Console.WriteLine("{0} converts to '{1}'", charPos, i);
                return i;
            }
        }
    }
