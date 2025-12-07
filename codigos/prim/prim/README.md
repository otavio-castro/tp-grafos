# Algoritmo de Prim - Árvore Geradora Mínima

## ?? Descrição do Código

Este programa em C# lê uma matriz de adjacência a partir de um arquivo de texto e calcula a **Árvore Geradora Mínima (MST)** utilizando o **algoritmo de Prim**.

Para melhorar a visualização no terminal, o código utiliza a biblioteca **Spectre.Console**, exibindo o resultado em formato de árvore estilizada.

## ?? Funcionamento Geral

### 1. Leitura do Grafo
- O programa lê o arquivo `TextFile1.txt`, que contém uma matriz de adjacência representando um grafo ponderado e não direcionado.
- A matriz é convertida para um array bidimensional (`int[,]`).

### 2. Execução do Algoritmo de Prim
- Começa pelo **vértice 0**
- A cada passo, escolhe a **aresta de menor peso** que conecta um vértice visitado a um não visitado
- Constrói uma lista com as arestas da MST

### 3. Exibição Formatada no Console
Com **Spectre.Console**:
- Título em destaque
- Árvore visual com vértices e pesos coloridos

## ?? Principais Funções

### `LerGrafoDoArquivo()`
- Lê todas as linhas do arquivo
- Converte cada linha em inteiros
- Preenche a matriz de adjacência

### `Prim()`
- Implementa o algoritmo de Prim manualmente
- Mantém controle de vértices visitados
- Busca sempre a menor aresta válida
- Retorna a MST no formato de lista de tuplas `(origem, destino, peso)`

### `Main()`
- Define o caminho do arquivo
- Carrega o grafo
- Executa Prim
- Exibe o resultado com formatação colorida

## ?? Formato esperado do arquivo TextFile1.txt

Cada linha deve conter valores separados por espaço, representando a matriz de adjacência do grafo:

```
0 2 0 6 0
2 0 3 8 5
0 3 0 0 7
6 8 0 0 9
0 5 7 9 0
```

Onde:
- `0` indica que **não há aresta** entre os vértices
- Valores positivos representam o **peso da aresta** entre dois vértices

## ?? Como Executar

1. Certifique-se de ter o **.NET 8** instalado
2. Navegue até o diretório do projeto:
   ```bash
   cd prim
   ```
3. Execute o projeto:
   ```bash
   dotnet run
   ```

## ?? Dependências

- **.NET 8.0**
- **Spectre.Console** (v0.54.0)

## ?? Exemplo de Saída

```
Árvore Geradora Mínima (Prim)
Grafo
??? 0 — 1  (peso: 2)
??? 1 — 2  (peso: 3)
??? 1 — 4  (peso: 5)
??? 0 — 3  (peso: 6)
```

## ?? Observações

- O algoritmo trata grafos **desconexos** interrompendo a execução quando não há mais arestas disponíveis
- A complexidade do algoritmo é **O(V²)**, onde V é o número de vértices
- O arquivo de entrada deve estar localizado no mesmo diretório do executável

## ?? Tecnologias Utilizadas

- **C# 12.0**
- **.NET 8**
- **Spectre.Console** para visualização aprimorada
