# ?? BFS - Busca em Amplitude (Breadth-First Search)

## ?? Estrutura do Projeto

```
amplitude/
??? Data/                           # Dados de entrada
?   ??? grafo.txt                   # Arquivo com definição do grafo
?
??? Models/                         # Modelos de domínio
?   ??? Grafo.cs                    # Classe que representa o grafo
?   ??? ResultadoBFS.cs             # Classe com resultado da busca BFS
?
??? Services/                       # Lógica de negócio
?   ??? Interfaces/
?   ?   ??? IGrafoService.cs        # Interface para operações de grafo
?   ?   ??? IBFSService.cs          # Interface para algoritmo BFS
?   ??? GrafoService.cs             # Implementação de operações de grafo
?   ??? BFSService.cs               # Implementação do algoritmo BFS
?
??? UI/                             # Interface com usuário
?   ??? Interfaces/
?   ?   ??? IGrafoUI.cs             # Interface para UI do grafo
?   ?   ??? IBFSUI.cs               # Interface para UI do BFS
?   ??? GrafoUI.cs                  # Implementação UI do grafo
?   ??? BFSUI.cs                    # Implementação UI do BFS
?
??? Program.cs                      # Ponto de entrada da aplicação
```

## ?? Responsabilidades

### ?? Models
- **Grafo**: Representa a estrutura do grafo com suas adjacências
- **ResultadoBFS**: Armazena o resultado da busca (ordem, níveis, passos)
- **PassoBFS**: Representa cada passo da execução do algoritmo

### ?? Services
- **IGrafoService/GrafoService**: 
  - Carrega grafo de arquivo
  - Valida estrutura do grafo
  
- **IBFSService/BFSService**: 
  - Executa o algoritmo de busca em amplitude
  - Gera resultado detalhado com passos

### ?? UI
- **IGrafoUI/GrafoUI**: 
  - Exibe título e interface principal
  - Mostra estrutura do grafo
  - Gerencia interação com usuário
  
- **IBFSUI/BFSUI**: 
  - Exibe resultado da busca BFS
  - Mostra passos da execução
  - Apresenta ordem de visitação e níveis

## ?? Como Usar

1. Edite o arquivo `Data/grafo.txt` com seu grafo:
```
# Formato: vertice:adjacente1,adjacente2,adjacente3
A:B,C,D
B:A,E,F
C:A,G
```

2. Execute o programa:
```bash
dotnet run
```

3. Digite o vértice inicial quando solicitado

## ??? Tecnologias

- **.NET 8**
- **C# 12.0**
- **Spectre.Console** - Para interface rica no terminal

## ?? Princípios Aplicados

- ? **SOLID**: Separação de responsabilidades
- ? **Dependency Injection**: Uso de interfaces
- ? **Clean Architecture**: Camadas bem definidas
- ? **Single Responsibility**: Cada classe tem uma responsabilidade única
