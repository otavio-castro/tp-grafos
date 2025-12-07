using System;

class Grafo
{
    private int vertices;
    private int[,] matriz;
    private int tempo;
    private int[] descoberta;
    private int[] termino;

    public Grafo(int v)
    {
        vertices = v;
        matriz = new int[v, v];
    }

    
    public void AdicionarAresta(int v, int u, int peso)
    {
        matriz[v, u] = peso;
        matriz[u, v] = peso; 
    }

    public void DFS(int inicio)
    {
        bool[] visitado = new bool[vertices];
        descoberta = new int[vertices];
        termino = new int[vertices];
        tempo = 0;

        Console.WriteLine("Árvore DFS:");
        DFSUtil(inicio, visitado, -1);

        Console.WriteLine("\nTempos de Descoberta e Término:");
        for (int i = 0; i < vertices; i++)
        {
            if (descoberta[i] > 0)
                Console.WriteLine($"{i}: ({descoberta[i]},{termino[i]})");
        }
    }

    private void DFSUtil(int v, bool[] visitado, int pai)
    {
        visitado[v] = true;
        tempo++;
        descoberta[v] = tempo;

        if (pai != -1)
        {
            Console.WriteLine($"{pai} -- {v}");
        }
        else
        {
            Console.WriteLine($"Raiz: {v}");
        }

        for (int u = 0; u < vertices; u++)
        {
            if (matriz[v, u] > 0 && !visitado[u])
            {
                DFSUtil(u, visitado, v);
            }
        }

        tempo++;
        termino[v] = tempo;
    }

    public void ImprimirArestas()
    {
        Console.WriteLine("Arestas (u, v, peso):");

        for (int i = 0; i < vertices; i++)
        {
            for (int j = i + 1; j < vertices; j++)
            {
                if (matriz[i, j] > 0)
                    Console.WriteLine($"{i} -- {j}  (peso {matriz[i, j]})");
            }
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        string[] linhas = File.ReadAllLines("grafo.txt");

        int numVertices = int.Parse(linhas[0]);
        Grafo g = new Grafo(numVertices);

        for (int i = 1; i < linhas.Length; i++)
        {
            string[] partes = linhas[i].Split(' ');
            int v = int.Parse(partes[0]);
            int u = int.Parse(partes[1]);
            int peso = int.Parse(partes[2]);
            g.AdicionarAresta(v, u, peso);
        }


        g.ImprimirArestas();
        Console.WriteLine();
        g.DFS(0);
    }
}
