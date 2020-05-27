using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms
{
    class Edge
    {
        public Vertex vertex;
        public int distance;
        public Vertex connectedVertex;

        public Edge(Vertex vertex, int distance, Vertex connectedVertex)
        {
            this.vertex = vertex;
            this.connectedVertex = connectedVertex;
            this.distance = distance;
        }
    }
}
