using Amplitude.Models;
using Amplitude.Services.Interfaces;

namespace Amplitude.Services;

public class BFSService : IBFSService
{
    public ResultadoBFS ExecutarBFS(Grafo grafo, string verticeInicial)
    {
        var resultado = new ResultadoBFS();
        var visitados = new HashSet<string>();
        var fila = new Queue<string>();

        fila.Enqueue(verticeInicial);
        visitados.Add(verticeInicial);
        resultado.Niveis[verticeInicial] = 0;

        int numeroPasso = 1;

        while (fila.Count > 0)
        {
            var verticeAtual = fila.Dequeue();
            resultado.OrdemVisitacao.Add(verticeAtual);

            // Registrar o passo atual
            var passo = new PassoBFS
            {
                NumeroPasso = numeroPasso++,
                VerticeAtual = verticeAtual,
                EstadoFila = new List<string>(fila),
                VerticesVisitados = new List<string>(visitados)
            };
            resultado.Passos.Add(passo);

            // Processar adjacentes
            var adjacentes = grafo.ObterAdjacentes(verticeAtual);
            foreach (var adjacente in adjacentes)
            {
                if (!visitados.Contains(adjacente))
                {
                    visitados.Add(adjacente);
                    fila.Enqueue(adjacente);
                    resultado.Niveis[adjacente] = resultado.Niveis[verticeAtual] + 1;
                }
            }
        }

        return resultado;
    }
}
