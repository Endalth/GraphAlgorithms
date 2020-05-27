using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms
{
    class Vertex
    {
        public string vertexName;
        public bool visited;

        public Vertex(string vertexName) => this.vertexName = vertexName;
    }
}
