using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms
{
    class Graph
    {
        public List<Vertex> vertices;
        public List<Edge> edges;
        public bool undirected;

        public Graph(bool undirected) => this.undirected = undirected;

        public Graph Copy()
        {
            Graph tempGraph = new Graph(undirected);
            tempGraph.vertices = new List<Vertex>(vertices);
            foreach (Vertex vertex in tempGraph.vertices)
            {
                vertex.visited = false;
            }
            tempGraph.edges = new List<Edge>(edges);
            return tempGraph;
        }
    }
}
