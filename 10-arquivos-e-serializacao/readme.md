# Etapa 10 - Arquivos e Serialização

## 1. Escrita e leitura básica de arquivo texto
 
```csharp
using System.IO;
 
// Escrever
File.WriteAllText("dados.txt", "Olá, arquivo!");
 
// Ler
string conteudo = File.ReadAllText("dados.txt");
Console.WriteLine(conteudo);
```

`File.WriteAllText` **sobrescreve** o arquivo inteiro se ele já existir, não adiciona ao final, substitui todo o conteúdo. Se você quiser adicionar sem apagar o que já existia, usa `File.AppendAllText("dados.txt", "mais uma linha\n")`.
 
### Trabalhando com linhas

```csharp
string[] linhas = { "linha 1", "linha 2", "linha 3" };
File.WriteAllLines("dados.txt", linhas);
 
string[] linhasLidas = File.ReadAllLines("dados.txt");
foreach (string linha in linhasLidas)
{
    Console.WriteLine(linha);
}
```

`ReadAllLines` retorna um array de strings, uma por linha, mais conveniente que `ReadAllText` quando o arquivo tem estrutura por linha (ex: um item de lista por linha).

---

## 2. StreamWriter e StreamReader — controle mais fino
 
`File.WriteAllText`/`ReadAllText` são convenientes, mas abrem e fecham o arquivo automaticamente numa chamada só, bom pra casos simples. Quando você precisa **escrever várias vezes ao longo do código**, sem reabrir o arquivo do zero a cada linha, usa `StreamWriter`:
 
```csharp
using (StreamWriter writer = new StreamWriter("dados.txt"))
{
    writer.WriteLine("Primeira linha");
    writer.WriteLine("Segunda linha");
} // o arquivo é fechado automaticamente aqui, ao saír do bloco using
```
 
**O bloco `using` é essencial aqui, não decoração.** `StreamWriter` mantém o arquivo **aberto** enquanto está em uso, se você não fechar explicitamente (`writer.Close()`) ou usar `using` (que fecha automaticamente ao sair do bloco, mesmo se ocorrer exceção no meio), o arquivo continua "travado" pelo seu programa. Isso é exatamente o tipo de bug que o exercício de debug dessa etapa vai expor: esquecer de fechar o `StreamWriter` deixa o arquivo bloqueado, impedindo até seu próprio programa de reabri-lo depois, ou perdendo dados que ficaram só no buffer de memória, nunca gravados no disco de fato.
 
```csharp
using (StreamReader reader = new StreamReader("dados.txt"))
{
    string? linha;
    while ((linha = reader.ReadLine()) != null)
    {
        Console.WriteLine(linha);
    }
}
```

`ReadLine()` retorna `null` quando chega ao fim do arquivo, por isso o `while` com essa condição funciona como "leia até acabar".
 
### Conectando com a Etapa 8 (exceções)

Operação de arquivo é o exemplo clássico de "erro que você não pode prever nem evitar completamente", o arquivo pode não existir, o caminho pode estar errado, a permissão pode faltar:
 
```csharp
try
{
    string conteudo = File.ReadAllText("dados.txt");
}
catch (FileNotFoundException ex)
{
    Console.WriteLine("Arquivo não encontrado: " + ex.Message);
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine("Sem permissão para acessar o arquivo: " + ex.Message);
}
```

Isso é onde `finally` (Etapa 8) also faz mais sentido na prática, garantir que um recurso seja fechado independente do resultado, embora `using` já cubra isso automaticamente pra `StreamWriter`/`StreamReader`, sendo geralmente preferível a escrever `finally` manual pra esse caso específico.
 
### Checando existência antes de agir

```csharp
if (File.Exists("dados.txt"))
{
    // seguro ler
}
else
{
    Console.WriteLine("Arquivo não existe.");
}
```

Isso não elimina 100% o risco (o arquivo pode ser deletado por outro processo entre o `Exists` e o `ReadAllText`, uma race condition rara mas possível), mas reduz bastante o caso comum de esquecimento.

---

## 3. Serialização com JSON
 
**Serializar** significa transformar um objeto C# em uma representação que pode ser salva ou transmitida (aqui, texto JSON). **Desserializar** é o processo inverso: pegar o texto JSON e reconstruir o objeto C#.
 
```csharp
using System.Text.Json;
 
class Pessoa
{
    public string Nome { get; set; } = "";
    public int Idade { get; set; }
}
 
Pessoa pessoa = new Pessoa { Nome = "Arthur", Idade = 17 };
 
// Serializar (objeto -> JSON)
string json = JsonSerializer.Serialize(pessoa);
Console.WriteLine(json); // {"Nome":"Arthur","Idade":17}
 
File.WriteAllText("pessoa.json", json);
```
 
```csharp
// Desserializar (JSON -> objeto)
string jsonLido = File.ReadAllText("pessoa.json");
Pessoa? pessoaLida = JsonSerializer.Deserialize<Pessoa>(jsonLido);
 
if (pessoaLida != null)
{
    Console.WriteLine($"{pessoaLida.Nome}, {pessoaLida.Idade} anos");
}
```
 
**`Deserialize<T>` retorna `T?` (nullable) — pode retornar `null`** se o JSON estiver malformado, ou vazio, ou não corresponder à estrutura esperada de forma alguma. Isso conecta direto com o que você já aprendeu na Etapa 9 sobre `FirstOrDefault`, sempre checar `null` antes de usar o resultado, nunca assumir que desserializar sempre funciona.
 
### Listas de objetos

```csharp
List<Pessoa> pessoas = new List<Pessoa>
{
    new Pessoa { Nome = "Arthur", Idade = 17 },
    new Pessoa { Nome = "Maria", Idade = 30 }
};
 
string jsonLista = JsonSerializer.Serialize(pessoas);
File.WriteAllText("pessoas.json", jsonLista);
 
string jsonListaLido = File.ReadAllText("pessoas.json");
List<Pessoa>? pessoasLidas = JsonSerializer.Deserialize<List<Pessoa>>(jsonListaLido);
```

Serializar/desserializar uma `List<T>` funciona exatamente igual a um objeto único, só muda o tipo genérico que você passa pro `Deserialize<T>`.
 
### Formatação legível (opcional, mas comum)

```csharp
var opcoes = new JsonSerializerOptions { WriteIndented = true };
string jsonFormatado = JsonSerializer.Serialize(pessoa, opcoes);
```

`WriteIndented = true` produz JSON com quebras de linha e indentação, mais fácil de ler num arquivo aberto manualmente, sem isso, o JSON vem tudo numa linha só, compacto.

---

## 4. Exceções comuns nessa etapa
 
| Exceção | Quando ocorre |
|---|---|
| `FileNotFoundException` | Tentar ler um arquivo que não existe |
| `UnauthorizedAccessException` | Falta de permissão pra acessar o arquivo/pasta |
| `DirectoryNotFoundException` | O caminho da pasta não existe |
| `IOException` | Erro genérico de I/O — ex: arquivo sendo usado por outro processo |
| `JsonException` | JSON malformado durante `Deserialize` |

---

## Checklist antes de ir pros exercícios
 
- [X] Eu sei por que esquecer de fechar um `StreamWriter` pode causar perda de dados, mesmo que o programa não trave?
- [X] Eu sei a diferença entre `File.WriteAllText` (sobrescreve) e `File.AppendAllText` (adiciona ao final)?
- [X] Eu sei por que `Deserialize<T>` pode retornar `null`, e por que isso exige a mesma cautela que `FirstOrDefault`?
- [X] Eu sei nomear pelo menos 3 exceções específicas de operação de arquivo, além da genérica `IOException`?

---

## Exercícios
 
1. **Diário de bordo**: programa que permite adicionar uma linha de texto a um arquivo `diario.txt` (usando `AppendAllText`, sem sobrescrever o que já existe), e outra opção pra ler e mostrar todo o conteúdo do arquivo
2. **Exportador de lista para CSV manual**: dada uma `List<Produto>` (reaproveita da Etapa 6/9), escreva um arquivo `produtos.csv` onde cada linha é `Nome,Preco,Quantidade` (sem usar biblioteca de CSV, só concatenando string com vírgula e `WriteLine`)
3. **Salvar e carregar cadastro em JSON**: serializa uma `List<Pessoa>` pra um arquivo `pessoas.json`, depois desserializa de volta e imprime os dados lidos, confirmando que os valores batem com o que foi salvo
4. **Backup com tratamento de exceção**: método que tenta ler um arquivo específico; se o arquivo não existir, cria ele com um conteúdo padrão, tratando `FileNotFoundException` explicitamente (mesmo sabendo que dá pra evitar isso com `File.Exists`, o exercício é pra você praticar capturar essa exceção especificamente)


### [DEBUG]

Código abaixo com dois problemas: um `StreamWriter` que nunca é fechado corretamente (arquivo pode ficar travado ou perder dados), e uma desserialização que falha silenciosamente por propriedade faltando no JSON. Ache e corrija ambos.
 
```csharp
class Produto
{
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
}
 
// Bug 1
StreamWriter writer = new StreamWriter("produtos.txt");
writer.WriteLine("Caneta - R$2.50");
writer.WriteLine("Caderno - R$15.00");
// esqueceu de fechar o writer aqui
 
// Bug 2
string jsonIncompleto = "{\"Nome\":\"Mouse\"}"; // faltando "Preco" de propósito
Produto? produto = JsonSerializer.Deserialize<Produto>(jsonIncompleto);
Console.WriteLine($"Preço do produto: {produto.Preco}"); // o que acontece aqui?
```