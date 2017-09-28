using System;
using System.Collections.Generic;
using System.Text;

namespace SCC
{
    public class Graph
    {
        private List<Vertex> vertexList;
        private int size;
        private int[] visited;

        public Graph()
        {
            this.size = 0;
            vertexList = new List<Vertex>();
        }

        public void AddEdge(int from, int to)
        {
            if (!vertexList.Exists(v => v.index == from))
            {
                vertexList.Add(new Vertex(from));
                size += 1;
            }
            if (!vertexList.Exists(v => v.index == to))
            {
                vertexList.Add(new Vertex(to));
                size += 1;
            }
            vertexList.Find(v => v.index == from).AddOutNeighbor(to);
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
