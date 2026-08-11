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

# 6 Visual Studio

O **Visual Studio** é um IDE (*Integrated Development Environment*) completa da Microsoft.
Você pode escrever:
```csharp
    Console.WriteLine("Hello World!");
```
E executar/debugar diretamente pelo Visual Studio.

## Quando usar?

Ele é particularmente forte para:
- C#
- .NET
- ASP.NET
- APIs
- Aplicações desktop
- debugging
- projetos grandes

---

# 7 VS code

O **Visual Studio Code** é um editor de código.

Ele é diferente do Visual Studio.
Apesar dos nome semelhantes, são produtos diferentes. O VS Code é mais leve e extremamente extensível. Para trabalhar com C#, você instala as extensões apropriadas e utiliza o **.NET SDK** instalado no computador.

---

# 8 Terminal

O terminal permite interagir com o sistema usando comandos de texto.
No windows, você pode usar:
- PowerShell
- Windows Terminal
- Prompt de comando
No Linux/macOS, existem outros shells, como Bash. Para C#/.NET, você utiliza bastante o comando: `dotnet`
Por exemplo:
```bash
    dotnet --version
```
O terminal é importante porque é preciso aprender a trabalhar com o **.NET CLI (Command-line Interface)**

---

# 9 `dotnet new`

Esse comando cria projetos a partir de templates.
Por exemplo:
```bash
    dotnet new console
```
Isso cria um projeto de aplicação de console.
Você pode especificar uma pasta.
```bash
    dotnet new console -n MeuPrimeiroProgram
```
## O que é `.csproj`?

É o arquivo que descreve o projeto **.NET**.
Por exemplos, ele pode definir:
- Framework utilizado
- Dependências
- Configurações
- Propriedades do projeto

---

# 10 `dotnet run`

Executa o projeto.
Se você estiver dentro da pasta do projeto:
```bash
    dotnet run
```
Por exemplo:
```csharp
    Console.WriteLine("Olá, mundo!");
```
Executando:
```bash
    dotnet run
```
Saída:
```text
    Olá, mundo!
```

O `dotnet run`normalmente cuida das etapas necessárias para você conseguir executar o projeto.

---

# 11 `dotnet build`

Compila o projeto
```bash
    dotnet build
```

Se houver um erro como:
```csharp
    int idade = "16";
```
O compilador vai reclamar, pois `"16"` é uma `string`, não um `int`

---

# 12 `dotnet restore`

Esse comando restaura as dependências do projeto. Hoje, em muitos casos, **nem precisa executar `dotnet restore` Manualmente**, porque comando como `dotnet build` e `dotnet run` podem realizar a restauração automaticamente quando necessário.

---