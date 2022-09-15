using System;

namespace ChessBoard
{
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
