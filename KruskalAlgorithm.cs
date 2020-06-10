using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Greedy minimum spanning tree algorithm for undirected graphs
namespace GraphAlgorithms
{
    class KruskalAlgorithm : IGraphAlgorithm
    {
        public void Run(Graph graph)
        {
            //Step 1: Sort the graph edges in ascending order by edge weight/distance
            QuickSortAlg(graph.edges, 0, graph.edges.Count - 1);

            //Step 2: remove unneccessary edges
            graph.edges = EdgeRemovalForestList(graph);
        }

        //Step 2: edge removal with forest list
        public List<Edge> EdgeRemovalForestList(Graph graph)
        {
            List<Edge> edgeList = new List<Edge>();

            List<List<Vertex>> forest = new List<List<Vertex>>();

            foreach (var item in graph.vertices)
            {
                forest.Add(new List<Vertex>() { item });
            }

            //Remove unneccessary edges
            foreach (Edge edge in graph.edges)
            {
                //Find the trees that contain these vertices
                int vertexIndex = FindVertexIndex(forest, edge.vertex);
                int connectedVertexIndex = FindVertexIndex(forest, edge.connectedVertex);

                //If indexes are different that means they are in different trees and we can safely add this edge to combine them
                if (vertexIndex != connectedVertexIndex)
                {
                    //Combine the trees
                    forest[vertexIndex].AddRange(forest[connectedVertexIndex]);

                    //Remove the unneccessary tree
                    forest.RemoveAt(connectedVertexIndex);

                    //Add edge to the list
                    edgeList.Add(edge);

                    edge.vertex.visited = true;
                    edge.connectedVertex.visited = true;
                }
            }

            return edgeList;
        }

        //Step 2 alternative: edge removal with adjacency matrix
        public List<Edge> EdgeRemovalAdjMatrix(Graph graph)
        {
            List<Edge> edgeList = new List<Edge>();

            byte[,] adjacencyMatrix = new byte[graph.vertices.Count, graph.vertices.Count];

            //Remove unneccessary edges
            foreach (Edge edge in graph.edges)
            {
                //If one side of the edge is not visited take the edge or if edge connects two seperate trees
                if (!edge.vertex.visited || !edge.connectedVertex.visited || adjacencyMatrix[graph.vertices.IndexOf(edge.vertex), graph.vertices.IndexOf(edge.connectedVertex)] == 0)
                {
                    edgeList.Add(edge);
                    edge.vertex.visited = true;
                    edge.connectedVertex.visited = true;

                    int vertexIndex = graph.vertices.IndexOf(edge.vertex);
                    int connectedVertexIndex = graph.vertices.IndexOf(edge.connectedVertex);

                    //Using adjacency matrix to keep track of all the nodes you can reach from a node
                    //if a node has 0 for another node that means its in another tree
                    adjacencyMatrix[vertexIndex, connectedVertexIndex] = 1;
                    adjacencyMatrix[connectedVertexIndex, vertexIndex] = 1;
                    for (int i = 0; i < graph.vertices.Count; i++)
                    {
                        for (int j = 0; j < graph.vertices.Count; j++)
                        {
                            if (adjacencyMatrix[i, j] == 1)
                            {
                                for (int k = 0; k < graph.vertices.Count; k++)
                                {
                                    if (adjacencyMatrix[j, k] == 1)
                                        adjacencyMatrix[i, k] = 1;
                                }
                            }
                        }
                    }
                }
            }

            return edgeList;
        }

        public int FindVertexIndex(List<List<Vertex>> forest, Vertex vertex)
        {
            for (int i = 0; i < forest.Count; i++)
            {
                if (forest[i].Contains(vertex))
                    return i;
            }

            return -1;
        }

        public void QuickSortAlg(List<Edge> array, int left, int right)
        {
            int i = left, j = right;
            int pivot = array[(left + right) / 2].distance;

            //Partition
            while (i <= j)
            {
                while (array[i].distance < pivot)
                    i++;
                while (array[j].distance > pivot)
                    j--;
                if (i <= j)
                {
                    Edge temporary = array[i];
                    array[i] = array[j];
                    array[j] = temporary;
                    i++;
                    j--;
                }
            };

            //Recursion
            if (left < j)
                QuickSortAlg(array, left, j);
            if (i < right)
                QuickSortAlg(array, i, right);
        }
    }
}