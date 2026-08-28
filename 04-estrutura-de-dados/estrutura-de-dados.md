# Etapa 4 - Estruturas de Dados

## 1. Arrays

Coleção de tamanho **fixo**, definido na criação, de um único tipo:

```Csharp
    int[] numeros = new int[5]; // 5 posições, todas com valor default (0 para int)
    numero[0] = 10;
    numero[1] = 20;
    int[] outrosNumeros = {1, 2, 3, 4, 5}; // Inicialização direta
```

Acesso por índice, começando em `0`. Acessar um índice que não existe (`numero[5]` num array de 5 posições, índice válido 0-4) lança `IndexOutOfRangeException`, **em runtime**, não erro de compilação; O compilador não sabe prever isso.

Tamanho fixo é a limitação central: se você precisar de mais espaço depois de criado, não dá para "aumentar" um array, precisa criar um novo. É exatamente essa limitação que motiva a próxima estrutura.

---

## 2. List<T>

Coleção de tamanho **dinâmico**, cresce e encolhe conforme você adicona/remove itens. `T` é um placeholder de tipo (Você vai estudar Generics a fundo mais para frente, por enquanto, trate `List<int>` como "lista que só aceita int", `List<string>` como "Lista que só aceita string").

```Csharp
   List<string> nomes = new List<string>();
   nomes.Add("Arthur"); 
   nomes.Add("Maria");

   nomes.Remove("Maria"); // Remove pelo valor
   nomes.RemoveAt(0); // Remove pelo índice
   bool existe = nomes.Contains("Arthur");
   int total = nomes.Count;
```

Diferença importante que confunde quem vem de array: `array.length` vs `list.Count`. São conceitos equivalentes, nomes diferentes, e usar o errado é erro de compilação, não algo sutil. Igual array, acessar índice inexistente (`nomes[10]` numa lista de 2 itens) lança `ArgumentOutOfRangeException` em runtime.

### Percorrendo uma List

```Csharp
   foreach (string nome in nomes)
   {
        Console.WriteLine(nome);
   } 
```

`foreach` é preferível a `for` quando você só precisa ler cada item, sem precisar de índice numérico. Usa `for` quando precisa do índice (ex: modificar item específico, comparar posições)

---

## 3. Dictionary<Tkey, Tvalue>

Coleção de pares `chave-valor`. Diferente de List (acesso por posição numérica), Dictionary acessa por chave, de qualquer tipo:

```Csharp
   Dictionary<string, int> idades = new Dictionary<string, int>();
   idades.Add("Arthur", 17);
   idades["Maria"] = 30; // Forma alternativa de adicionar/atualizar

   int idadeArthur = idades["Arthur"]; // Acesso direto pela chave
```

**Risco real**: Acessar uma chave que não existe (`idades["pedros"]` sem ter sido adicionado) lança `KeyNotFoundException` em runtime. isso é o erro mais comum de quem começa a usar Dictionary, acessar direto sem checar se a chave existe primeiro.

Forma segura de checar antes de acessar:

```Csharp
   if (idades.ContainsKey("Pedro"))
   {
        Console.WriteLine(idade["Pedro"]);
   } 
   else
   {
        Console.WriteLine("Chave não encontrada.");
   }
```

Ou, mais eficiente (evitar checar duas vezes, uma no `ContainsKey`, outra implícita no acesso):

```Csharp
   if (idades.TryGetValue("Pedro", out int idade))
   {
        Console.WriteLine(idade);
   } 
   else
   {
        Console.WriteLine("Chave não encontrada.");
   }
```

Repare no padrão: `TryGetValue` segue a mesma filosofia do `TryParse` que você já usa, método que tenta uma operação arriscada e retorna `bool` indicando sucesso, em vez de lançar exceção direto. Esse padrão (`TryX` retornando bool + `out`) é recorrente em C#, não é coincidência de nome.

### Percorrendo um Dictionary

```Csharp
   foreach (KeyValuePair<string, int> par in idades)
   {
        Console.WriteLine($"{par.key}: {par.value}");
   } 

   // ou, de forma mais moderna e comum:
   foreach (var (nome, idade) in idades)
   {
        Console.WriteLine($"{nome}: {idade}");
   }
```

---

## 4. Queue<T> (fila)

Estrutura **FIFO** (First In, First Out), o primeiro item que entra é o primeiro que sai. Pense em fila de banco.

```Csharp
   Queue<string> filaAtendimento = new Queue<string>();
   filaAtendimento.Enqueue("Cliente 1");
   filaAtendimento.Enqueue("Cliente 2");

   string proximo = filaAtendimento.Dequeue(); // Remove e retorna "Cliente 1"
   string espiando = filaAtendimento.Peek(); // Olha o próximo sem remover
```

Chamar `Dequeue()` ou `Peek()` numa fila **vazia** lança `InvalidOperationException` em runtime. Sempre confira `filaAtendimento.Count > 0` antes, se não tiver certeza que a fila tem item.

---

## 5. Stack<T> (Pilha)

Estrutura **LIFO** (Last In, First Out), o último item que entra é o primeiro que sai, Pense em pilha de pratos, ou o botão "Voltar" do navegador (histórico).

```Csharp
   Stack<string> historico = new Stack<string>();
   historico.Push("Página 1");
   historico.Push("Página 2");

   string ultima = historico.Pop(); // remove e retorna "página 2"
   string espiando = historico.Peek(); // Olha o topo sem remover 
```

Mesmo risco que Queue: `Pop()` ou `Peek()` numa pilha vazia lança `InvalidOperationException`.

## Quando usar qual

- **List**: Quando você precisa de acesso aleatório por índice, ou não importa ordem de entrada/saída
- **Queue**: Quando a ordem de processamento precisa respeitar "quem chegou primeiro" (ex: fila de tarefas, fila de impressão).
- **Stack**: Quando você precisa desfazer a última ação, ou processar do mais recente pro mais antigo (ex: histórico de navegação, undo de editor de texto).

---

## Checklist antes de ir pros exercícios

- [ ] Eu sei a diferença entre `array,Length` e `list.Count`, e sei que confudir isso é erro de compilação?
- [ ] Eu sei por que acessar `dicionario["chave_inexistente"] direto é arriscado, e qual método usar para evitar isso sem exceção?
- [ ] Eu sei explicar a diferença entre FIFO (Queue) e LIFO (Stack) com um exemplo do mundo real para cada?
- [ ] Eu sei que `Dequeue`, `Pop`, `Peek` numa estrutura vazia lançam exceção em runtime, não erro de compilação?

---

## Exercícios

1. **Lista de compras**: Adicione itens a uma `List<string>`, remova um item específico, imprima a lista final e a quantidade de itens
2. **Agenda de contatos**: use `Dictionary<string, string>` (nome -> telefone), adicione 3 contatos, busque um contato por nome tratando o caso de não encontrar (sem lançar exceção pro usuário)
3. **Fila de atendimento**: Simule uma fila de atendimento com `Queue<string>`: Adicione 4 pessoas, atenda (remova) 2 em ordem, imprima quem ainda está na fila
4. **Pilha de histórico de navegação**: simule histórico de páginas visitadas com `Stack<string>`: visite 4 páginas, "volte" (pop) 2 vezes, imprima a página atual após os 2 voltares

### [DEBUG]

Código abaixo com bugs propositais envolvendo Dictionary e List. Ache e corrija.

```Csharp
   Dictionary<string, int> estoque = new Dictionary<string, int>();
estoque.Add("Maçã", 50);
estoque.Add("Banana", 30);

Console.Write("Digite o produto para consultar: ");
string produto = Console.ReadLine();

int quantidade = estoque[produto];
Console.WriteLine($"Estoque de {produto}: {quantidade}");

List<string> produtosEsgotados = new List<string>();

for (int i = 0; i <= estoque.Count; i++)
{
    if (estoque.ElementAt(i).Value == 0)
    {
        produtosEsgotados.Add(estoque.ElementAt(i).Key);
    }
}

Console.WriteLine($"Produtos esgotados: {produtosEsgotados.Count}"); 
```

### Checkpoint 1

Volte no exercício "**Menu de opções no controle**" da Etapa 3 e reescreva usando estruturas dessa etapa, por exemplo, trocar variáveis soltar por uma `List<string>` de itens de "estoque" ou "saldo", ou usar `Dictionary<int, string>` para mapear opções -> ação.