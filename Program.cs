using System;

namespace ChessBoard
{
    //abstract class that all chessboard pieces should inherit
    public abstract class Piece
    {
        public string Color = "NoColor";
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

            //allow for starting positon "jump" for white pawns
            if (Color == "White" && intCurrentY == 6 && Math.Abs(yPosInt - intCurrentY) == 2)
                return true;
            //allow for starting positon "jump" for black pawns
            else if (Color == "Black" && intCurrentY == 1 && Math.Abs(yPosInt - intCurrentY) == 2)
                return true;
            //there should always be a unitary move in the vertical direction
            else if (Math.Abs(yPosInt - intCurrentY) != 1)
                return false;
            //out of bounds
            else if (yPosInt > 7 || yPosInt < 1)
                return false;
            //filter illegal moves towards the wrong direction
            else if (Color == "White" && yPosInt - intCurrentY >= 0)
                return false;
            else if (Color == "Black" && yPosInt - intCurrentY <= 0)
                return false;
            else if (p_targ == null)
                //no horizontal movement allowed, diagonal movememnt allowed only for !=null target
                if (Math.Abs(intCurrentX - xPosInt) == 1)
                    return false;
                else
                    return true;
            //not allow horizontal jumps > 1 or movements to already occupied cells of the same color
            else if (Math.Abs(intCurrentX - xPosInt) != 1 || Color == p_targ.getColor())
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
            else if(p_targ == null)
                if (Math.Abs(intCurrentX - xPosInt) > 1 || Math.Abs(intCurrentX - xPosInt) > 1)
                    return false;
                else
                    return true;
            else if(Color == p_targ.getColor())
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

    //ilustrates a chess board
    public class ChessBoard
    {
        public Piece[,] board = new Piece [8, 8];

        public void placePieceAt(Piece p, char xPos, int yPos)
        {
            board[xPos, yPos] = p;
        }

        public Piece getPieceAt(char xPos, int yPos)
        {
            int xPosInt = Utilities.char2Int(xPos);
            int yPosInt = yPos - 1;
            return board[xPosInt, yPosInt];
        }

        public void movePieceAt(char xOrig, int yOrig, char xDest, int yDest)
        {
            int xOrigInt = Utilities.char2Int(xOrig);
            int yOrigInt = yOrig - 1;

            int xDestInt = Utilities.char2Int(xDest);
            int yDestInt = yDest - 1;

            Piece movingPiece = board[xOrigInt, yOrigInt];
            movingPiece.Position = "" + xDest + yDest;

            board[xOrigInt, yOrigInt] = null;
            board[xDestInt, yDestInt] = movingPiece;
        }

        //responsible for initialising the chess board with every piece in position
        public void loadBoard()
        {
            //Create 8 x Black Pawns in their spawning positions
            Pawn P1 = new Pawn("Black", "A2",'p');
            board[0,1] = P1;
            Pawn P2 = new Pawn("Black", "B2",'p');
            board[1,1] = P2;
            Pawn P3 = new Pawn("Black", "C2",'p');
            board[2,1] = P3;
            Pawn P4 = new Pawn("Black", "D2",'p');
            board[3,1] = P4;

            Pawn P5 = new Pawn("Black", "E2",'p');
            board[4,1] = P5;
            Pawn P6 = new Pawn("Black", "F2",'p');
            board[5,1] = P6;
            Pawn P7 = new Pawn("Black", "G2",'p');
            board[6,1] = P7;
            Pawn P8 = new Pawn("Black", "H2",'p');
            board[7,1] = P8;

            //Create 2 x Black Rooks in their spawning positions
            Rook R1 = new Rook("Black", "A1",'r');
            board[0,0] = R1;
            Rook R2 = new Rook("Black", "H1",'r');
            board[7,0] = R2;

            //Create 2 x Black Knights in their spawning positions
            Knight H1 = new Knight("Black", "B1",'h');
            board[1,0] = H1;
            Knight H2 = new Knight("Black", "G1",'h');
            board[6,0] = H2;

            //Create 2 x Black Bishops in their spawning positions
            Bishop B1 = new Bishop("Black", "C1",'b');
            board[2,0] = B1;
            Bishop B2 = new Bishop("Black", "F1",'b');
            board[5,0] = B2;

            //Create 1 x Black King in his spawning position
            King K = new King("Black", "E1",'k');
            board[4,0] = K;

            //Create 1 x Black Queen in her spawning position
            Queen Q = new Queen("Black", "D1",'q');
            board[3,0] = Q;

            /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

            //Create 8 x White Pawns in their spawning positions
            Pawn p1 = new Pawn("White", "A7",'P');
            board[0,6] = p1;
            Pawn p2 = new Pawn("White", "B7",'P');
            board[1,6] = p2;
            Pawn p3 = new Pawn("White", "C7",'P');
            board[2,6] = p3;
            Pawn p4 = new Pawn("White", "D7",'P');
            board[3,6] = p4;

            Pawn p5 = new Pawn("White", "E7",'P');
            board[4,6] = p5;
            Pawn p6 = new Pawn("White", "F7",'P');
            board[5,6] = p6;
            Pawn p7 = new Pawn("White", "G7",'P');
            board[6,6] = p7;
            Pawn p8 = new Pawn("White", "H7",'P');
            board[7,6] = p8;

            //Create 2 x White Rooks in their spawning positions
            Rook r1 = new Rook("White", "A8",'R');
            board[0,7] = r1;
            Rook r2 = new Rook("White", "H8",'R');
            board[7,7] = r2;

            //Create 2 x White Knights in their spawning positions
            Knight h1 = new Knight("White", "B8",'H');
            board[1,7] = h1;
            Knight h2 = new Knight("White", "G8",'H');
            board[6,7] = h2;

            //Create 2 x White Bishops in their spawning positions
            Bishop b1 = new Bishop("White", "C8",'B');
            board[2,7] = b1;
            Bishop b2 = new Bishop("White", "F8",'B');
            board[5,7] = b2;

            //Create 1 x White King in his spawning position
            King k = new King("White", "E8",'K');
            board[4,7] = k;

            //Create 1 x White Queen in her spawning position
            Queen q = new Queen("White", "D8",'Q');
            board[3,7] = q;
        }

        public void printBoard()
        {
            Console.WriteLine("    A   B   C   D   E   F   G   H");
            Console.Write("   ___ ___ ___ ___ ___ ___ ___ ___");
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("");
                Console.Write(i + " ");
                for (int j = 0; j < 8; j++)
                {
                    if (board[j, i-1] != null)
                        //Console.WriteLine(i+ " " + j);
                        Console.Write("|_" + board[j, i-1].getKind() + "_");
                    else
                        Console.Write("|___");
                }
                Console.Write("|");
            }
        }

        static void Main(string[] args)
        {
            ChessEngine.playChess();
            //ChessBoard b = new ChessBoard();
            //ChessBoard.loadBoard();
            //ChessBoard.movePieceAt('E',8,'E',3);
            //Console.WriteLine(b.getPieceAt('A',7).isLegalMove('A',5,b));
            //ChessBoard.printBoard();
            //Utilities.char2Int('H');
        }
    }

    public class ChessEngine
    {
        public static void printNotation()
        {
            Console.WriteLine("<=========================================>");
            Console.WriteLine("<                 RULES                   >");
            Console.WriteLine("<                                         >");
            Console.WriteLine("<  1.1 CONVENTION                         >");
            Console.WriteLine("<   The white pieces are repesented with  >");
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

        public static void playChess()
        {
            ChessBoard b = new ChessBoard();
            Console.WriteLine("EMPTY BOARD");
            b.printBoard();

            Console.WriteLine("");
            Console.WriteLine("Setting things up ...");
            ChessEngine.printNotation();
            b.loadBoard();
            b.printBoard();

            ChessEngine.nextMove(b, true);
        }

        public static void nextMove(ChessBoard b, bool turn)
        {
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

            Piece p_select = null;

            while (p_select == null)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"{name}'s turn!");
                Console.WriteLine("Please enter the coordinates of the piece");
                Console.WriteLine("you would like to move (ex. of format A2):");
                Console.WriteLine("__________________________________________");

                origIn = Console.ReadLine();
                xOrig = origIn[0];
                //ranges from [0,..,7]
                yOrig = Convert.ToInt32(origIn[1] - 48);

                Console.WriteLine("Please enter the coordinates of the cell");
                Console.WriteLine("you would like to move your piece to:");
                Console.WriteLine("__________________________________________");

                destIn = Console.ReadLine();
                xDest = destIn[0];
                yDest = Convert.ToInt32(destIn[1] - 48);

                if (Utilities.char2Int(xOrig) < 0 || Utilities.char2Int(xOrig) > 7 || yOrig < 0 || yOrig > 7)
                    p_select = null;
                else
                    p_select = b.getPieceAt(xOrig, yOrig);

                while (p_select == null || Utilities.char2Int(xOrig) < 0 || Utilities.char2Int(xOrig) > 7 || yOrig < 0 || yOrig > 7)
                {
                    Console.WriteLine(xOrig);
                    Console.WriteLine(yOrig);
                    Console.WriteLine("Empty selection or selection out of bounds!");
                    Console.WriteLine("Enter a valid position on the chessboard:");
                    Console.WriteLine("__________________________________________");
                    origIn = Console.ReadLine();
                    xOrig = origIn[0];
                    yOrig = Convert.ToInt32(origIn[1] - 48);
                    if (Utilities.char2Int(xOrig) < 0 || Utilities.char2Int(xOrig) > 7 || yOrig < 0 || yOrig > 7)
                    {
                        p_select = null;
                    }
                    
                    else
                        p_select = b.getPieceAt(xOrig, yOrig);
                }

                if (p_select.isLegalMove(xDest, yDest, b) && p_select.Color == name) 
                {
                    b.movePieceAt(xOrig, yOrig, xDest, yDest);
                    b.printBoard();
                    ChessEngine.nextMove(b, !turn);
                }
                else
                {
                    Console.WriteLine("The move you have selected was not valid!"); 
                    Console.WriteLine("Please try again!");
                    Console.WriteLine("__________________________________________");

                    ChessEngine.nextMove(b, turn);
                }
            }
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
            int i = Convert.ToInt32(charPos)-65;
            //Console.WriteLine("{0} converts to '{1}'", charPos, i);
            return i;
        }
    }
}