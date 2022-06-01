using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PZ2_MSG_Decoder
{
    internal class BlockText
    {
        public int Index { get; set; }
        public long PointerOffset { get; set; }
        public long NewPointerOffset { get; set; }
        public Message[] Messages { get; set; }
        public BlockText(int index, long pointerOffset)
        {
            this.Index = index;
            this.PointerOffset = pointerOffset;
        }
    }
    internal class Message
    {
        public int Index { get; set; }
        public long Pointer { get; set; }
        public string Value { get; set; }
        public Message (int index, long pointer)
        {
            Index = index;
            Pointer = pointer;
        }
    }
}
