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
            Graph g = new Graph(5);
            g.AddEdge(1, 2);
            Graph t = g.GetTranspose();
        }
    }
}
