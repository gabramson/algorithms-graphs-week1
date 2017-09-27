using System;
using System.Collections.Generic;
using System.Text;

namespace SCC
{
    public class Graph
    {
        private List<Vertex> vertexList;

        public Graph(int size)
        {
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
            return new Graph(vertexList.Capacity);
        }

        private class Vertex
        {
            public int index { get; }
            List<int> outNeighbors;

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
