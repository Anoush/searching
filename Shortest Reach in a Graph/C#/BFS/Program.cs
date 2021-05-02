using System;
using System.Text;

public class Graph
{

    // find vertex with minimun distance values, from the set of vertices
    int minDistance(int[] dist, bool[] set, int V)
    {
        int min = int.MaxValue, min_index = -1;

        for (int i = 0; i < V; i++)
        {
            if (!set[i] && dist[i] <= min)
            {
                min = dist[i];
                min_index = i;
            }
        }

        return min_index;
    }

    string shortest(int[,] graph, int src, int V)
    {
        //output array
        int[] dist = new int[V];
        // processed vertex
        bool[] set = new bool[V];

        //initialize all distances as infinite with set in false
        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue;
            set[i] = false;
        }

        dist[src] = 0;

        // find shortest path for all vertices
        for (int count = 0; count < V - 1; count++)
        {
            int u = minDistance(dist, set, V); // pick the min distance
            set[u] = true; //marks the picked vertex

            // updates distance of the adjacent
            for (int v = 0; v < V; v++)
            {
                // updates if its not in set, THERE IS AN EDGE FROM U TO V, 
                // and total weight of path from src to v through u is smaller than current value of dist[v]
                if (!set[v] &&
                    graph[u, v] > 0 &&
                    dist[u] != int.MaxValue &&
                    dist[u] + graph[u, v] < dist[v])
                {
                    //dist[v] = (graph[u, v] == -1) ? graph[u, v] : dist[u] + graph[u, v];
                    dist[v] = dist[u] + graph[u, v];
                }
            }
        }

        return printSolution(src, V, dist);
    }

    private static string printSolution(int src, int V, int[] dist)
    {
        // print solution
        var sb = new StringBuilder();
        for (int i = 0; i < V; i++)
        {
            // do not include node s
            if (i != src)
            {
                // if some node is unreachable from s, it prints -1 as the distance to the node
                int d = (dist[i] == int.MaxValue ? -1 : dist[i]);
                sb.Append($"{d} ");
            }

        }
        return sb.ToString();
    }

    public static void Main()
    {
        //int[,] graph = new int[,] { { 0,6,0,0,6,-1 }, { 6,0,6,0,-1,-1 }, { 0,6,0,6,-1,-1}, {0,0,6,0,-1,-1},{6,-1,-1,-1,0,-1},{-1,-1,-1,-1,-1,0}};
        //int[,] graph = new int[,] { { 0,6,6,-1 }, { 6,0,-1,-1 },{ 6,-1,0,-1}, { 1,-1,-1,0}};
        //int[,] graph = new int[,] { { 0,-1,-1 }, { -1,0,6 },{ -1,6,0}};

        Console.Write("Number of queries: ");
        var l1 = Console.ReadLine();
        try
        {
            int q = Convert.ToInt32(l1);
            var sb = new StringBuilder();
            for (int i = 0; i < q; i++)
            {
                sb.Append(newQuery()).AppendLine();
            }
            Console.Write(sb.ToString() + "\n");


        }
        catch
        {
            Console.WriteLine("Wrong input: {0} ", l1);
        }
    }

    static string newQuery()
    {
        Console.Write("Number of nodes and edges (n and m): ");
        var l2 = Console.ReadLine();
        var nm = l2.Split(" ");
        int n = 0;
        int m = 0;
        int dist = 6;
        if (nm.Length >= 2)
        {
            n = Convert.ToInt16(nm[0]);
            m = Convert.ToInt16(nm[1]);
        }
        if (n == 0 || m == 0)
        {
            Console.WriteLine("Wrong input: {0} ", l2);
        }
        // define the size of the graph, given a number of nodes n
        int[,] graph = new int[n, n];
        // n[0,0] n[0,1] n[0,2] n[0,3]
        // n[1,0] n[1,1] n[1,2] n[1,3]
        // n[2,0] n[2,1] n[2,2] n[2,3]
        // n[3,0] n[3,1] n[3,2] n[3,3]

        var edge = 1;
        int start;
        do
        {
            Console.Write("Describe edge {0}: ", edge);
            var e = Console.ReadLine();
            var eg = e.Split(" ");
            if (eg.Length >= 2)
            {
                var u = Convert.ToUInt16(eg[0]);
                var v = Convert.ToUInt16(eg[1]);
                if (u > 0 && v > 0)
                {
                    graph[u - 1, v - 1] = dist;
                    graph[v - 1, u - 1] = dist;
                    edge++;
                }

            }

        } while (edge <= m);
        Console.Write("Start Node: ");
        var s = Console.ReadLine();
        start = Convert.ToInt16(s);
        if (start <= 0 && start >= n)
        {
            Console.WriteLine("Wrong input: {0} ", s);
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j) // start node
                {
                    graph[i, j] = 0;
                }
                else if (graph[i, j] != dist) // unreachable node
                {
                    graph[i, j] = -1;
                }
            }
        }

        Graph t = new Graph();
        return t.shortest(graph, start-1, n);

    }
}
