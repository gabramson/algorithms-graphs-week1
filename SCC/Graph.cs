using System;
using System.Collections.Generic;
using System.Text;

namespace SCC
{
    public class Graph
    {
        private List<Vertex> vertexList;
        private List<Boolean> visited;
        private Stack<int> finishingTimes;
        private int size;

        public Graph()
        {
            this.size = 0;
            vertexList = new List<Vertex>();
            visited = new List<bool>();
            finishingTimes = new Stack<int>();
        }

        public void AddEdge(int from, int to)
        {
            if (!vertexList.Exists(v => v.index == from))
            {
                vertexList.Add(new Vertex(from));
                visited.Add(false);
                size += 1;
            }
            if (!vertexList.Exists(v => v.index == to))
            {
                vertexList.Add(new Vertex(to));
                visited.Add(false);
                size += 1;
            }
            vertexList.Find(v => v.index == from).AddOutNeighbor(to);
        }

        public void MakeSCC()
        {
            ResetVisited();
            foreach (Vertex v in vertexList)
            {
                if (!visited[v.index-1])
                {
                    DFS(v, 
                        vertex=> visited[vertex.index - 1] = true,
                        vertex=> finishingTimes.Push(vertex.index));
                }
            }

        }

        private void DFS(Vertex v, Action<Vertex> pre, Action<Vertex> post)
        {
            pre(v);

            foreach(int outNeighbor in v.outNeighbors)
            {
                if (!visited[outNeighbor-1])
                {
                    DFS(vertexList.Find(vertex => vertex.index == outNeighbor),pre, post);
                }
            }
            post(v);
        }


        public Graph GetTranspose()
        {
            Graph t = new Graph();

            foreach (Vertex v in vertexList)
            {
                foreach (int outNeighbor in v.outNeighbors)
                {
                    t.AddEdge(outNeighbor, v.index);
                }
            }
            return t;
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
