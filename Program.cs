using System;

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
        
        public override bool isLegalMove(char xPos, int yPos, ChessBoard b)
        {
            //current position
            int intCurrentX = Utilities.char2Int(Position[0]);
            //parsing string and converting second coordinate appropriately
            int intCurrentY = Convert.ToInt32(Position[1] - 49);

            //target position
           int xPosInt = Utilities.char2Int(xPos);
           int yPosInt = yPos - 1;

           if (Math.Abs(yPosInt - intCurrentY) != 1 || b.getPieceAt(xPos, yPos) != null)
               return false;
           else if (yPosInt > 7 || yPosInt < 1)
               return false;
           else if (b.getColor() == "White")
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
        public static Piece[,] board = new Piece [8, 8];

        public Piece getPieceAt(char xPos, int yPos)
        {
            int xPosInt = Utilities.char2Int(xPos);
            int yPosInt = yPos - 1;
            return board[xPosInt, yPosInt];
        }

        public static void movePieceAt(char xOrig, int yOrig, char xDest, int yDest)
        {
            int xOrigInt = Utilities.char2Int(xOrig);
            int yOrigInt = yOrig - 1;

            int xDestInt = Utilities.char2Int(xDest);
            int yDestInt = yDest - 1;

            Piece movingPiece = board[xOrigInt, yOrigInt];
            board[xOrigInt, yOrigInt] = null;
            board[xDestInt, yDestInt] = movingPiece;
        }

        //responsible for initialising the chess board with every piece in position
        public static void loadBoard()
        {
            //Create 8 x White Pawns in their spawning positions
            Pawn P1 = new Pawn("White", "A2",'p');
            board[0,1] = P1;
            Pawn P2 = new Pawn("White", "B2",'p');
            board[1,1] = P2;
            Pawn P3 = new Pawn("White", "C2",'p');
            board[2,1] = P3;
            Pawn P4 = new Pawn("White", "D2",'p');
            board[3,1] = P4;

            Pawn P5 = new Pawn("White", "E2",'p');
            board[4,1] = P5;
            Pawn P6 = new Pawn("White", "F2",'p');
            board[5,1] = P6;
            Pawn P7 = new Pawn("White", "G2",'p');
            board[6,1] = P7;
            Pawn P8 = new Pawn("White", "H2",'p');
            board[7,1] = P8;

            //Create 2 x White Rooks in their spawning positions
            Rook R1 = new Rook("White", "A1",'r');
            board[0,0] = R1;
            Rook R2 = new Rook("White", "H1",'r');
            board[7,0] = R2;

            //Create 2 x White Knights in their spawning positions
            Knight H1 = new Knight("White", "B1",'h');
            board[1,0] = H1;
            Knight H2 = new Knight("White", "G1",'h');
            board[6,0] = H2;

            //Create 2 x White Bishops in their spawning positions
            Bishop B1 = new Bishop("White", "C1",'B');
            board[2,0] = B1;
            Bishop B2 = new Bishop("White", "F1",'B');
            board[5,0] = B2;

            //Create 1 x White King in his spawning position
            King K = new King("White", "E1",'K');
            board[4,0] = K;

            //Create 1 x White Queen in her spawning position
            Queen Q = new Queen("White", "D1",'Q');
            board[3,0] = Q;

            /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

            //Create 8 x Black Pawns in their spawning positions
            Pawn p1 = new Pawn("Black", "A7",'p');
            board[0,6] = p1;
            Pawn p2 = new Pawn("Black", "B7",'p');
            board[1,6] = p2;
            Pawn p3 = new Pawn("Black", "C7",'p');
            board[2,6] = p3;
            Pawn p4 = new Pawn("Black", "D7",'p');
            board[3,6] = p4;

            Pawn p5 = new Pawn("Black", "E7",'p');
            board[4,6] = p5;
            Pawn p6 = new Pawn("Black", "F7",'p');
            board[5,6] = p6;
            Pawn p7 = new Pawn("Black", "G7",'p');
            board[6,6] = p7;
            Pawn p8 = new Pawn("Black", "H7",'p');
            board[7,6] = p8;

            //Create 2 x Black Rooks in their spawning positions
            Rook r1 = new Rook("Black", "A8",'r');
            board[0,7] = r1;
            Rook r2 = new Rook("Black", "H8",'r');
            board[7,7] = r2;

            //Create 2 x Black Knights in their spawning positions
            Knight h1 = new Knight("Black", "B8",'h');
            board[1,7] = h1;
            Knight h2 = new Knight("Black", "G8",'h');
            board[6,7] = h2;

            //Create 2 x Black Bishops in their spawning positions
            Bishop b1 = new Bishop("Black", "C8",'b');
            board[2,7] = b1;
            Bishop b2 = new Bishop("Black", "F8",'b');
            board[5,7] = b2;

            //Create 1 x Black King in his spawning position
            King k = new King("Black", "E8",'k');
            board[4,7] = k;

            //Create 1 x Black Queen in her spawning position
            Queen q = new Queen("Black", "D8",'q');
            board[3,7] = q;
        }

        public static void printBoard()
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
            ChessBoard b = new ChessBoard();
            ChessBoard.loadBoard();
            ChessBoard.movePieceAt('A',8,'A',4);
            Console.WriteLine(b.getPieceAt('A',7).isLegalMove('A',8,b));
            ChessBoard.movePieceAt('A',7,'A',1);
            ChessBoard.printBoard();
            Utilities.char2Int('H');
        }
    }

    public class Utilities
    {
        public static char int2Char(int intPos)
        {
            //convert to range {0,1,2,...,7} --> {A,B,C,...,H}
            char ch = Convert.ToChar(intPos + 65);
            //Console.WriteLine("{0} converts to '{1}'", intPos, ch_low);
            return ch;
        }

        public static int char2Int(char charPos)
        {
            //convert to range {A,B,C,...,H} --> {0,1,2,...,7}
            int i = Convert.ToInt32(charPos)-65;
            //Console.WriteLine("{0} converts to '{1}'", charPos, i);
            return i;
        }
    }
}