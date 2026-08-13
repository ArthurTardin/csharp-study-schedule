# Cronograma de estudo - C#

## Objetivo

Documentar minha jornada de aprendizado em C#, desde os fundamentos da linguagem até o desenvolvimento de aplicações profissionais, registrando conceitos estudados, exercícios, projetos e minha evolução ao longo do tempo.

## Estrutura

### Etapa 1 - Primeiros Passos

#### 1 - Ambiente

- O que é C#
- O que é .NET
- .NET SDK
- Runtime
- CLR
- Visual Studio
- VS code
- Terminal
- `dotnet new`
- `dotnet run`
- `dotnet build`
- `dotnet restore`

#### 2 - Primeiro código

- Main
- Console.WriteLine
- Console.Write
- Comentários
- Estrutura de um programa

#### Exercícios

- Hello World
- Seu nome
- Sua idade
- Operações matemáticas
- Conversor de temperatura

### Etapa 2 - Variáveis e tipos

#### Tipos

- `int`
- `long`
- `short`
- `byte`
- `double`
- `float`
- `decimal`
- `bool`
- `char`
- `string`

#### Conceitos

- Declaração
- Inicialização
- Atribuição
- Constrantes
- `var`
- escopo
- conversão de tipos
- casting
- `Parse`
- `TryParse`

#### Exercícios

- IMC
- Média Escolar
- Conversor de moedas
- Conversor de unidades
- Cálculo de salário

### Etapa 3 - Estruturas de controle

#### Condicionais

- `if`/`else`/`else if`
- `switch`/`switch expression`
- Operadores relacionais (`==, !=, >, <, >=, <=`)
- Operadores lógicos (`&&, ||, !`)
- Operador ternário

#### Laços de repetição

- `for`
- `while`
- `do-while`
- `foreach`
- `break`
- `continue`

#### Exercícios

- par ou ímpar
- tabuada
- fibonacci
- verificador de senha
- jogo de adivinhação de número
- Menu de opções no console

### Etapa 4 - Estruturas de dados

#### Conceitos

- Arrays (unidimensionais e multidimensionais)
- List<T>
- Dictionary<TKey, TValue>
- Queue<T>e Stack<T>
- HashSet<T>
- Percorrendo coleções (for, foreach)
- Métodos de coleção (Add, Remove, Contains, Sort)

#### Exercícios

- Lista de compras
- Agenda de contatos (Dictionary)
- Cadastro de alunos com notas
- fila de atendimento (Queue)
- pilha de histórico (Stack)

### Etapa 5 - Métodos

#### Conceitos
- Declaração e chamada de métodos
- Parâmetros e retorno
- Sobrecarga de método (overload)
- Parâmetros opcionais e nomeados
- ref, out e in
- params
- Recursão
- Métodos de extensão (introdução)

#### Exercícios

- Calculadora com método separados
- Verificador de número primo (recursivo e iterativo)
- Validador de CPF
- Biblioteca de utilitários matemáticos

### Etapa 6 - Programação Orientada a Objetos (Fundamentos)

#### Conceitos

- Classes e objetos
- Atributos e propriedades (get/set)
- construtores
- this
- Encapsulamento
- Membros estático (static)
- Métodos de instância vs métodos estáticos
- sobrecarga de construtores

#### Exercícios

- Classe pessoa / Aluno / Produto
- Sistema de cadastro simples (CRUD em memória)
- Classe conta bancária (Depósito, saque, saldo)

### Etapa 7 - Programação Orientada a objetos (Avançado)

#### Conceitos

- Herança
- Polimorfismo
- Classes abstratas (abstract)
- Interfaces
- override, virtual, new
- sealed
- Modificadores de acesso (public, private, protected, internal)
- Composição vs herança

#### Exercícios

- Sistema de formas geométricas (áreas e perímetro polimórficos)
- Sistema de funcionários (Herança: Gerente, vendedor)
- Sistema de veículos com interfaces (IMovable, IRefuelable)

### Etapa 8 - Tratamento de exceções

#### Conceitos

- Try/Catch/finally
- throw e throw ex
- exceções built-in (DivideByZeroException, NullReferenceException, etc)
- Exceções customizadas
- Boas práticas de tratamento de erros

#### Exercícios

- Calculadora com tratamento de divisão por zero
- Validador de entrada de dados robustos
- Sistema de login com exceções customizadas

### Etapa 9 - Coleções avançadas, Delegates e LINQ

#### Conceitos

- Delegates
- Lambda expressions
- Func, Action, predicate
- Events
- LINQ (Where, Select, OrderBy, GroupBy, FirstOrDefault, Any, All, Sum, Count)
- LINQ Method Syntax vs Query Syntax

#### Exercícios

- Filtro de produtos por categoria/preço com LINQ
- Sistema de notificação com events
- Relatório de evendas agrupdo por LINQ

### Etapa 10 - Manipulação de arquivos e serialização

#### Conceitos

- File, Directory
- StreamReader / StreamWriter
- Leitura e escrita de arquivos texto e CSV
- Serialização e desserialização JSON (System.Text.Json)
- Serialização XML (Introdução)

#### Exercícios

- Sistema de log em arquivo
- Exportador de dados para CSV
- Salvar e carregar cadastro de usuários em JSON

### Etapa 11 - Programação Assíncrona

#### Conceitos

- Threads (introdução)
- Task e Task<T>
- async / await
- Task.whenAll / Task.When.Any
- Programação Assíncrona em I/O (arquivos, requisições HTTP)
- CancellationToken

#### Exercícios

- Download assíncrono de múltiplos arquivos
- Consumo de API pública com HttpClient
- Simulador de processamento paralelo de pedidos

### Etapa 12 - Banco de dados

#### Conceitos

- ADO.NET (conexão, comandos, DataReader)
- Entity Framework Core
- Code first vs Database First
- Migrations
- DbContext e Dbset
- LINQ to entities
- Relacionamentos (1:1. 1:n, N:N)
- MySQL e PostgreSQL com EF Core

#### Exercícios

- CRUD completo com Entity Framework
- Sistema de biblioteca (livros, autores, empréstimos) com relacionamentos
- Migração de banco com seed de dados

### Etapa 13 - Testes automatizados

#### Conceitos

- Testes unitários
- XUnit ou NUnit
- Arrange, Act, Assert
- Macking (Moq)
- Introdução a TDD

#### Exercícios

- Teste unitários para as classes de negócios já criadas
- Cobertura de testes de um serviço de validação
- Refatorar um projeto anterior seguindo TDD

### Etapa 14 - Desenvolvimento Web com ASP.NET Core

#### Conceitos

- Estrutura de um projeto ASP.NET Core
- WEB API vs MVC vs Razor Pages
- Controllers e routing
- Dependency injection
- Middleware
- DTOs
- Autenticação e autorização (JWT)
- Swagger/OpenAPI

#### Exercícios

- API REST de tarefas (To-DO-List)
- API de cadastro de produtos com autenticação JWT
- Sistema de blog com web API + banco de dados

### Etapa 15 - Boas práticas e arquitetura

#### Conceitos

- Princípios SOLID
- CLean Code
- Design PAtterns (repository, Singleton, factory, strategy)
- Clean Architecture (introdução)
- Separação em camadas (controller, service, repository)
- injeção de dependência avançada

#### Exercícios

- Refatorar uma API existente aplicando repository Pattern
- Reestruturar um projeto em camadas
- Implementar um Design Pattern em um projeto próprio

### Etapa 16 - Ferramentas e workflow profissional

#### Conceitos

- Git avançado (Branches, rebase, merge, pull requests)
- Docker (Dockerfile, docker-compose)
- CI/CD (introdução com GitHub Actions)
- Logging estruturado (Serilog)
- Versionamento de API
- Deploy em nuvem (AWS/Azure - introdução)

#### Exercícios

- Dockerizar uma API já criada
- Criar pipeline simples em CI no GitHub Actions
- Deploy de uma API em ambiente de nuvem

### Etapa 17 - Projeto final profssional

#### Objetivo

Consolidar todo o aprendizado em um projeto completo, aplicando arquitetura em camadas, banco de dados relacional, autenticação, testes e deploy

#### Sugestões de projeto

- API REST completa de e-commerce (produtos, pedidos, usuários, autenticação, JWT)
- Sistema de gestão (financeiro, estoque ou tarefas) com Front-end em React/Next.js consumindo a API
- Deploy completo com Docker + banco de dados em nuvem.
