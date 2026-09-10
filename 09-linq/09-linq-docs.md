# Etapa 9 - Coleções Avançadas, Delegates e LINQ

## 1. HashSet<T> - a coleção avançada que faltava

Você já conhece `List`, `Dictionary`, `Queue`, `Stack` desde a Etapa 4. `HashSet<T>` é outra coleção da mesma família, com uma característica central: **não permite elementos duplicados, e não mantém ordem garantida**.

```CSharp
    HashSet<string> nomes = new HashSet<string>();
    bool adicionou1 = nomes.Add("Arthur"); // true, adicionou
    bool adicionou2 = nomes.Add("Arthur"); // false, já existia, não duplicou

    Console.WriteLine(nomes.Count); //1, não 2
```

`Add` retorna `bool`, `true` se realmente adicionou, `false` se o item já existia (nesse caso, não muda no HashSet). Isso é diferente de `List.Add`, que sempre adiciona, gerando duplicada se você não checar antes.

### Quando usar HashSet em vez de List:

- Quando duplicata não faz sentido no seu domínio (ex: lista de emails já cadastrados, tags únicas).
- Quando você vai fazer muitas checagens de "esse item já existe?" (`Contains`), HashSet é **muito mais rápido** que List para essa operação escificamente, por como ele é implementado internamente (hashing, não busca sequencial).

```CSharp
    bool existe = nomes.Contains("Arthur"); // Rápido em HashSet, mais lento em List grande
```

### Operações de conjunto (o que HashSet faz quando List não faz nativamente)

```CSharp
    HashSet<int> a = new HashSet<int> { 1, 2, 3, 4 };
    HashSet<int> b = new HashSet<int> { 3, 4, 5, 6 };

    a.UnionWith(b); // a agora tem união: {1,2,3,4,5,6}
    a.IntersectWith(b); //a agora tem só o que está em AMBOS
    a.ExceptWith(b); //a agora tem só o que está em 'a' e NÃO em 'b'
```

Isso reflete operações matemáticas de conjuntos (união, intersecção, diferença), útil quando você precisa comparar dois grupos de dados.

---

## 2. Delegates - a base de tudo que vem depois

Um **delegate** é um tipo que representa **uma referência a um método**, em vez de uma variável guardar um número ou uma string, ela guarda "um método que pode ser chamado depois".

```CSharp
   delegate int Operacao(int a, int b);

   static int Somar(int a, int b) => a + b;
   static int Subtrair(int a, int b) => a - b;

   Operacao op = Somar;
   Console.WriteLine(op(5, 3)) // 8, "op" está apontando pro método Somar 

   op = Subtrair
   Console.WriteLine(op(5, 3)) // 2, agora aponta pro método Subtrair
```

Isso é poderoso, porque permite **passar comportamento como parâmetro**, não só dados:

```CSharp
    static void Executar(Operacao operacao, int x, int y)
    {
        Console.WriteLine(operacao(x, y));
    }

    Executar(somar, 10, 5); // 15
    Executar(Subtrair, 10, 5); // 5
```

`Executar` não sabe, de antemão, qual operação vai rodar, quem chama decide, passando o método desejado. Isso é a base conceitual de todo o LINQ que você vai estudar mais abaixo: `Where(condicao)`, por exemplo, é `Where` recebendo um método/lambda como parâmetro, exatamento como `Executar` recebe `operacao`.

---

## 3. Func, Action, Predicate - Delegates genéricos prontos

Declarar um `delegate` customizado toda vez (como `operacao` acima) é raro na prática moderna, o .NET já fornece delegates genéricos prontos para cobrir os casos comuns:

### Func<T, TResult> - Método que recebe e retorna algo

```CSharp
   Func<int, int, int> Somar = (a, b) => a + b;
   Console.WriteLine(somar(5, 3)); // 8 
```

Os parâmetros de tipo são: todos os tipos de entrada, e o último é sempre o tipo de retorno. `Func<int, int, int>`= recebe dois `int`, retorna um `int`.

### Action<T> - Método que recebe algo mas não retorna nada (void)

```CSharp
   Action<string> imprimir = (mensagem) => Console.WriteLine(mensagem);
   imprimir("Olá!"); // "Olá!"
```

`Action` nunca tem tipo de retorno declarado, porque é sempre `void` impricitamente.

### Predicate<T> - Método que recebe algo e retorna bool

```CSharp
   Predicate<int> ehPar = (n) => n % 2 == 0;
   Console.WriteLine(ehPar(4)); // True 
```

isso é equivalente, na prática, a `Func<int, bool>`, `Predicate<T>` é só um nome mais expressivo par caso específico (condição booleana), usado historicamente em métodos como `List<T>.Find(Predicate<T>)`.

Conexão direta com o que vem a seguir: quando você escreve `numeros.Where(n => n % 2 == 0)`, o `Where` está internamente esperando receber algo do tipo `Func<int, bool>`, a lambda que você escreve é esse delegate sendo construído inline, sem você precisar nomear o tipo explicitamente. Isso é o que amarra Delegates + Lambda + LINQ como um assunto só.

---

## 4. Events - Notificação entre objetos

Um event é um mecanismo baseado em delegate que permite um objeto avisar outros objetos quando algo acontece, sem precisar saber quem está "escutando".

```CSharp
   class Alarme
   {
        public event Action? Disparado; // event baseado no delegate Action

        public void Ativar()
        {
            Console.WriteLine("Alarme ativado!");
            Disparado?.Invoke(); // Notifica quem estiver "escutando", se houver algúem
        }
   }

   class Program
   {
        static void Main()
        {
            Alarme alarme = new Alarme();

            alarme.Disparado += () => Console.WriteLine("Vizinho ouviu o alarme.");
            alarme.Disparado += () => Console.WriteLine("Polícia foi notificada.");

            alarme.Ativar();
            // Imprime: "Alarme ativado!", depois as duas reações, na ordem que foram registradas
        }
   }
```

- `+=`: Inscreve um método (ou lambda) para ser chamado quando o evento disparar. Você pode inscrever vários.
- `Disparado?.Invoke()`: dispara o evento, chamando todos os métodos inscritos, na ordem que foram adicionados. O `?.` existe porque, se nenhum método foi escrito, `Disparado` é `null`, e chamar `Invoke()` num delegate `null` lançaria exceção, o `?.` evita isso.

### Diferença entre event e delegate comum:

Um delegate público comum poderia ser substituído de fora da classe (`meuDelegate = outroMetodo;`, perdendo tudo que estava inscrito antes.) `event` restringe isso, de fora da classe, você só pode `+=` ou `-=` (inscrever/desinscrever), nunca substituir tudo de uma vez. Isso protege a classe de perder inscrições por acidente ou má-fé de código externo.

---

## 5. O que é LINQ

Um conjunto de método de extensão que permitem consultar, filtrar, ordenar e transformar coleções (`List`, array, `Dictionary`, etc) de forma declarativa, você escreve o que quer, não como conseguir, ao contrário de escrever um `foreach` manual passo a passo.

```CSharp
   List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

   // Sem LINQ (Etapa 3/4, jeito manual)
   List<int> pares = new List<int>();
   foreach(int n in numeros)
   {
        if (n % 2 == 0) pares.Add(n);
   } 

   // Com LINQ
   List<int> pareslinq = numeros.Where(n => n % 2 == 0).ToList();
```

Os dois produzem o mesmo resultado, LINQ não é "mágica nova", é uma forma mais compacta e legível de expressar operações que você já sabe fazer manualmente.

---

## 6. Expressões lambda - a peça central

```CSharp
   n => n % 2 == 0 
```

Isso é uma expressão lambda, uma função anônima, sem nome, escrita inline. `n` é o parâmetro (o tipo é inferido do contexto, LINQ sabe que `n` é `int` por que é de `int`), e depois do `=>` vem o corpo, que retona um valor (aqui, um `bool`).

Você pode per `n => n% 2 == 0` como "dado um `n`, retorna-se `n % 2 == 0`". É equivalente, conceitualmente, a um método:

```CSharp
   bool EhPar(int n) => n % 2 == 0; // você já usou essa sintaxe (expression body) na Etapa 5 
```

LINQ usa lambdas exatamente para receber "a condição" ou "a transformação" que você quer aplicar, sem precisar declarar um método nomeado só para isso.

---

## 7. Métodos de filtro

### Where = filtra elementos que satisfazem uma condição

```CSharp
   var adultos = pessoas.Where(p => p.idade >= 18); 
```

Retorna todos os elementos onde a condição é verdadeira, pode ser zero, um ou vários.

### First/FirstOrDefault

```CSharp
   var primeiro = pessoas.First(p => p.Idade >= 18); // Lança InvalidOperationException se não achar nenhum
   var primeiroOuNulo = pessoas.FirstOrDefault(p => p.Idade >= 18); // retorna null (ou default do tipo) se não achar
```

Essa é a distinção que mais gera bug em código real. `First` exige que exista pelo menos um resultado, se a lista estiver vazia ou nenhum item satisfazer a condição, ele lança exceção. `FirstOrDefault` é mais tolerante, devolve `null` (para tipos referência) ou o valor default (`0` para `int`, por exemplo) se não achar nada, sem lançar exceção.

O perigo: se você usa `FirstOrDefault` e esquece de checar se o resultado é `null` antes de usar, vai cai exatamente no erro que o documento da Etapa 4 avisou sobre Dictionary, só que agora escondido dentro de uma linha de LINQ compacta:

```CSharp
   var pessoa = pessoas.FirstOrDefault(p => p.Nome == "Zé");
   Console.WriteLine(pessoa.Idade); // NullReferenceException se "Zé" não existir na lista.
```

### Single/SingleOrDefault

```CSharp
   var unico = pessoas.Single(p => p.Nome == "Arthur"); 
```

Parecido com `First`, mas exige que exista exatamente um resultado, lança exceção tanto se não achar nenhum quanto  se achar mais de um. Usado quando você espera unicidade (ex: buscar por um ID que deveria ser único).

### Any/All

```CSharp
   bool existeMenor = pessoas.Any(p => p.Idade < 18); // true se PELO MENOS UM satisfizer
   bool todosAdultos = pessoas.All(p => p.Idade >= 18); // true se TODOS satisfizerem
```

Você já usou `.All()` na Etapa 5, agora formalizado. `Any()` sem condição (`pessoas.Any()`) também serve para checar rapidamente se uma coleção tem qualquer elemento, sem precisar comparar `Count > 0`.

---

## 8. Métodos de projeção e transformação
 
### Select — transforma cada elemento

```csharp
List<string> nomes = pessoas.Select(p => p.Nome).ToList();
```

Pra cada `Pessoa` na lista original, extrai só o `Nome`, produzindo uma nova lista de strings. Diferente de `Where` (que filtra, mantendo o tipo original), `Select` **transforma**, podendo até mudar completamente o tipo do resultado.
 
### OrderBy / OrderByDescending

```csharp
var porIdade = pessoas.OrderBy(p => p.Idade).ToList();
var porIdadeDesc = pessoas.OrderByDescending(p => p.Idade).ToList();
```
 
### GroupBy — agrupa elementos por uma chave

```csharp
var porFaixaEtaria = pessoas.GroupBy(p => p.Idade >= 18 ? "Adulto" : "Menor");
 
foreach (var grupo in porFaixaEtaria)
{
    Console.WriteLine($"{grupo.Key}: {grupo.Count()} pessoas");
    foreach (var pessoa in grupo)
    {
        Console.WriteLine($"  - {pessoa.Nome}");
    }
}
```

`grupo.Key` é o valor usado pra agrupar ("Adulto"/"Menor" nesse caso). Cada `grupo` também é, ele mesmo, uma coleção, dá pra usar `Count()`, `foreach`, ou até outro LINQ dentro dele.
 
---

## 9. Métodos de agregação
 
```csharp
int total = numeros.Sum();
double media = numeros.Average();
int maximo = numeros.Max();
int minimo = numeros.Min();
int quantidade = numeros.Count();          // igual a .Count, mas como método -- útil depois de Where
int quantidadeFiltrada = numeros.Count(n => n % 2 == 0); // conta só os que satisfazem a condição
```

Repara que `Count()` como método (com parênteses) é diferente de `Count` como propriedade (sem parênteses, que você já usa desde a Etapa 4) — o método aceita uma condição opcional, a propriedade não aceita nada.
 
---

## 6. Encadeamento (method chaining)
 
O poder real de LINQ aparece quando você **encadeia** vários métodos:
 
```csharp
var resultado = pessoas
    .Where(p => p.Idade >= 18)
    .OrderBy(p => p.Nome)
    .Select(p => p.Nome)
    .ToList();
```
Isso filtra adultos, ordena por nome, extrai só o nome, e materializa em uma `List<string>`. Cada método LINQ (exceto os de agregação/materialização como `ToList()`, `Count()`, `Sum()`) retorna outra coleção "consultável", permitindo encadear o próximo método em sequência, é assim que se lê código LINQ real: de cima pra baixo, cada linha refinando o resultado da anterior.
 
### ToList() — quando materializar
LINQ é **lazy** (avaliação adiada) — `Where`, `Select`, `OrderBy` não executam a busca imediatamente, eles só **descrevem** a operação. A execução real só acontece quando você "materializa" o resultado, com `ToList()`, `ToArray()`, ou quando você itera com `foreach`. Isso é um detalhe avançado que não vai te travar agora, mas explica por que às vezes você vê `.ToList()` no final de uma cadeia e às vezes não — depende se você precisa do resultado como uma lista concreta pra usar depois, ou só vai iterar uma vez.
 
---

## Checklist antes de ir pros exercícios
 
- [ ] Eu sei explicar, com minhas palavras, o que um delegate representa (uma referência a método, não a um valor comum)?
- [ ] Eu sei a diferença entre `Func`, `Action` e `Predicate`, qual tem retorno, qual não tem, qual é sempre bool?
- [ ] Eu sei por que `event` restringe de fora da classe pra só `+=`/`-=`, em vez de permitir substituição total?
- [ ] Eu sei quando `HashSet` é preferível a `List` (dica: duplicata e busca frequente)?
- [ ] Eu sei explicar, com um exemplo, quando `FirstOrDefault` retorna `null` e por que isso é perigoso se eu não checar antes de usar o resultado?
- [ ] Eu sei a diferença entre `Where` (filtra) e `Select` (transforma)?
- [ ] Eu sei ler uma lambda simples (`p => p.Idade >= 18`) e explicar o que ela faz, sem travar na sintaxe?
- [ ] Eu sei por que `Single` lança exceção em dois cenários diferentes (nenhum resultado E múltiplos resultados), não só um?

---

## Exercícios
 
### Bloco 1 — Coleções avançadas, Delegates, Events
 
1. **HashSet de e-mails únicos**: simule um cadastro que rejeita e-mails duplicados usando `HashSet<string>`, mostrando mensagem diferente se o e-mail já existia ou se foi adicionado com sucesso (use o retorno `bool` de `Add`)
2. **Calculadora com delegate**: crie um `delegate` customizado (`Operacao`, como no exemplo do documento) e um método `Executar` que recebe a operação e dois números, testando com pelo menos 3 operações diferentes (soma, subtração, multiplicação)
3. **Sistema de notificação com Events**: crie uma classe `Pedido` com um `event Action<string>? StatusAlterado`. Um método `AtualizarStatus(string novoStatus)` que dispara o evento. No `Main`, inscreva pelo menos 2 "ouvintes" diferentes (ex: um que imprime no console, outro que simula "enviar email") e dispare a atualização
### Bloco 2 — LINQ
 
4. **Filtro de produtos**: dada uma `List<Produto>` (reaproveita a classe da Etapa 6), use `Where` para filtrar produtos com `Preco` acima de um valor, e `Select` para extrair só os nomes desses produtos
5. **Relatório de vendas agrupado**: dada uma lista de vendas (crie uma classe simples `Venda` com `Vendedor` e `Valor`), use `GroupBy` para agrupar por vendedor e mostrar o total vendido por cada um (`Sum()`)
6. **Busca seguro com FirstOrDefault**: busque uma pessoa por nome numa `List<Pessoa>`, tratando corretamente o caso de não encontrar (sem lançar `NullReferenceException`), decida você se prefere checar `null` antes de usar, ou usar `Any()` primeiro
7. **Estatísticas com agregação**: dada uma lista de números, calcule e imprima: soma, média, máximo, mínimo, e quantidade de números pares, tudo usando métodos LINQ (sem loop manual).

---

### [DEBUG]
Código abaixo com dois bugs: um `FirstOrDefault` não tratado corretamente, e uma lambda capturando variável de loop de forma problemática. Ache e corrija ambos.
 
```csharp
List<Produto> produtos = new List<Produto>
{
    new Produto("Caneta", 5, 2.50m),
    new Produto("Caderno", 10, 15.00m)
};
 
var produtoBuscado = produtos.FirstOrDefault(p => p.Nome == "Lápis");
Console.WriteLine($"Preço encontrado: {produtoBuscado.Preco}");
 
List<Func<int>> funcoes = new List<Func<int>>();
 
for (int i = 0; i < 3; i++)
{
    funcoes.Add(() => i);
}
 
foreach (var funcao in funcoes)
{
    Console.WriteLine(funcao()); // o que você espera imprimir aqui, e o que realmente imprime?
}
```
