using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Greedy minimum spanning tree algorithm
namespace GraphAlgorithms
{
    class PrimAlgorithm : IGraphAlgorithm
    {
        public void Run(Graph graph)
        {
            Vertex currentVertex = graph.vertices[0];
            currentVertex.visited = true;

            List<Edge> edgeList = new List<Edge>();

            List<Edge> availableEdges = new List<Edge>();

            bool loopSwitch = true;
            while (loopSwitch)
            {
                //FindAll => find all the edges connected to the current vertex
                //Aggragate => from the found edges pick the one with the smallest distance
                List<Edge> currentVertexEdges = graph.edges.FindAll(x => x.vertex == currentVertex && !x.connectedVertex.visited || x.connectedVertex == currentVertex && !x.vertex.visited);

                //add found edges to the available ones
                availableEdges.AddRange(currentVertexEdges);

                if (availableEdges.Count == 0)
                    break;
                //Find minimum from available edges
                Edge selectedEdge = availableEdges.FindAll(x => !x.vertex.visited || !x.connectedVertex.visited).Aggregate((x, y) => x.distance <= y.distance ? x : y);

                //Remove selected edge from available edges
                availableEdges.Remove(selectedEdge);

                //Add the edge to the list
                edgeList.Add(selectedEdge);

                //Change currentVertex to not visited one from the selected edge
                currentVertex = selectedEdge.vertex.visited ? selectedEdge.connectedVertex : selectedEdge.vertex;
                currentVertex.visited = true;

                //If all vertices are visited finish loop
                loopSwitch = false;
                foreach (Vertex item in graph.vertices)
                {
                    if (!item.visited)
                    {
                        loopSwitch = true;
                        break;
                    }
                }
            }

            graph.edges = edgeList;
        }
    }
}
