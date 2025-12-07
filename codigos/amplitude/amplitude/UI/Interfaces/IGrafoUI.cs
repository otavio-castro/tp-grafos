using Amplitude.Models;

namespace Amplitude.UI.Interfaces;

public interface IGrafoUI
{
    void ExibirTitulo();
    void ExibirGrafo(Grafo grafo);
    void ExibirMensagemSucesso(string mensagem);
    void ExibirMensagemErro(string mensagem);
    string SolicitarVerticeInicial();
    void AguardarTecla();
}
