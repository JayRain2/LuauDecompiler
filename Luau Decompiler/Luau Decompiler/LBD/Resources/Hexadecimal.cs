using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luau_Decompiler.LBD.Resources
{
    // Simple hex libary so that we can manipulate hexadecimal strings
    public static class HexLib
    {
        public static byte[] ToByteArray(string Bytecode)
        {
            byte[] bytes = Bytecode.Split(' ').Select(s => Convert.ToByte(s, 16)).ToArray();
            return bytes;
        }

        public static char ToChar(byte Hex)
        {
            return Encoding.ASCII.GetChars(new byte[] { Hex })[0];
        }

        public static string ToString(byte[] Hex)
        {
            return Encoding.ASCII.GetString(Hex);
        }

        public static int ToInt(byte Hex)
        {
            return int.Parse(Hex.ToString("X"), System.Globalization.NumberStyles.HexNumber);
        }

    }
}
