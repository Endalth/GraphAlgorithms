using System;
using System.Collections.Generic;
using System.Text;

//all pairs shortest path algorithm - can be used with negative weights
namespace GraphAlgorithms
{
    class FloydWarshallAlgorithm : IGraphAlgorithm
    {
        public void Run(Graph graph)
        {
            int[,] connections = new int[graph.vertices.Count, graph.vertices.Count];
            Vertex[,] parents = new Vertex[graph.vertices.Count, graph.vertices.Count];

            List<Edge> edgeList = new List<Edge>();

            //Setting up the array
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                for (int j = 0; j < graph.vertices.Count; j++)
                {
                    if (i != j)
                    {
                        Edge edge = null;
                        //Finding the edge
                        if (graph.undirected)
                            edge = graph.edges.Find(x => x.vertex == graph.vertices[i] && x.connectedVertex == graph.vertices[j] || x.connectedVertex == graph.vertices[i] && x.vertex == graph.vertices[j]);
                        else
                            edge = graph.edges.Find(x => x.vertex == graph.vertices[i] && x.connectedVertex == graph.vertices[j]);

                        if (edge == null)
                        {
                            connections[i, j] = 99;

                            if (graph.undirected)
                                connections[j, i] = 99;
                        }
                        else
                        {
                            if (graph.undirected)
                            {
                                connections[i, j] = edge.distance;
                                connections[j, i] = edge.distance;
                                //Since in an undirected graph source can be on either end we need to know which one is source
                                if (edge.vertex == graph.vertices[i])
                                {
                                    parents[i, j] = edge.vertex;//[i,j] is destination index and "= vertex(source)" means we use this vertex to reach destination
                                    parents[j, i] = edge.connectedVertex;
                                }
                                else
                                {
                                    parents[i, j] = edge.connectedVertex;
                                    parents[j, i] = edge.vertex;
                                }
                            }
                            else
                            {
                                connections[i, j] = edge.distance;
                                parents[i, j] = edge.vertex;
                            }
                        }
                    }
                }
            }

            //Calculating shortest paths
            for (int k = 0; k < graph.vertices.Count; k++)
            {
                //Pick all vertices as source one by one
                for (int i = 0; i < graph.vertices.Count; i++)
                {
                    //Check if vertex k is on the path from i to j
                    for (int j = 0; j < graph.vertices.Count; j++)
                    {
                        if (connections[i, k] + connections[k, j] < connections[i, j])
                        {
                            connections[i, j] = connections[i, k] + connections[k, j];
                            parents[i, j] = parents[k,j];
                        }
                    }
                }
            }


            PrintArray(graph, parents);
            PrintArrayDistance(graph, connections);

            //Edge Finding
            int source = 0; //Different source can be chosen to get different paths
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                Edge edge = null;
                if (graph.undirected)
                    edge = graph.edges.Find(x => x.vertex == parents[source, i] && x.connectedVertex == graph.vertices[i] || x.vertex == graph.vertices[i] && x.connectedVertex == parents[source, i]);
                else
                    edge = graph.edges.Find(x => x.vertex == parents[source, i] && x.connectedVertex == graph.vertices[i]);

                if (edge != null)
                    edgeList.Add(edge);
            }

            graph.edges = edgeList;
        }

        private void PrintArray(Graph graph, Vertex[,] connections)
        {
            Console.Write("   ");
            for (int j = 0; j < graph.vertices.Count; j++)
            {
                Console.Write(graph.vertices[j].vertexName.PadRight(4, ' '));
            }
            Console.WriteLine();

            //Print connections
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                Console.Write(graph.vertices[i].vertexName.PadRight(3, ' '));

                for (int j = 0; j < graph.vertices.Count; j++)
                {
                    Console.Write((connections[i, j] == null ? "" : connections[i, j].vertexName).PadRight(4, ' '));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintArrayDistance(Graph graph, int[,] connections)
        {
            Console.Write("   ");
            for (int j = 0; j < graph.vertices.Count; j++)
            {
                Console.Write(graph.vertices[j].vertexName.PadRight(4, ' '));
            }
            Console.WriteLine();

            //Print connections
            for (int i = 0; i < graph.vertices.Count; i++)
            {
                Console.Write(graph.vertices[i].vertexName.PadRight(3, ' '));

                for (int j = 0; j < graph.vertices.Count; j++)
                {
                    Console.Write(connections[i, j].ToString().PadRight(4, ' '));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
