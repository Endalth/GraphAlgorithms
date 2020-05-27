using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = CreateUndirectedGraph();

            List<IGraphAlgorithm> graphAlgorithms = new List<IGraphAlgorithm>();
            //Minimum Spanning Tree(MST) Algorithms - for undirected graphs
            graphAlgorithms.Add(new KruskalAlgorithm());
            graphAlgorithms.Add(new PrimAlgorithm());

            //Single Source Shortest Path Algorithm
            graphAlgorithms.Add(new DjisktraAlgorithm());
            graphAlgorithms.Add(new BellmanFordAlgorithm()); //Works with negative weights

            //All Pairs Shortest Path Algorithm
            graphAlgorithms.Add(new FloydWarshallAlgorithm());

            Stopwatch stopwatch = new Stopwatch();
            foreach (IGraphAlgorithm graphAlgorithm in graphAlgorithms)
            {
                Graph tempGraph = graph.Copy();

                //stopwatch.Restart();
                graphAlgorithm.Run(tempGraph);
                //stopwatch.Stop();
                //Console.WriteLine(stopwatch.ElapsedMilliseconds);

                PrintEdges(tempGraph);
            }

            Console.ReadKey();
        }

        public static Graph CreateUndirectedGraph()
        {
            Graph graph = new Graph(true);

            Vertex a = new Vertex("a");
            Vertex b = new Vertex("b");
            Vertex c = new Vertex("c");
            Vertex d = new Vertex("d");
            Vertex e = new Vertex("e");
            Vertex f = new Vertex("f");
            Vertex g = new Vertex("g");
            Vertex h = new Vertex("h");
            Vertex i = new Vertex("i");

            graph.vertices = new List<Vertex>() { a, b, c, d, e, f, g, h, i };
            graph.edges = new List<Edge>()
            {
                new Edge(a, 4, b),
                new Edge(a, 8, h),
                new Edge(b, 8, c),
                new Edge(b, 11, h),
                new Edge(c, 7, d),
                new Edge(c, 4, f),
                new Edge(c, 2, i),
                new Edge(d, 9, e),
                new Edge(d, 14, f),
                new Edge(e, 10, f),
                new Edge(f, 2, g),
                new Edge(g, 1, h),
                new Edge(g, 6, i),
                new Edge(h, 7, i)
            };

            return graph;
        }

        public static Graph CreateDirectedGraph()
        {
            Graph graph = new Graph(false);

            Vertex a = new Vertex("A");
            Vertex b = new Vertex("B");
            Vertex c = new Vertex("C");
            Vertex d = new Vertex("D");

            graph.vertices = new List<Vertex>() { a, b, c, d };
            graph.edges = new List<Edge>()
            {
                new Edge(a, 3, c),
                new Edge(a, 1, d),
                new Edge(b, 2, a),
                new Edge(b, 1, d),
                new Edge(c, 5, d),
                new Edge(d, 4, b)
            };

            return graph;
        }

        public static Graph CreateDirectedNWGraph() //NW = Negative Weights
        {
            Graph graph = new Graph(false);

            Vertex a = new Vertex("A");
            Vertex b = new Vertex("B");
            Vertex c = new Vertex("C");
            Vertex d = new Vertex("D");
            Vertex e = new Vertex("E");

            graph.vertices = new List<Vertex>() { a, b, c, d, e };
            graph.edges = new List<Edge>()
            {
                new Edge(a, -1, b),
                new Edge(a, 4, c),
                new Edge(b, 3, c),
                new Edge(b, 2, d),
                new Edge(b, 2, e),
                new Edge(d, 1, b),
                new Edge(d, 5, c),
                new Edge(e, -3, d)
            };

            return graph;
        }

        public static void PrintEdges(Graph graph)
        {
            foreach (var item in graph.edges)
            {
                Console.WriteLine(item.vertex.vertexName + " " + item.distance + " " + item.connectedVertex.vertexName);
            }
            Console.WriteLine();
        }
    }
}
