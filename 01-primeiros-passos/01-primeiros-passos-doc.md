# Etapa 1 - Primeiros passos

## Antes de começar

Não decore definição. Depois de cada bloco, pare e responda pra você mesmo: "eu consigo explicar isso sem reler?". Se não, releia só aquele bloco, não o texto todo.

---

# 1. Ambiente

##  O que é C#?

C# é uma linguagem de programação orientada a objetos, fortemente tipada, criada pela Microsoft em 2000. Roda sobre a plataforma .NET. Não é interpretada linha a linha como Python, ele é compilada.

## 2 O que é .NET?

.NET é a **plataforma** (não a linguagem). É o conjunto de bibliotecas, runtime e ferramentes que executam código C# (e também F#, VB.NET). Pense assim: C# é o idioma que você fala, .NET é o corpo que processa e executa esse idioma.

Existem hoje duas linhas principais:

- .NET (antigo .NET Core):
Multiplataforma (windows, Linux, macOS)
- .NET Framework:
Só windows, legado, usado em sistema antigos.

## .NET SDK

SDK = Software Developtment Kit. É o pacote que contém tudo que você precisa pra **desenvolver**: compilador, ferramentas de linha de comando (`dotnet`), bibliotecas de desenvolvimento.

Sem o SDK instalado, o comando `dotnet` não existe no seu terminal.

## Runtime

Runtime é o que **executa** o programa já compilado. Deferença chave:

- **SDK** = Para desenvolver (compilar, rodar, testar)
- **Runtime** = só para executar um programa já pronto

## CLR (Common Language Runtime)

É a máquina virtual do .NET, o motor que efetivamente executa seu código. Funciona assim, em ordem:

- VOcê escreve código em C# (`.cs`)
- O compilador (`csc`, acionado pelo `dotnet build`) transforma isso em **IL** (Intermediate Language), um código intermediário, não é binário de máquina ainda
- Quando você roda o programa, o CLR usa o **JIT** (Just-In-Time compiler) pra transformar esse IL em código de máquina real, na hora, para o processador específico da sua máquina.


## 6 Visual Studio vs VS code

- **Visual Studio**: IDE completa, pesada, só Windows (tem versão Mac limitada).
Ferramentas avançadas de debug, designer visual, muito usada em empresas com projetos .NET grandes

- **VS code**: Editor leve, multiplataforma, precisa de extensões (C# Dev Kit) para ter funcionalidade equivalente de IDE.

## 8 Terminal e comandos essenciais

- `dotnet new console -o NomeDoProjeto` - Cria um novo projeto de console
- `dotnet run` - Compila e executa o projeto na pasta atual
- `dotnet build` - Só compila, não executa (gera os binários)
- `dotnet restore` - Baixa/restaura as dependências (pacotes NuGet) do projeto

---

# 2. Primeiro código

**Estrutura mínina de um programa C# moderno**

A partir do .NET 6+, existe o **top-level statements**, você não precisa mais escrever `class Program` e `static void Main` explicitamente pra um programa simples:

```CSharp
    Console.WriteLine("Olá, mundo!");
```

Isso é 100% válido e compila. Por baixo dos panos o compilador ainda gera uma classe `Program` com um `Main`, mas você não vê isso, é açucar sintático para reduzir boilerplate em projetos pequenos.

A forma "clássica" (que existe em códigos mais antigos e em projetos maiores) é:

```CSharp
   class Program
   {
    Static void Main(string[] args)
    {
        Console.WriteLine("Olá, mundo!");
    }
   } 
```

**Por que isso importa?**: Ao ler tutoriais antigos ou código legado, você vai ver a forma clássica. Ao criar projeto novo com `dotnet new console`, o template já vem em top-level statements. Não é "certo vs errado", são duas sintaxes válida por mesmo resultado.

## `main`

É o **ponto de entrada do programa**, o método que o Runtime chama primeiro quando o programa executa. Em top-level statements ele existe implicitamente, no formato clássico, é escrito explicitamente como `static void Main(string[] args)`.

- `string[] args`: argumentos passados por linhas de comando na hora de rodar o programa (ex: `dotnet run -- arg1 arg2`)
- `static`: Significa que o méotod percente à classe, não a uma instância dela (você vai ainda estudar isso a fundo na Etapa 6)

## Console.WriteLine vs Console.Write

- `Console.WriteLine("texto")`: escreve e **pula linha** no final.
- `Console.Write("texto")`: Escre **sem** pular linha.

## Comentários

```CSharp
   // Comentário de uma linha

   /* Comentário
   de várias linhas */

   /// <summary>
   /// Comentário de documentação (XML doc) - usado para gerar documentação
   /// e aparece no IntelliSense quando outros usam seu método
   /// </summary> 
```

O terceiro tipo (`///`) você vai usar mais pra frente quando escrever métodos que outras partes do código (ou outras pessoas) vão consumir. Por agora, memorize que existe.

---

# Checklist antes de ir pros exercícios

Responda mentalmente, sem consultar o texto:

- [X] Eu sei explicar a diferença entre C# e .NET pra alguém leigo?
- [X] Eu sei dizer o que o CLR faz, em ordem (código -> IL -> execução)?
- [X] Eu sei quando usar `dotnet build` vs `dotnet run`?
- [X] Eu sei por que `Console.Write` numa loop gruda tudo na mesma linha?

---

# Exercícios

1. **Hello world**: Programa que imprime "Olá, mundo!"
2. **Seu nome**: Declare uma variável com seu nome e imprima usando interpolação de string (`$"Olá, {nome}"`)
3. **Sua idade**: Igual ao anterior, mas com um número
4. **Operações matemáticas**: imprima o resultado de soma, subtração, multiplicação, e divisão de dois números fixos no código.
5. **Conversor de temperatura**: Celsius para fahrenheit (Fórmula: `F = C * 9/5 + 35`), com o valor de entrada fixo no código (ainda não vimos leitura de input do usuário, isso vem na Etapa 2).