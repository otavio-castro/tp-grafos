using Amplitude.Models;

namespace Amplitude.Services.Interfaces;

public interface IGrafoService
{
    Grafo CarregarGrafoDeArquivo(string caminhoArquivo);
    bool ValidarGrafo(Grafo grafo);
}
