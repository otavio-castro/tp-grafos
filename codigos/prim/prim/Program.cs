using Spectre.Console;

public class Program
{
    private static void Main()
    {
        string caminho = "TextFile1.txt";
        int[,] grafo = LerGrafoDoArquivo(caminho);

        var mst = Prim(grafo);

        AnsiConsole.MarkupLine("[bold underline green]Árvore Geradora Mínima (Prim)[/]");
        var tree = new Tree("[yellow]Grafo[/]");

        foreach (var e in mst)
            tree.AddNode($"[cyan]{e.origem}[/] — [cyan]{e.destino}[/]  (peso: [green]{e.peso}[/])");

        AnsiConsole.Write(tree);
    }

    private static int[,] LerGrafoDoArquivo(string caminho)
    {
        var linhas = File.ReadAllLines(caminho);
        int n = linhas.Length;
        int[,] matriz = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            var valores = linhas[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < n; j++)
                matriz[i, j] = int.Parse(valores[j]);
        }

        return matriz;
    }

    private static List<(int origem, int destino, int peso)> Prim(int[,] grafo)
    {
        int n = grafo.GetLength(0);
        bool[] visitado = new bool[n];
        List<(int origem, int destino, int peso)> resultado = new();

        visitado[0] = true;

        for (int k = 0; k < n - 1; k++)
        {
            int menor = int.MaxValue;
            int origem = -1;
            int destino = -1;

            for (int i = 0; i < n; i++)
            {
                if (!visitado[i]) continue;

                for (int j = 0; j < n; j++)
                {
                    if (!visitado[j] && grafo[i, j] != 0 && grafo[i, j] < menor)
                    {
                        menor = grafo[i, j];
                        origem = i;
                        destino = j;
                    }
                }
            }

            if (destino == -1)
                break; // Grafo desconexo

            visitado[destino] = true;
            resultado.Add((origem, destino, menor));
        }

        return resultado;
    }
}