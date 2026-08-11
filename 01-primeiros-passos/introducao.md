# 1 O que é C#?

O C# (lê-se CSharp) é um **Moderno, inovador, software livre, plataforma cruzada** linguagem de programação orientada a objeto, de alto nível, desenvolvida pela **Microsoft** em 2002, como parte da plataforma **.NET** e uma das cinco principais linguagens de programação no GitHub. Ela é usada para criar aplicativos web, móveis, de desktop e jogos eletrônicos

## Principais Características

- **Orientação a Objeto**: Organiza o código em classes e objetos para facilitar a reutilização.
- **Tipagem forte**: Garante segurança ao tipo de dados e previne erros comuns de programação.
- **Multiplataforma**: Funciona em Windows, Linux e macOS por meio do ecossistema .NET.
- **Sintaxe Familiar**: Semelhante a C, C++ e Java, o que facilita o aprtendizado.

---

# 2 O que é .NET?

O **Microsoft .NET** é uma plataforma de desenvolvimento gratuita, de código aberto e multiplataforma. Criada e mantida pela **Microsoft** e pela comunidade, ela fornece ferramentas, bibliotecas e um ambiente de execução para criar diferentes tipos de aplicativos, como sistema web, mobile, desktop, jogos e soluções de **IoT**.

## Principais componentes e linguagens
- **Linguagens compatíveis**: C#, F# e Visual Basic.
- **Bibliotecas prontas**: Conjunto amplo de funções para tarefas comuns como acesso a dados, redes e manipulação de arquivos.
- **Ambiente de execução(CLR)**: Gerencia a execução do código, fazendo a gestão de memória e a coleta de lixo (*Garbage Collector*).

## Evolução da tecnologia

- **.NET Framework**: A versão original e mais antiga, focada **exclusivamente** no sistema operacional Windows.
- **.NET (anteriormente .NET Core)**: A versão moderna, de código aberto e totalmente multiplataforma, que roda em Windows, Linux e macOS.

---

# 3 O que é .NET SDK

O **Microsoft .NET SDK** (Software Development Kit) é um pacote completo de ferramentas de programação, bibliotecas e compiladores que permite criar, compilar, testar e executar aplicativos desenvolvidos para a plataforma **.NET**.

## O que ele inclui

- **CLI do .NET (.NET CLI)**: Ferramenta de linha de comando (comando `dotnet`) para criar projetos, restaurar dependências e rodar aplicações.
- **Runtime e Bibliotecas**: O ambiente necessário para executar o código, o qual já vem embutido no SDK.
- **Compilador e Ferramentas de Build (MSBuild)**: Responsáveis por transformar o código escrito em linguagens como C#, F# ou Visual Basic em programas executáveis.

## Para que serve e quem precisa dele
- **Desenvolvedores**: É essencial para quem escreve códigos ou mantém projetos baseados em .NET.
- **Usuários comuns**: Não é obrigatório se o objetivo for apenas usar o computador, embora alguns programas o instalem automaticamente para permitir o funcionamento de ferramentas específicas de desenvolvimento.

---

# 4 Runtime em C#

**Runtime** é o momento em que seu programa está **em execução**, depois de compilado, enquanto está rodando de fato na máquina do usuário.

## As duas fases de um Programa C#

1. **Compile time (tempo de compilação)**
    - O código C# é convertido em **IL** (*Intermediate Language*), empacotado num `.dll` ou `.exe`.
    - Erros de sintaxe, tipos incompatíveis, etc. São pegos aqui.
2. **Runtime (tempo de execução)**
    - O **CLR** (*Common language Runtime*), parte do .NET, pega esse IL e faz a compilação **JIT** (*Just-In-Time*) para código de máquina nativo, e então executa
    - É aqui que o programa realmente "roda": lê arquivos, recebe input do usuário, faz cálculos, etc.
    - Erros que só aparecem aqui são chamados de **runtime errors** (ex: `NullReferenceException`, `DivideByZeroException`), coisas que o compilador não conseguiu prever.

## Exemplo prático

```csharp
    int[] numeros = {1, 2, 3};
    Console.WriteLine(numeros[5]); // compila sem erro!
```
Esse código **compila perfeitamente** (sintaxe válida, tipos corretos), mas em **runtime** vai lançar um `IndexOutOfRangeException`, porque o array só tem índices de 0 a 2.

## O "Runtime" como coisa concreta

Em C#/.NET, "the runtime" também se refere ao próprio **CLR**, o ambiente que:
- Gerencia memória (Garbage Collector)
- Faz a compilação JIT
- Trata exceções
- Verifica tipos em tempo de execução
- Gerencia threads

---

# 5 CLR

**CLR = Common Language Runtime**
É um ambiente de execução responsável por executar aplicações .NET e fornecer serviços fundamentais durante a execução.

Quando escreve:

```csharp
    int x = 10;
    Console.WriteLine(x);
```

O computador não executa diretamente o código-fonte C#.

## IL

O código C# é compilado para uma linguagem intermediária chamada **IL (Intermediate language)**, também conhecida como CIL.
Depois, durante a execução, o **JIT (Just-In-Time compiler)** transforma o IL em código nativo que o processador pode executar.

## E o CLR?

O CLR gerencia várias coisas durante a execução, como:
- Execução do código
- Gerenciamento de memória
- Garbage Collector
- Tratamento de exceções
- Segurança
- Interoperabilidade
- JIT compilation

**O CLR é uma das principais partes do .NET Runtime responsável por fonecer o ambiente de execução das aplicações .NET.**

---