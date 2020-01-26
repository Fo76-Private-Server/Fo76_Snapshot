using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Fo76_Snapshot;

namespace Fo76_Snapshot_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] SnapshotData = File.ReadAllBytes("Snapshot.bin");
            Snapshot snap = new Snapshot(SnapshotData);
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
