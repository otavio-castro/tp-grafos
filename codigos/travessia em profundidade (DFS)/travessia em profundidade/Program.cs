using System;
using System.IO;
using Spectre.Console;

class Grafo
{
    // Representa um grafo não-direcionado com matriz de adjacência e suporte para DFS.
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

    // Adiciona aresta bidirecional entre v e u com peso especificado.
    public void AdicionarAresta(int v, int u, int peso)
    {
        matriz[v, u] = peso;
        matriz[u, v] = peso;
    }

    // Executa DFS a partir do vértice inicial e exibe a árvore de exploração e tempos formatados.
    public void DFS(int inicio)
    {
        bool[] visitado = new bool[vertices];
        descoberta = new int[vertices];
        termino = new int[vertices];
        tempo = 0;

        AnsiConsole.Write(
            new Rule("[yellow]Árvore DFS[/]")
                .RuleStyle("grey")
                .LeftJustified());

        var tree = new Tree("[cyan]Estrutura da Árvore DFS[/]");
        DFSUtil(inicio, visitado, -1, tree, null);
        AnsiConsole.Write(tree);

        // Tabela de tempos
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue);

        table.AddColumn(new TableColumn("[yellow]Vértice[/]").Centered());
        table.AddColumn(new TableColumn("[green]Descoberta[/]").Centered());
        table.AddColumn(new TableColumn("[red]Término[/]").Centered());

        for (int i = 0; i < vertices; i++)
        {
            if (descoberta[i] > 0)
            {
                table.AddRow(
                    $"[cyan]{i}[/]",
                    $"[green]{descoberta[i]}[/]",
                    $"[red]{termino[i]}[/]"
                );
            }
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(
            new Rule("[yellow]Tempos de Descoberta e Término[/]")
                .RuleStyle("grey")
                .LeftJustified());
        AnsiConsole.Write(table);
    }

    // Função recursiva da DFS que visita vértices, registra tempos e constrói a árvore visual.
    private void DFSUtil(int v, bool[] visitado, int pai, Tree tree, TreeNode? parentNode)
    {
        visitado[v] = true;
        tempo++;
        descoberta[v] = tempo;

        TreeNode currentNode;
        if (pai == -1)
        {
            currentNode = tree.AddNode($"[bold green]Raiz: {v}[/] [dim][/]");
        }
        else
        {
            currentNode = parentNode!.AddNode($"[cyan]{v}[/] [dim][/]");
        }

        for (int u = 0; u < vertices; u++)
        {
            if (matriz[v, u] > 0 && !visitado[u])
            {
                DFSUtil(u, visitado, v, tree, currentNode);
            }
        }

        tempo++;
        termino[v] = tempo;
    }

    // Exibe tabela formatada com todas as arestas do grafo evitando duplicatas.
    public void ImprimirArestas()
    {
        var table = new Table()
            .Border(TableBorder.Double)
            .BorderColor(Color.Green);

        table.AddColumn(new TableColumn("[yellow]Vértice U[/]").Centered());
        table.AddColumn(new TableColumn("[yellow]Vértice V[/]").Centered());
        table.AddColumn(new TableColumn("[yellow]Peso[/]").Centered());

        for (int i = 0; i < vertices; i++)
        {
            for (int j = i + 1; j < vertices; j++)
            {
                if (matriz[i, j] > 0)
                {
                    table.AddRow(
                        $"[cyan]{i}[/]",
                        $"[cyan]{j}[/]",
                        $"[green]{matriz[i, j]}[/]"
                    );
                }
            }
        }

        AnsiConsole.Write(
            new Rule("[bold blue]Arestas do Grafo[/]")
                .RuleStyle("grey")
                .LeftJustified());
        AnsiConsole.Write(table);
    }
}

class Program
{
    // Carrega grafo do arquivo, executa DFS e exibe resultados com interface visual animada.
    static void Main(string[] args)
    {
        try
        {
            AnsiConsole.Write(
                new FigletText("DFS Grafo")
                    .LeftJustified()
                    .Color(Color.Blue));

            AnsiConsole.Status()
                .Start("Carregando grafo...", ctx =>
                {
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    string[] linhas = File.ReadAllLines("grafo.txt");
                    int numVertices = int.Parse(linhas[0]);

                    ctx.Status($"Criando grafo com {numVertices} vértices...");
                    Grafo g = new Grafo(numVertices);

                    ctx.Status("Adicionando arestas...");
                    for (int i = 1; i < linhas.Length; i++)
                    {
                        string[] partes = linhas[i].Split(' ');
                        int v = int.Parse(partes[0]);
                        int u = int.Parse(partes[1]);
                        int peso = int.Parse(partes[2]);
                        g.AdicionarAresta(v, u, peso);
                    }

                    ctx.Status("Processando grafo...");
                    System.Threading.Thread.Sleep(500); 

                    AnsiConsole.MarkupLine("[green]✓[/] Grafo carregado com sucesso!");
                    AnsiConsole.WriteLine();

                    g.ImprimirArestas();
                    AnsiConsole.WriteLine();
                    g.DFS(0);
                });

            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[dim]Pressione qualquer tecla para sair...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }
}