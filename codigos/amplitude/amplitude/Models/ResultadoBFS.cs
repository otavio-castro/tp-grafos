namespace Amplitude.Models;

public class ResultadoBFS
{
    public List<string> OrdemVisitacao { get; set; } = new();
    public Dictionary<string, int> Niveis { get; set; } = new();
    public List<PassoBFS> Passos { get; set; } = new();
}

public class PassoBFS
{
    public int NumeroPasso { get; set; }
    public string VerticeAtual { get; set; } = string.Empty;
    public List<string> EstadoFila { get; set; } = new();
    public List<string> VerticesVisitados { get; set; } = new();
}
