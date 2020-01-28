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
            while(true) {
                //Console.WriteLine(this.Reader.BaseStream.Position.ToString("X") + " ==> " + Data[this.Reader.BaseStream.Position].ToString("X"));
                Component _component = ParseComponent();
                Console.WriteLine(_component);
            }
        }

        private Component ParseComponent() {
            byte v278 = this.Reader.ReadByte();
            uint v30 = (uint)(v278 >> 6);
            uint v31 = (uint)(((v278 >> 4) & 3) + 1);
            uint v32 = 0;
            uint v33 = 0;
            uint v34 = 0;
            if(v30 != 3) {
                v32 = (uint)((v278 & 8) > 0 ? 1 : v32);
                v32++;
            }
            if(v30 == 3 || v30 != 0) {
                v33 = 0;
                v34 = 0;
            }
            else {
                v33 = (((uint)(byte)v278 >> 1) & 3) + 1;
            }
            v34 = 1;

            if((v30 == 3 || v30 != 0) && v30 != 1) {
                v34 = 0;
            }

            uint EntityId = 0;
            uint ComponentId = 0;
            uint RessourceId = 0;
            uint ComponentSize = 0;
            bool UseZeroRunLengthCompression = ((v278 & 1) == 0); //v104

            int v28 = 0;
            if(v31 != 0) {
                do
                {
                    EntityId |= (uint)(this.Reader.ReadByte() << v28);
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
                    ComponentId |= (uint)(this.Reader.ReadByte() << v28);
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
                    RessourceId |= (uint)(this.Reader.ReadByte() << v39);
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

            byte[] componentBuffer;
            if (UseZeroRunLengthCompression)
            {
                ZeroRunLengthCompression comp = new ZeroRunLengthCompression();
                componentBuffer = new byte[ComponentSize];
                comp.Start(new MemoryStream(componentBuffer), (MemoryStream)this.Reader.BaseStream, componentBuffer.Length);
                comp.ReadBytes(componentBuffer, componentBuffer.Length);
            }
            else {
                componentBuffer = new byte[ComponentSize];
                this.Reader.Read(componentBuffer, 0, componentBuffer.Length);
            }

            //Console.WriteLine(" v30:"+v30.ToString("X") + " v31:" + v31.ToString("X") + " v32:" + v32.ToString("X") + " v33:" + v33.ToString("X") + " v34:" + v34.ToString("X"));
            //Console.WriteLine(" EntityId:" + EntityId.ToString("X") + " RessourceId:" + RessourceId.ToString("X") + " ComponentSize:" + ComponentSize.ToString("X") + " ComponentId:" + ComponentId.ToString("X"));

            return new Component(EntityId, RessourceId, ComponentSize, ComponentId, componentBuffer);
        }
    }
}
