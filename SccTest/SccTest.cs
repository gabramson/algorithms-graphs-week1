using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCC;

namespace SccTest
{
    [TestClass]
    public class TestGraph
    {
        [TestMethod]
        public void TestMethod1()
        {
            Graph g = new Graph();
            g.AddEdge(2, 1);
            g.AddEdge(1, 3);
            g.AddEdge(3, 2);
            g.AddEdge(1, 4);
            g.AddEdge(4, 5);

            Graph t = g.GetTranspose();
        }
    }
}
