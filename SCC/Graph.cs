using System;
using System.Collections.Generic;
using System.Text;

namespace SCC
{
    public class Graph
    {
        private List<Vertex> vertexList;
        private List<Boolean> visited;
        private int size;

        public Graph()
        {
            this.size = 0;
            vertexList = new List<Vertex>();
            visited = new List<bool>();
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

        public Graph GetTranspose()
        {
            Graph t = new Graph();
            ResetVisited();

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
