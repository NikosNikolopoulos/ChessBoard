using System;

namespace ChessBoard
{
    public class ApplicationMessage
    {
        public static void printNotation()
        {
            Console.WriteLine("<=========================================>");
            Console.WriteLine("<                 RULES                   >");
            Console.WriteLine("<                                         >");
            Console.WriteLine("< 1.1 CONVENTION                          >");
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
}
