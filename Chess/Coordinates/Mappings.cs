namespace Chess
{
    public static class Mappings
    {
        public static int ToNum(string x)
        {
            switch (x)
            {
                case "A": case "1": return 1;
                case "B": case "2": return 2;
                case "C": case "3": return 3;
                case "D": case "4": return 4;
                case "E": case "5": return 5;
                case "F": case "6": return 6;
                case "G": case "7": return 7;
                case "H": case "8": return 8;

                default: return 0;
            }
        }

        public static string ToLet(int x)
        {
            switch (x)
            {
                case 1: return "A";
                case 2: return "B";
                case 3: return "C";
                case 4: return "D";
                case 5: return "E";
                case 6: return "F";
                case 7: return "G";
                case 8: return "H";

                default: return "Z";
            }
        }
    }
}
