using Amplitude.Models;
using Amplitude.UI.Interfaces;
using Spectre.Console;

namespace Amplitude.UI;

public class GrafoUI : IGrafoUI
{
    public void ExibirTitulo()
    {
        AnsiConsole.Write(
            new FigletText("BFS - Busca em Amplitude")
                .LeftJustified()
                .Color(Color.Cyan1));
    }

    public void ExibirGrafo(Grafo grafo)
    {
        var tabela = new Table();
        tabela.Border(TableBorder.Rounded);
        tabela.AddColumn(new TableColumn("[cyan]Vértice[/]").Centered());
        tabela.AddColumn(new TableColumn("[cyan]Adjacências[/]").LeftAligned());

        foreach (var vertice in grafo.ObterTodosVertices())
        {
            var adjacencias = string.Join(", ", grafo.ObterAdjacentes(vertice));
            tabela.AddRow($"[yellow]{vertice}[/]", $"[white]{adjacencias}[/]");
        }

        AnsiConsole.Write(
            new Panel(tabela)
                .Header("[bold blue]Estrutura do Grafo[/]")
                .BorderColor(Color.Blue));
    }

    public void ExibirMensagemSucesso(string mensagem)
    {
        AnsiConsole.MarkupLine($"[green]? {mensagem}[/]");
    }

    public void ExibirMensagemErro(string mensagem)
    {
        AnsiConsole.MarkupLine($"[red]? {mensagem}[/]");
    }

    public string SolicitarVerticeInicial()
    {
        AnsiConsole.WriteLine();
        return AnsiConsole.Ask<string>("[yellow]Digite o vértice inicial para a busca:[/]");
    }

    public void AguardarTecla()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[green]Pressione qualquer tecla para sair...[/]");
        Console.ReadKey();
    }
}
