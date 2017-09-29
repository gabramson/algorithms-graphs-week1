using System;
using System.Collections.Generic;
using System.Text;

namespace SCC
{
    public class Graph
    {
        private Dictionary<int, Vertex> forwardList;
        private Dictionary<int, Vertex> transposeList;
        private HashSet<int> visited;
        private Stack<int> finishingOrder;
        public List<int> sccSizes { get; }
        private int size;

        public Graph()
        {
            this.size = 0;
            forwardList = new Dictionary<int, Vertex>();
            transposeList = new Dictionary<int, Vertex>();
            visited = new HashSet<int>();
            finishingOrder = new Stack<int>();
            sccSizes = new List<int>();
        }

        public void AddEdge(int from, int to)
        {
            if (!forwardList.ContainsKey(from))
            {
                forwardList.Add(from, new Vertex(from));
                transposeList.Add(from, new Vertex(from));
                size += 1;
            }
            if (!forwardList.ContainsKey(to))
            {
                forwardList.Add(to, new Vertex(to));
                transposeList.Add(to, new Vertex(to));
                size += 1;
            }
            forwardList[from].AddOutNeighbor(to);
            transposeList[to].AddOutNeighbor(from);
        }

        public void MakeSCC()
        {
            ResetVisited();
            foreach (Vertex v in forwardList.Values)
            {
                if (!visited.Contains(v.index))
                {
                    DFS(forwardList,
                        v, 
                        vertex=> visited.Add(vertex.index),
                        vertex=> finishingOrder.Push(vertex.index));
                }
            }
            ResetVisited();
            while (finishingOrder.Count > 0)
            {
                int i = finishingOrder.Pop();
                int count = 0;
                if (!visited.Contains(i))
                {
                    DFS(transposeList,
                        transposeList[i],
                        vertex => { visited.Add(vertex.index); count += 1; },
                        vertex => { });
                    sccSizes.Add(count);
                }

            }

        }

        private void DFS(Dictionary<int, Vertex> list,Vertex v, Action<Vertex> pre, Action<Vertex> post)
        {
            pre(v);
            foreach(int outNeighbor in v.outNeighbors)
            {
                if (!visited.Contains(outNeighbor))
                {
                    DFS(list,list[outNeighbor],pre, post);
                }
            }
            post(v);
        }

        private void ResetVisited()
        {
            visited.Clear();
        }

        private class Vertex
        {
            public int index { get; }
            public List<int> outNeighbors { get; }

            public Vertex(int myIndex)
            {
                index = myIndex;
                outNeighbors = new List<int>();
            }

            public void AddOutNeighbor(int neighborIndex)
            {
                outNeighbors.Add(neighborIndex);
            }
        }


    }

}
