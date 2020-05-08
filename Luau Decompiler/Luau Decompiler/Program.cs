using LBD.Source_Files;
using Luau_Decompiler.LBD.Resources;
using System;

namespace LBD
{
    class Program
    {
        static void Main(string[] args)
        {
            Decompiler a = new Decompiler(HexLib.ToByteArray("01 03 05 70 72 69 6E 74 05 48 65 6C 6C 6F 05 73 70 61 77 6E 02 02 00 00 00 06 C0 00 00 00 A4 00 01 00 00 00 00 40 6F 01 02 00 9F 00 02 01 82 00 01 00 03 03 01 04 00 00 00 40 03 02 00 00 06 01 01 00 00 00 01 00 02 00 00 01 07 A3 00 00 00 A4 00 01 00 00 00 00 40 D9 01 00 00 9F 00 02 02 9F 00 01 01 82 00 01 00 02 03 03 04 00 00 00 40 01 00 00 07 01 00 00 00 00 00 02 00"));
        }
    }
}
