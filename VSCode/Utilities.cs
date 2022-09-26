using System;

namespace ChessBoard
{
    public class Utilities
    {
        public static char Int2Char(int intPos)
        {
            char ch = Convert.ToChar(intPos + 64);                                                                                                  //convert range {1,2,...,8} --> {A,B,C,...,H}
            return ch;                                                                                                                              
        }

        public static int Char2Int(char charPos)
        {
            int i = Convert.ToInt32(charPos) - 64;                                                                                                  //convert range {A,B,C,...,H} --> {1,2,...,8}
            return i;
        }
    }
}
