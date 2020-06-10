using System;
using System.Collections.Generic;
using System.Text;

//Single source shortest path algorihm - can be used with negative weights
namespace GraphAlgorithms
{
    class BellmanFordAlgorithm : IGraphAlgorithm
    {
        public void Run(Graph graph)
        {
            Dictionary<Vertex, int> vertexDistance = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> vertexParent = new Dictionary<Vertex, Vertex>();
            List<Edge> edgeList = new List<Edge>();

            for (int i = 0; i < graph.vertices.Count; i++)
            {
                vertexDistance.Add(graph.vertices[i], 99);
                vertexParent.Add(graph.vertices[i], null);
            }

            vertexDistance[graph.vertices[0]] = 0;

            //Edge Relaxation - this guarantees shortest distances
            for (int i = 0; i < graph.vertices.Count - 1; i++)
            {
                for (int j = 0; j < graph.edges.Count; j++)
                {
                    Vertex src = graph.edges[j].vertex;
                    Vertex dst = graph.edges[j].connectedVertex;
                    int edgeWeight = graph.edges[j].distance;

                    if (vertexDistance[src] + edgeWeight < vertexDistance[dst])
                    {
                        vertexDistance[dst] = vertexDistance[src] + edgeWeight;
                        vertexParent[dst] = src;
                    }

                    //Undirected edges go both ways so we also need the check the opposite
                    if (graph.undirected)
                    {
                        src = graph.edges[j].connectedVertex;
                        dst = graph.edges[j].vertex;

                        if (vertexDistance[src] + edgeWeight < vertexDistance[dst])
                        {
                            vertexDistance[dst] = vertexDistance[src] + edgeWeight;
                            vertexParent[dst] = src;
                        }
                    }
                }
            }

            //Negative Cycle Check - if we get a shorter path then there is a cycle
            for (int j = 0; j < graph.edges.Count; j++)
            {
                Vertex src = graph.edges[j].vertex;
                Vertex dst = graph.edges[j].connectedVertex;
                int edgeWeight = graph.edges[j].distance;

                if (vertexDistance[src] + edgeWeight < vertexDistance[dst])
                {
                    Console.WriteLine("Graph contains negative weight cycle");
                }

                if (graph.undirected)
                {
                    src = graph.edges[j].connectedVertex;
                    dst = graph.edges[j].vertex;

                    if (vertexDistance[src] + edgeWeight < vertexDistance[dst])
                    {
                        Console.WriteLine("Graph contains negative weight cycle");
                    }
                }
            }


            //Edge Finding
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
