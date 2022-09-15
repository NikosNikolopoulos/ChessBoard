using System;

namespace ChessBoard
{
    public class ApplicationMessage
    {
        public static void printNotation()
        {
            Console.WriteLine("<=========================================>" +
                              "\n<                 RULES                   >" +
                              "\n<                                         >" +
                              "\n< 1.1 CONVENTION                          >" +
                              "\n<  The white pieces are represented with  >" +
                              "\n< capital letters whereas the black ones  >" +
                              "\n< are represented with small letters.     >" +
                              "\n<                                         >" +
                              "\n<  To UNDO a piece selection enter \"\" in  >" +
                              "\n< the destination coordinates field for   >" +
                              "\n< your selected piece.                    >" +
                              "\n<                                         >" +
                              "\n< 1.2 NOTATION                            >" +
                              "\n<  P --> white pawn    p --> black pawn   >" +
                              "\n<  R --> white rook    r --> black rook   >" +
                              "\n<  H --> white knight  h --> black knight >" +
                              "\n<  B --> white bishop  b --> black bishop >" +
                              "\n<  Q --> white queen   q --> black queen  >" +
                              "\n<  K --> white king    k --> black king   >" +
                              "\n<                                         >" +
                              "\n<=========================================>");
        }
    }
}
