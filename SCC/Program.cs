using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace SCC
{
    class Program
    {
        public static void CallToChildThread()
        {
            Graph g = new Graph();
            string line;
            using (StreamReader srStreamRdr = new StreamReader(@"D:\Projects\C#\Coursera\alogorithms-graphs\Week1\scc.txt"))
            {
                while ((line = srStreamRdr.ReadLine()) != null)
                {
                    string[] values = line.Split(new Char[] { ' ' });
                    g.AddEdge(int.Parse(values[0]), int.Parse(values[1]));
                }
            }
            g.MakeSCC();
            foreach (int i in g.sccSizes.OrderByDescending(item => item).Take(5))
            {
                Console.Write($"{i},");
            }
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            ThreadStart childref = new ThreadStart(CallToChildThread);
            Thread childThread = new Thread(childref,10000000);
            childThread.Start();
        }
    }
}
