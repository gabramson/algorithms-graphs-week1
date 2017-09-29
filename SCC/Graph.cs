using System;
using System.Collections.Generic;
using System.Text;

namespace SCC
{
    public class Graph
    {
        private List<Vertex> vertexList;
        private List<Vertex> transposeList;
        private List<Boolean> visited;
        private Stack<int> finishingTimes;
        public List<int> sccSizes { get; }
        private int size;

        public Graph()
        {
            this.size = 0;
            vertexList = new List<Vertex>();
            transposeList = new List<Vertex>();
            visited = new List<bool>();
            finishingTimes = new Stack<int>();
            sccSizes = new List<int>();
        }

        public void AddEdge(int from, int to)
        {
            if (!vertexList.Exists(v => v.index == from))
            {
                vertexList.Add(new Vertex(from));
                transposeList.Add(new Vertex(from));
                visited.Add(false);
                size += 1;
            }
            if (!vertexList.Exists(v => v.index == to))
            {
                vertexList.Add(new Vertex(to));
                transposeList.Add(new Vertex(to));
                visited.Add(false);
                size += 1;
            }
            vertexList.Find(v => v.index == from).AddOutNeighbor(to);
            transposeList.Find(v => v.index == to).AddOutNeighbor(from);
        }

        public void MakeSCC()
        {
            ResetVisited();
            foreach (Vertex v in vertexList)
            {
                if (!visited[v.index-1])
                {
                    DFS(vertexList,
                        v, 
                        vertex=> visited[vertex.index - 1] = true,
                        vertex=> finishingTimes.Push(vertex.index));
                }
            }
            ResetVisited();
            while (finishingTimes.Count > 0)
            {
                int i = finishingTimes.Pop();
                int count = 0;
                if (!visited[i-1])
                {
                    DFS(transposeList,
                        transposeList.Find(v => v.index == i),
                        vertex => { visited[vertex.index - 1] = true; count += 1; },
                        vertex => { });
                    sccSizes.Add(count);
                }

            }

        }

        private void DFS(List<Vertex> list,Vertex v, Action<Vertex> pre, Action<Vertex> post)
        {
            pre(v);
            foreach(int outNeighbor in v.outNeighbors)
            {
                if (!visited[outNeighbor-1])
                {
                    DFS(list,list.Find(vertex => vertex.index == outNeighbor),pre, post);
                }
            }
            post(v);
        }

        private void ResetVisited()
        {
            for (int i=0; i<size; i+=1){
                visited[i]=false;
            }
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
