using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PZ2_MSG_Decoder
{
    internal class CustomEncoding
    {
        #region En
        public static Dictionary<byte, string> Page0 = new Dictionary<byte, string>()
        {
            { 0x5A, " " },
            { 0x1, "A" },
            { 0x2, "B" },
            { 0x3, "C" },
            { 0x4, "D" },
            { 0x5, "E" },
            { 0x6, "F" },
            { 0x7, "G" },
            { 0x8, "H" },
            { 0x9, "I" },
            { 0xA, "J" },
            { 0xB, "K" },
            { 0xC, "L" },
            { 0xD, "M" },
            { 0xE, "N" },
            { 0xF, "O" },
            { 0x10, "P" },
            { 0x11, "Q" },
            { 0x12, "R" },
            { 0x13, "S" },
            { 0x14, "T" },
            { 0x15, "U" },
            { 0x16, "V" },
            { 0x17, "W" },
            { 0x18, "X" },
            { 0x19, "Y" },
            { 0x1A, "Z" },
            { 0x1B, "a" },
            { 0x1C, "b" },
            { 0x1D, "c" },
            { 0x1E, "d" },
            { 0x1F, "e" },
            { 0x20, "f" },
            { 0x21, "g" },
            { 0x22, "h" },
            { 0x23, "i" },
            { 0x24, "j" },
            { 0x25, "k" },
            { 0x26, "l" },
            { 0x27, "m" },
            { 0x28, "n" },
            { 0x29, "o" },
            { 0x2A, "p" },
            { 0x2B, "q" },
            { 0x2C, "r" },
            { 0x2D, "s" },
            { 0x2E, "t" },
            { 0x2F, "u" },
            { 0x30, "v" },
            { 0x31, "w" },
            { 0x32, "x" },
            { 0x33, "y" },
            { 0x34, "z" },

            { 0x35, "０" },
            { 0x36, "１" },
            { 0x37, "２" },
            { 0x38, "３" },
            { 0x39, "４" },
            { 0x3A, "５" },
            { 0x3B, "６" },
            { 0x3C, "７" },
            { 0x3D, "８" },
            { 0x3E, "９" },

            { 0x3F, "0" },
            { 0x40, "1" },
            { 0x41, "2" },
            { 0x42, "3" },
            { 0x43, "4" },
            { 0x44, "5" },
            { 0x45, "6" },
            { 0x46, "7" },
            { 0x47, "8" },
            { 0x48, "9" },

            { 0x4D, "(" },
            { 0x4E, ")" },
            { 0x4F, "," },
            { 0x50, "?" },
            { 0x51, "!" },
            { 0x52, "/" },
            { 0x54, ":" },
            { 0x55, "*" },
            { 0x56, "~" },
            { 0x57, "-" },
            { 0x58, "'" },
            { 0x59, "." },
            
            { 0x5C, "&" },
            { 0x60, "%" },
            { 0x61, ";" },

            { 0x6F, "“" },
            { 0x5B, "”" },
        };
        #endregion
        #region Jp
        public static Dictionary<byte, string> Page1 = new Dictionary<byte, string>()
        {

        };
        #endregion
        public static Dictionary<string, string> Code = new Dictionary<string, string>()
        {
            { "{251}", "{Next}" },
            { "{254}", "{LF}" },
            { "{255}", "{End}" },
            { "{73}", "{Circle}" },
            { "{74}", "{Cross}" },
            { "{75}", "{Triangle}" },
            { "{76}", "{Square}" },
            { "{253}A", "{EndColor}" },
            { "{253}o", "{FD29}" },
            { "{253}k", "{FD25}" },
            { "{253}l", "{FD26}" },
            { "{253}p", "{FD2A}" },
            { "{253}q", "{FD2B}" },
            { "{253}r", "{FD2C}" },
            { "{253}s", "{FD2D}" },
            { "{253}{0}", "{FD00}" },
        };


    }
}
