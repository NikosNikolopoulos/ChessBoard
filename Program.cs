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
    public class Program
    {
        static void Main(string[] args)
        {
            ChessEngine.playChess();
        }
    }

}