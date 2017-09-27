using System;
using System.Collections.Generic;
using System.Text;

namespace SCC
{
    public class Graph
    {
        private List<Vertex> vertexList;
        private int size;

        public Graph(int size)
        {
            this.size = size;
            vertexList = new List<Vertex>(size);
        }

        public void AddEdge(int from, int to)
        {
            if (!vertexList.Exists(v => v.index == from))
            {
                vertexList.Add(new Vertex(from));
            }
            if (!vertexList.Exists(v => v.index == to))
            {
                vertexList.Add(new Vertex(to));
            }
            vertexList.Find(v => v.index == from).AddOutNeighbor(to);
        }

        public Graph GetTranspose()
        {
            Graph t = new Graph(size);

            foreach (Vertex v in vertexList)
            {
                foreach (int outNeighbor in v.outNeighbors)
                {
                    t.AddEdge(outNeighbor, v.index);
                }
            }
            return t;
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
