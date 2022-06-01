using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PZ2_MSG_Decoder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = @"D:\VietHoaGame\Fatal Frame 2 PS2\Text\3270";
            List<BlockText> blocks = Decoder.Decode(file);
            List<string> lines = new List<string>();
            for (int i = 0; i < blocks.Count; i++)
            {
                foreach (var messages in blocks[i].Messages)
                {
                    lines.Add(messages.Value);
                }
            }
            File.WriteAllLines(Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".txt"), lines);
            Console.ReadKey();
        }
    }
}
