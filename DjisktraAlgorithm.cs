using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Single source shortest path algorihm for undirected graphs
namespace GraphAlgorithms
{
    class DjisktraAlgorithm : IGraphAlgorithm
    {
        public void Run(Graph graph)
        {
            Dictionary<Vertex, int> vertexDistances = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> vertexParent = new Dictionary<Vertex, Vertex>();
            List<Edge> edgeList = new List<Edge>();

            for (int i = 0; i < graph.vertices.Count; i++)
            {
                vertexDistances.Add(graph.vertices[i], 99999);
                vertexParent.Add(graph.vertices[i], null);
            }

            vertexDistances[graph.vertices[0]] = 0;

            while (vertexDistances.Count > 0)
            {
                //Find the vertex with smallest value in the dictionary
                Vertex currentVertex = vertexDistances.Aggregate((x, y) => x.Value <= y.Value ? x : y).Key;
                currentVertex.visited = true;

                //Find all the edges that belong to the currentVertex and doesn't point towards a visited vertex
                List<Edge> currentVertexEdges = null;
                if (graph.undirected)
                    currentVertexEdges = graph.edges.FindAll(x => (x.vertex == currentVertex && !x.connectedVertex.visited) || (x.connectedVertex == currentVertex && !x.vertex.visited));
                else
                    currentVertexEdges = graph.edges.FindAll(x => x.vertex == currentVertex && !x.connectedVertex.visited);

                //Update distances and parents
                foreach (Edge edge in currentVertexEdges)
                {
                    if (edge.vertex == currentVertex)
                    {
                        if (vertexDistances[currentVertex] + edge.distance < vertexDistances[edge.connectedVertex])
                        {
                            vertexDistances[edge.connectedVertex] = vertexDistances[currentVertex] + edge.distance;
                            vertexParent[edge.connectedVertex] = currentVertex;
                        }
                    }
                    else
                    {
                        if (vertexDistances[currentVertex] + edge.distance < vertexDistances[edge.vertex])
                        {
                            vertexDistances[edge.vertex] = vertexDistances[currentVertex] + edge.distance;
                            vertexParent[edge.vertex] = currentVertex;
                        }
                    }
                }

                vertexDistances.Remove(currentVertex);
            }

            //Create the edgeList by finding the edges in vertexParent
            foreach (var item in vertexParent)
            {
                if (item.Value != null)
                {
                    Edge edge = null;
                    if (graph.undirected)
                        edge = graph.edges.Find(x => x.vertex == item.Key && x.connectedVertex == item.Value || x.connectedVertex == item.Key && x.vertex == item.Value);
                    else
                        edge = graph.edges.Find(x => x.vertex == item.Value && x.connectedVertex == item.Key);

                    edgeList.Add(edge);
                }
            }

            graph.edges = edgeList;
        }
    }
}
