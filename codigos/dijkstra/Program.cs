using System;
using System.IO;
using Spectre.Console;

class DijkstraAlgorithm
{
    static int V = 6;

    static int MinDistance(int[] dist, bool[] visited)
    {
        int min = int.MaxValue;
        int minIndex = -1;

        for (int v = 0; v < V; v++)
        {
            if (!visited[v] && dist[v] <= min)
            {
                min = dist[v];
                minIndex = v;
            }
        }
        return minIndex;
    }

    static void Dijkstra(int[,] graph, int src)
    {
        int[] dist = new int[V];
        bool[] visited = new bool[V];

        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue;
            visited[i] = false;
        }

        dist[src] = 0;

        for (int count = 0; count < V - 1; count++)
        {
            int u = MinDistance(dist, visited);
            visited[u] = true;

            for (int v = 0; v < V; v++)
            {
                if (!visited[v] &&
                    graph[u, v] != 0 &&
                    dist[u] != int.MaxValue &&
                    dist[u] + graph[u, v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u, v];
                }
            }
        }

        MostrarResultado(dist, src);
    }

    static void MostrarResultado(int[] dist, int src)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]Vértice[/]")
            .AddColumn("[green]Distância Mínima[/]");

        for (int i = 0; i < V; i++)
        {
            string distancia = dist[i] == int.MaxValue ? "∞ (Inatingível)" : dist[i].ToString();
            table.AddRow(i.ToString(), distancia);
        }

        AnsiConsole.MarkupLine($"\n[bold cyan]Menor distância a partir do vértice {src}:[/]");
        AnsiConsole.Write(table);
    }

    static void MostrarVertices()
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[cyan]Vértices Disponíveis[/]");

        for (int i = 0; i < V; i++)
        {
            table.AddRow(i.ToString());
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    static void MostrarArestas(int[,] graph)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[cyan]Vértice[/]")
            .AddColumn("[magenta]Conectado a[/]");

        for (int i = 0; i < V; i++)
        {
            var conexoes = new System.Collections.Generic.List<string>();
            
            for (int j = 0; j < V; j++)
            {
                if (graph[i, j] != 0)
                {
                    conexoes.Add($"V{j}({graph[i, j]})");
                }
            }

            string ligacoes = conexoes.Count > 0 ? string.Join(", ", conexoes) : "[dim]Nenhuma[/]";
            table.AddRow($"V{i}", ligacoes);
        }

        AnsiConsole.MarkupLine("\n[bold yellow]Mapa de Conexões (Peso das Arestas):[/]");
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    // Lê o grafo de um arquivo TXT
    static int[,] LerGrafoDoArquivo(string caminhoArquivo)
    {
        try
        {
            if (!File.Exists(caminhoArquivo))
            {
                AnsiConsole.MarkupLine($"[red]✗ Arquivo não encontrado: {caminhoArquivo}[/]");
                return null;
            }

            string[] linhas = File.ReadAllLines(caminhoArquivo);
            
            // Remove linhas de comentário e vazias
            var linhasValidas = new System.Collections.Generic.List<string>();
            foreach (var linha in linhas)
            {
                if (!linha.StartsWith("#") && !string.IsNullOrWhiteSpace(linha))
                {
                    linhasValidas.Add(linha.Trim());
                }
            }

            if (linhasValidas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]✗ Arquivo vazio ou contém apenas comentários![/]");
                return null;
            }

            V = int.Parse(linhasValidas[0]);
            
            if (linhasValidas.Count < V + 1)
            {
                AnsiConsole.MarkupLine($"[red]✗ Arquivo incompleto! Esperado {V} linhas de matriz, encontrado {linhasValidas.Count - 1}[/]");
                return null;
            }

            int[,] graph = new int[V, V];

            for (int i = 0; i < V; i++)
            {
                string[] valores = linhasValidas[i + 1].Split(' ');
                
                if (valores.Length != V)
                {
                    AnsiConsole.MarkupLine($"[red]✗ Erro na linha {i + 2}: esperado {V} valores, encontrado {valores.Length}[/]");
                    return null;
                }

                for (int j = 0; j < V; j++)
                {
                    graph[i, j] = int.Parse(valores[j]);
                }
            }

            AnsiConsole.MarkupLine($"[green]✓ Grafo carregado com sucesso! ({V} vértices)[/]");
            return graph;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]✗ Erro ao ler arquivo: {ex.Message}[/]");
            return null;
        }
    }

    static void Main(string[] args)
    {
        string caminhoArquivo = "graph.txt";

        AnsiConsole.MarkupLine("[bold blue]=== Algoritmo de Dijkstra ===[/]");
        AnsiConsole.MarkupLine("[dim]Encontra o caminho mais curto entre vértices[/]\n");
        
        int[,] graph = LerGrafoDoArquivo(caminhoArquivo);
        
        if (graph != null)
        {
            MostrarVertices();
            MostrarArestas(graph);
            
            int verticeInicial = AnsiConsole.Prompt(
                new TextPrompt<int>($"[yellow]Digite o vértice inicial (0 a {V-1}):[/] ")
                    .Validate(v => v >= 0 && v < V, "[red]Vértice inválido![/]")
            );

            Dijkstra(graph, verticeInicial);
        }
    }
}
