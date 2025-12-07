using Amplitude.Models;
using Amplitude.Services.Interfaces;

namespace Amplitude.Services;

public class GrafoService : IGrafoService
{
    public Grafo CarregarGrafoDeArquivo(string caminhoArquivo)
    {
        var grafo = new Grafo();

        try
        {
            // Tentar encontrar o arquivo em diferentes localizações
            var caminhoCompleto = ResolverCaminhoArquivo(caminhoArquivo);
            
            if (string.IsNullOrEmpty(caminhoCompleto))
            {
                throw new FileNotFoundException($"Arquivo não encontrado: {caminhoArquivo}");
            }

            var linhas = File.ReadAllLines(caminhoCompleto);

            foreach (var linha in linhas)
            {
                if (string.IsNullOrWhiteSpace(linha) || linha.StartsWith("#"))
                    continue;

                var partes = linha.Split(':');
                if (partes.Length != 2)
                    continue;

                var vertice = partes[0].Trim();
                var adjacentes = partes[1].Split(',')
                    .Select(a => a.Trim())
                    .Where(a => !string.IsNullOrEmpty(a))
                    .ToList();

                grafo.Adjacencias[vertice] = adjacentes;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao carregar grafo: {ex.Message}", ex);
        }

        return grafo;
    }

    public bool ValidarGrafo(Grafo grafo)
    {
        return grafo != null && grafo.TotalVertices > 0;
    }

    private string ResolverCaminhoArquivo(string caminhoRelativo)
    {
        // 1. Tentar caminho relativo ao diretório atual
        if (File.Exists(caminhoRelativo))
            return caminhoRelativo;

        // 2. Tentar caminho relativo ao diretório de execução
        var diretorioExecucao = AppDomain.CurrentDomain.BaseDirectory;
        var caminho1 = Path.Combine(diretorioExecucao, caminhoRelativo);
        if (File.Exists(caminho1))
            return caminho1;

        // 3. Tentar ir alguns níveis acima (para bin/Debug/net8.0)
        var caminho2 = Path.Combine(diretorioExecucao, "..", "..", "..", caminhoRelativo);
        if (File.Exists(caminho2))
            return Path.GetFullPath(caminho2);

        // 4. Tentar no diretório do projeto
        var diretorioProjeto = Directory.GetParent(diretorioExecucao)?.Parent?.Parent?.FullName;
        if (diretorioProjeto != null)
        {
            var caminho3 = Path.Combine(diretorioProjeto, caminhoRelativo);
            if (File.Exists(caminho3))
                return caminho3;
        }

        return string.Empty;
    }
}
