using Amplitude.Models;

namespace Amplitude.Services.Interfaces;

public interface IBFSService
{
    ResultadoBFS ExecutarBFS(Grafo grafo, string verticeInicial);
}
