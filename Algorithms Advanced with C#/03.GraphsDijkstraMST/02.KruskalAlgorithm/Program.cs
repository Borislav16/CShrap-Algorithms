using System;
using System.Linq;
using System.Collections.Generic;

namespace _02.KruskalAlgorithm
{

    public class Edge
    {
        public Edge(int first, int second, int weight)
        {
            First = first;
            Second = second;
            Weight = weight;
        }

        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge> edges;

        static void Main(string[] args)
        {
            int edgeQty = int.Parse(Console.ReadLine());

            edges = ReadEdges(edgeQty);

            List<Edge> sortedEdges = edges
                .OrderBy(edge => edge.Weight)
                .ToList();

            HashSet<int> nodes = edges
                .Select(edge => edge.First)
                .Union(edges.Select(edge => edge.Second))
                .ToHashSet();

            int[] parents = Enumerable.Repeat(-1, nodes.Max() + 1)
                .ToArray();

            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            foreach (var edge in sortedEdges)
            {
                int firstNodeRoot = GetRoot(parents, edge.First);
                int secondNodeRoot = GetRoot(parents, edge.Second);

                if (firstNodeRoot != secondNodeRoot)
                {
                    Console.WriteLine($"{edge.First} - {edge.Second}");
                    parents[firstNodeRoot] = secondNodeRoot;
                }
            }
        }

        private static int GetRoot(int[] parents, int node)
        {
            while(node != parents[node])
            {
                node = parents[node];
            }

            return node;
        }

        private static List<Edge> ReadEdges(int edgeQty)
        {
            List<Edge> result = new List<Edge>();
            for (int i = 0; i < edgeQty; i++)
            {
                int[] edgeData = Console.ReadLine()
                    .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int firstNode = edgeData[0];
                int secondNode = edgeData[1];
                int weight = edgeData[2];

                result.Add(new Edge(firstNode, secondNode, weight));
            }

            return result;
        }
    }
}
