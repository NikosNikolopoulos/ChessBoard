using System;

namespace ChessBoard
{
    public class Utilities
    {
        public static char int2Char(int intPos)
        {
            //convert range {1,2,...,8} --> {A,B,C,...,H}
            char ch = Convert.ToChar(intPos + 64);
            //Console.WriteLine("{0} converts to '{1}'", intPos, ch_low);
            return ch;
        }

        public static int char2Int(char charPos)
        {
            //convert range {A,B,C,...,H} --> {1,2,...,8}
            int i = Convert.ToInt32(charPos) - 64;
            //Console.WriteLine("{0} converts to '{1}'", charPos, i);
            return i;
        }
    }
}
