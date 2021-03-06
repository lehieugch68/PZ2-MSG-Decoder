using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PZ2_MSG_Decoder
{
    internal class Decoder
    {
        public static List<BlockText> Decode(string file)
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(file, FileMode.Open, FileAccess.Read)))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                int endPointerOffset = (int)reader.BaseStream.Length;
                int blockCount = 0;
                while (reader.BaseStream.Position < endPointerOffset)
                {
                    int temp = reader.ReadInt32();
                    if (temp < endPointerOffset) endPointerOffset = temp;
                    blockCount++;
                }
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                List<BlockText> blocks = new List<BlockText>();
                for (int i = 0; i < blockCount; i++)
                {
                    uint pointerOffset = reader.ReadUInt32();
                    BlockText block = new BlockText(i, pointerOffset);
                    blocks.Add(block);
                }
                BlockText[] blocksSorted = blocks.OrderBy(e => e.PointerOffset).ToArray();
                for (int i = 0; i < blocksSorted.Length; i++)
                {
                    List<Message> messages = new List<Message>();
                    reader.BaseStream.Seek(blocksSorted[i].PointerOffset, SeekOrigin.Begin);
                    int firstPointer = reader.ReadInt32();
                    int pointerCount = (firstPointer - (int)blocksSorted[i].PointerOffset) / 4;
                    reader.BaseStream.Seek(blocksSorted[i].PointerOffset, SeekOrigin.Begin);
                    for (int x = 0; x < pointerCount; x++)
                    {
                        int pointer = reader.ReadInt32();
                        Message message = new Message(x, pointer);
                        messages.Add(message);
                    }
                    blocksSorted[i].Messages = messages.OrderBy(e => e.Pointer).ToArray();
                    for (int x = 0; x < blocksSorted[i].Messages.Length; x++)
                    {
                        int nextPointer = x >= pointerCount - 1 ?
                            (i >= blocksSorted.Length - 1 ?
                                (int)reader.BaseStream.Length :
                                (int)blocksSorted[i + 1].PointerOffset) :
                            (int)blocksSorted[i].Messages[x + 1].Pointer;
                        int strLen = nextPointer - (int)blocksSorted[i].Messages[x].Pointer;
                        reader.BaseStream.Seek(blocksSorted[i].Messages[x].Pointer, SeekOrigin.Begin);
                        byte[] encoded = reader.ReadBytes(strLen);
                        string[] strs = new string[encoded.Length];
                        for (int y = 0; y < strs.Length; y++)
                        {
                            string temp = string.Empty;
                            if (CustomEncoding.Page0.TryGetValue(encoded[y], out temp)) strs[y] = temp;
                            else strs[y] = "{" + encoded[y].ToString() + "}";
                        }
                        string str = string.Join("", strs);
                        foreach (var entry in CustomEncoding.Code)
                        {
                            str = str.Replace(entry.Key, entry.Value);
                        }
                        blocksSorted[i].Messages[x].Value = str;
                    }
                }
                    
                return blocks;
            }
        }
        public static byte[] Encode(string input, string txt) 
        {
            BlockText[] blocks = Decode(input).ToArray();
            string[] text = File.ReadAllLines(txt);
            int index = 0;
            for (int i = 0; i < blocks.Length; i++) 
            {
                for (int x = 0; x < blocks[i].Messages.Length; x++)
                {
                    if (index < text.Length - 1) blocks[i].Messages[x].Value = text[index++];
                }
            }
            MemoryStream stream = new MemoryStream();
            using (BinaryWriter bw = new BinaryWriter(stream))
            {
                bw.Write(new byte[blocks.Length * 4]);
                var blocksSorted = blocks.OrderBy(b => b.PointerOffset).ToArray();
                for (int i = 0; i < blocksSorted.Length; i++)
                {
                    blocksSorted[i].PointerOffset = bw.BaseStream.Position;
                    bw.Write(new byte[blocksSorted[i].Messages.Length * 4]);
                    for (int x = 0; x < blocksSorted[i].Messages.Length; x++)
                    {
                        blocksSorted[i].Messages[x].Pointer = bw.BaseStream.Position;
                        byte[] bytes = EncodeText(blocksSorted[i].Messages[x].Value);
                        bw.Write(bytes);
                    }
                    bw.BaseStream.Position = blocksSorted[i].PointerOffset;
                    for (int x = 0; x < blocksSorted[i].Messages.Length; x++)
                    {
                        bw.Write((int)blocksSorted[i].Messages[x].Pointer);
                    }
                    bw.BaseStream.Position = bw.BaseStream.Length;
                }
                bw.BaseStream.Position = 0;
                for (int i = 0; i < blocks.Length; i++)
                {
                    bw.Write((int)blocks[i].PointerOffset);
                }
            }
            return stream.ToArray();
        }
        private static byte[] EncodeText(string input)
        {
            foreach (var code in CustomEncoding.Code)
            {
                input = input.Replace(code.Value, code.Key);
            }
            char[] chars = input.ToCharArray();
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] != '{')
                {
                    foreach (var encoding in CustomEncoding.Page0)
                    {
                        if (encoding.Value == chars[i].ToString())
                        {
                            bytes.Add(encoding.Key);
                            break;
                        }
                    }
                }
                else
                {
                    string dec = "";
                    i++;
                    while (chars[i] != '}')
                    {
                        dec += chars[i++];

                    }
                    bytes.Add((byte)int.Parse(dec));
                }
            }
            return bytes.ToArray();
        }
    }
}
