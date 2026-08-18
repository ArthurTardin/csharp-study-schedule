# Etapa 4 - Estrutura de dados

## Objetivo

Aprender a armazenar e manipular coleções de valores: arrays, listas, e dicionários, além das operações mais comuns sobre eles.

## Arrays

Uma array é uma coleção de **tamanho fixo**, em que todos os elementos são do mesmo tipo. Uma vez criado, o tamanho não muda.

```csharp
    int[] numeros = {1, 2, 3, 4, 5};
    string[] nomes = new string[3]; //array de 3 posições, vazio (com valores padrão)
```
### Acessando elementos

Os elementos são acessados por índice, começando em 0.

```csharp
    int[] numeros = {10, 20, 30};
    Console.WriteLine(numeros[0]); // 10
    numeros[1] = 99; // altera o segundo elemento
```

### Propriedade `Length`

Retorna a quantidade de elementos do array.

```csharp
    Console.WriteLine(numeros.length); // 3
```

### Percorrendo uma array

```csharp
    for (int i = 0; i < numeros.length; i++)
    {
        Console.WriteLine(numeros[i]);
    }
    // ou, de forma mis simples:
    foreach(int numero in numeros)
    {
        Console.WriteLine(numero);
    }
```

### Arrays Multidimensionais

```csharp
    int[,] matriz = new int[2, 3]; // 2 linhas, 3 colunas.
    matriz[0, 0] = 1;
    matriz[1, 2] = 9;
```

## Lista (`List<t>`)

Diferente do array, a List<T> tem **tamanho dinâmico**, pode crescer ou diminuir durante a execução. É um tipo genérico (<T> representa o tipo dos elementos que ela vai guardar), presente no namespace `System.Collections.Generic`.

```csharp
    using System.Collections.Generic;

    List<string> nomes = new List<string>();
    nomes.add("Arthur");
    nomes.add("Maria");
```

### Métodos comuns de `List<T>`

- Add(item) - Adiciona um elemento no final
- Remove(item) - Remove a primeira ocorrência do elemento
- RemoveAt(indice) - Remove o elemento em uma posição específica
- Contains(intem) - Verifica se o elemento existe na lista (retorna bool)
- IndexOf(intem) - retorna o índice do elemento (ou -1 se não encontrado)
- Count - Propriedade com a quantidade atual de elementos
- Clear() - Remove todos os elementos
- Sort() - Ordena os elementos

```csharp
    List<int> numeros = new List<string> {5, 3, 8, 1};
    numeros.Add(10);
    numeros.Remove(3);
    numeros.Sort();

    Console.WriteLine(numeros.Count); // 4
```

## Array vs List: Quando usar cada um

- Use `array` quando o tamanho da coleção é conhecido e fixo (ex: dias da semana, meses do ano).
- Use `List<T>` quando a quantidade de elementos pode variar durante a execução (ex: itens de um carrinho de compras, cadastro de usuários).

## Dicionário (`Dictionary<TKey, TValue>)

Armazena pares de **chave e valor**, em que cada chave é única. Acesso rápido ao valor através da chave, sem precisar percorrer a coleção inteira.

```csharp
    using System.Collections.Generic;

    Dictionary<string, int> idades = new Dictionary<string, int>();
    idades.Add("Arthur", 25);
    idades["Maria"] = 30; // Outra forma de adicionar/atualizar

    Console.WriteLine(idades["Arthur"]); // 25
```

### Métodos e propriedades comuns

- Add(chave, valor) - Adiciona um par chave-valor (erro se a chave já existir)
- ContainsKey(chave) - Verifica se a chave existe
- Remove(chave) - Remove o par pela chave
- TryGetValue(chave, out valor) - tenta obter o valor sem lançar exceção se a chave não existir
- Keys - coleção com todas as chaves
- Values - coleção com todos os valores

```csharp
    if (idades.TryGetVue("Arthur", out int idade))
    {
        Console.WriteLine(idade);
    }

    foreach(KeyValuePair<string, int> par in idades)
    {
        Console.WriteLine($"{par.key}: {par.Value}")
    }
```

## Tipos de valor vs tipos de referência (introdução)

Arrays, listas e dicionários são **tipos de referência**, a variável guarda um endereço de memória apontando para os dados, não os dados em si. Isso significa que, ao passar uma coleção para um método, alterações feitas nela dentro do método **refletem fora dele**(diferente do comportamento padrão de `int`, `double`, etc, que são tipos de valor).

```csharp
   static void AdicionarItem(List<string> lista)
   {
    lista.Add("novo item");
   } 

   List<string> minhaLista = new List<string>();
   AdicionarItem(minhaLista);
   Console.WriteLine(minhaLista.Count); // 1, a lista original foi alterada
```
*(Esse tópico será aprofundado mais adiante, quando falarmos sobre stack, heap e referência em detalhe)*

---

## Exercícios

- [] Lista de compras
- [] Agenda de contatos
- [] Cadastro de alunos com nota
- [] Fila de atendimento
- [] pilha de estoque