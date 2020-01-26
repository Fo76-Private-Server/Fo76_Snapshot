using System;
using System.IO;
using Fo76_SnapshotDecompression;

namespace Fo76_Snapshot
{
    public class Snapshot
    {
        private BinaryReader Reader;
        public Snapshot(byte[] Data) {
            this.Reader = new BinaryReader(new MemoryStream(Data));
            ParseComponents();
        }

        private void ParseComponents() {
            byte v278 = this.Reader.ReadByte();
            uint v30 = (uint)(v278 >> 6);
            uint v31 = (uint)(((v278 >> 4) & 3) + 1);
            uint v32 = 0;
            uint v33 = 0;
            uint v34 = 0;
            if(v31 != 3) {
                v32 = (uint)((v278 & 8) == 1 ? 1 : v32);
                v32++;
            }
            if(v31 == 3 || (v278 & 8) != 0) {
                v33 = 0;
                v34 = 0;
                if(v30 != 1) {
                }
            }
            else {
                v33 = (((uint)(byte)v278 >> 1) & 3) + 1;
            }
            v34 = 1;

            if((v31 == 3 || (v278 & 8) != 0) && v30 != 1) {
                v34 = 0;
            }

            uint Key = 0;
            uint Err = 0;
            uint Size = 0;
            uint ComponentSize = 0;

            int v28 = 0;
            if(v31 != 0) {
                do
                {
                    Key |= (uint)(this.Reader.ReadByte() << v28);
                    v28 += 8;
                    --v31;
                }
                while (v31 != 0);
                v28 = 0;
            }
            if (v32 != 0)
            {
                uint v37 = v32;
                do
                {
                    Err |= (uint)(this.Reader.ReadByte() << v28);
                    v28 += 8;
                    --v37;
                }
                while (v37 != 0);
            }
            if (v33 != 0)
            {
                int v39 = 0;
                uint v40 = v33;
                do
                {
                    Size |= (uint)(this.Reader.ReadByte() << v39);
                    v39 += 8;
                    --v40;
                }
                while (v40 != 0);
            }
            if (v34 != 0)
            {
                int v41 = 0;
                uint v42 = v34;
                do
                {
                    ComponentSize |= (uint)(this.Reader.ReadByte() << v41);
                    v41 += 8;
                    --v42;
                }
                while (v42 != 0);
            }

            ZeroRunLengthCompression comp = new ZeroRunLengthCompression();
            byte[] componentBuffer = new byte[ComponentSize];
            comp.Start(new MemoryStream(componentBuffer), (MemoryStream)this.Reader.BaseStream, componentBuffer.Length);
            comp.ReadBytes(componentBuffer, componentBuffer.Length);

            Console.WriteLine(this.Reader.ReadByte().ToString("X"));
        }
    }
}
