📌 Descrição do Código

Este programa em C# lê uma matriz de adjacência a partir de um arquivo de texto e calcula a Árvore Geradora Mínima (MST) utilizando o algoritmo de Prim.
Para melhorar a visualização no terminal, o código utiliza a biblioteca Spectre.Console, exibindo o resultado em formato de árvore estilizada.

📝 Funcionamento Geral

Leitura do Grafo
O programa lê o arquivo TextFile1.txt, que contém uma matriz de adjacência representando um grafo ponderado e não direcionado.
A matriz é convertida para um array bidimensional (int[,]).

Execução do Algoritmo de Prim

Começa pelo vértice 0

A cada passo, escolhe a aresta de menor peso que conecta um vértice visitado a um não visitado

Constrói uma lista com as arestas da MST

Exibição Formatada no Console
Com Spectre.Console:

título em destaque

árvore visual com vértices e pesos coloridos

⚙️ Principais Funções
LerGrafoDoArquivo()

Lê todas as linhas do arquivo

Converte cada linha em inteiros

Preenche a matriz de adjacência

Prim()

Implementa o algoritmo de Prim manualmente

Mantém controle de vértices visitados

Busca sempre a menor aresta válida

Retorna a MST no formato de lista de tuplas (origem, destino, peso)

Main()

Define o caminho do arquivo

Carrega o grafo

Executa Prim

Exibe o resultado com formatação colorida

📂 Formato esperado do arquivo TextFile1.txt

Cada linha deve conter valores separados por espaço, por exemplo:

0 2 0 6 0
2 0 3 8 5
0 3 0 0 7
6 8 0 0 9
0 5 7 9 0
