using Amplitude.Models;
using Amplitude.Services;
using Amplitude.Services.Interfaces;
using Amplitude.UI;
using Amplitude.UI.Interfaces;
using Spectre.Console;

namespace Amplitude;

class Program
{
    static void Main(string[] args)
    {
        // Injeção de dependências manual (pode ser substituído por DI Container)
        IGrafoService grafoService = new GrafoService();
        IBFSService bfsService = new BFSService();
        IGrafoUI grafoUI = new GrafoUI();
        IBFSUI bfsUI = new BFSUI();

        try
        {
            // Exibir título
            grafoUI.ExibirTitulo();

            // Selecionar arquivo de grafo
            var arquivoGrafo = SelecionarArquivoGrafo();

            // Carregar grafo
            var grafo = grafoService.CarregarGrafoDeArquivo(arquivoGrafo);
            
            if (!grafoService.ValidarGrafo(grafo))
            {
                grafoUI.ExibirMensagemErro("Não foi possível carregar o grafo!");
                return;
            }

            grafoUI.ExibirMensagemSucesso($"Grafo carregado com sucesso! ({grafo.TotalVertices} vértices)");

            // Exibir estrutura do grafo
            grafoUI.ExibirGrafo(grafo);

            // Solicitar vértice inicial
            var verticeInicial = grafoUI.SolicitarVerticeInicial();

            if (!grafo.ContemVertice(verticeInicial))
            {
                grafoUI.ExibirMensagemErro("Vértice não encontrado no grafo!");
                return;
            }

            // Executar BFS
            var resultado = bfsService.ExecutarBFS(grafo, verticeInicial);

            // Exibir resultado
            bfsUI.ExibirResultadoBFS(resultado);

            // Aguardar tecla
            grafoUI.AguardarTecla();
        }
        catch (Exception ex)
        {
            grafoUI.ExibirMensagemErro($"Erro: {ex.Message}");
        }
    }

    static string SelecionarArquivoGrafo()
    {
        var opcao = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Selecione o arquivo de grafo:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Grafo Padrão",
                    "Rede Social",
                    "Mapa de Cidades"
                }));

        return opcao switch
        {
            "Grafo Padrão" => "Data/grafo.txt",
            "Rede Social" => "Data/grafo_rede_social.txt",
            "Mapa de Cidades" => "Data/grafo_cidades.txt",
            _ => "Data/grafo.txt"
        };
    }
}
