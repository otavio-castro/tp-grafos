using Amplitude.Models;
using Amplitude.UI.Interfaces;
using Spectre.Console;

namespace Amplitude.UI;

public class BFSUI : IBFSUI
{
    public void ExibirResultadoBFS(ResultadoBFS resultado)
    {
        ExibirAnimacaoInicio();
        ExibirCabecalho();
        ExibirPassos(resultado.Passos);
        AnsiConsole.WriteLine();
        ExibirOrdemVisitacao(resultado.OrdemVisitacao);
        AnsiConsole.WriteLine();
        ExibirNiveis(resultado.Niveis);
    }

    private void ExibirAnimacaoInicio()
    {
        AnsiConsole.Status()
            .Start("[yellow]Executando BFS...[/]", ctx =>
            {
                Thread.Sleep(500);
            });
    }

    private void ExibirCabecalho()
    {
        AnsiConsole.Write(
            new Rule("[bold green]Travessia em Amplitude (BFS)[/]")
                .RuleStyle("green"));
    }

    private void ExibirPassos(List<PassoBFS> passos)
    {
        var tabela = new Table();
        tabela.Border(TableBorder.Rounded);
        tabela.AddColumn("[cyan]Passo[/]");
        tabela.AddColumn("[cyan]Vértice Atual[/]");
        tabela.AddColumn("[cyan]Fila[/]");
        tabela.AddColumn("[cyan]Visitados[/]");

        foreach (var passo in passos)
        {
            var filaStr = passo.EstadoFila.Count > 0 
                ? string.Join(", ", passo.EstadoFila) 
                : "vazia";
            
            var visitadosStr = string.Join(", ", passo.VerticesVisitados);

            tabela.AddRow(
                $"[white]{passo.NumeroPasso}[/]",
                $"[yellow]{passo.VerticeAtual}[/]",
                $"[grey]{filaStr}[/]",
                $"[green]{visitadosStr}[/]");
        }

        AnsiConsole.Write(tabela);
    }

    private void ExibirOrdemVisitacao(List<string> ordemVisitacao)
    {
        var painel = new Panel(
            new Markup($"[white]{string.Join(" ? ", ordemVisitacao)}[/]"))
            .Header("[bold cyan]Ordem de Visitação[/]")
            .BorderColor(Color.Cyan1);

        AnsiConsole.Write(painel);
    }

    private void ExibirNiveis(Dictionary<string, int> niveis)
    {
        var tabela = new Table();
        tabela.Border(TableBorder.Rounded);
        tabela.AddColumn("[cyan]Vértice[/]");
        tabela.AddColumn("[cyan]Nível[/]");

        var niveisAgrupados = niveis
            .GroupBy(n => n.Value)
            .OrderBy(g => g.Key);

        foreach (var grupo in niveisAgrupados)
        {
            var vertices = string.Join(", ", grupo.Select(v => v.Key).OrderBy(v => v));
            var cor = grupo.Key switch
            {
                0 => "yellow",
                1 => "green",
                2 => "blue",
                3 => "magenta",
                _ => "white"
            };
            tabela.AddRow($"[{cor}]{vertices}[/]", $"[white]{grupo.Key}[/]");
        }

        AnsiConsole.Write(
            new Panel(tabela)
                .Header("[bold blue]Níveis de Distância[/]")
                .BorderColor(Color.Blue));
    }
}
