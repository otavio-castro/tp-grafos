namespace Amplitude.Models;

public class Grafo
{
    public Dictionary<string, List<string>> Adjacencias { get; set; }

    public Grafo()
    {
        Adjacencias = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
    }

    public int TotalVertices => Adjacencias.Count;

    public bool ContemVertice(string vertice) => Adjacencias.ContainsKey(vertice);

    public List<string> ObterAdjacentes(string vertice)
    {
        return Adjacencias.ContainsKey(vertice) 
            ? Adjacencias[vertice] 
            : new List<string>();
    }

    public IEnumerable<string> ObterTodosVertices() => Adjacencias.Keys.OrderBy(v => v);
}
