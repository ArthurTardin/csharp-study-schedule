# Etapa 16 - Projeto Real (C#)

---

## 1. Escopo — O que você vai fazer

**Escolha UMA dessas ideias, ou defina a sua:**

### Opção A — API de Gerenciador de Tarefas
- Criar, listar, modificar, deletar tarefas
- Categorias de tarefas
- Marcar como concluído
- Filtrar por status
- Usuários (login básico com JWT, opcional)
- Banco: SQLite ou PostgreSQL
- Testes: unitários + integração

### Opção B — API de Blog
- Posts (criar, editar, deletar, listar)
- Comentários em posts
- Categorias
- Busca por título/conteúdo
- Admin (criar/editar posts) vs leitor (ver posts)
- Banco: SQLite ou PostgreSQL
- Testes: CRUD básico

### Opção C — API de Biblioteca
- Livros (cadastro, busca, filtro por autor/gênero)
- Empréstimos (quem pegou qual livro, quando devolve)
- Usuários
- Histórico de empréstimos
- Banco: SQLite ou PostgreSQL
- Testes: empréstimo, devolução, disponibilidade

### Opção D — Sua ideia
Desde que seja:
- **Pequeno o suficiente pra terminar em 2-4 semanas** (seu tempo livre)
- **Grande o suficiente pra ter**: banco, testes, múltiplos endpoints, alguma lógica
- **Claro nos requisitos** (você escreve num documento antes de codar)

---

## 2. Estrutura do Projeto

```
seu-projeto/
├── src/
│   ├── API/                    # ASP.NET Core project
│   │   ├── Controllers/
│   │   ├── Models/
│   │   ├── Services/
│   │   ├── Data/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   └── Tests/                  # xUnit project
│       ├── UnitTests/
│       └── IntegrationTests/
├── docs/                       # Documentação
│   └── API.md                  # endpoints, exemplos
├── .gitignore                  # git ignore
├── README.md                   # como usar, instalar, rodar
└── .git/                       # repositório git

```

---

## 3. Ciclo de Desenvolvimento

### Fase 1 - Planejamento (1-2 dias)

**Documento de requisitos (`REQUISITOS.md`):**

```markdown
# Requisitos do Projeto: [Nome]

## Objetivo
[Uma frase clara do que o projeto faz]

## Requisitos Funcionais
1. Usuário pode criar [coisa]
2. Usuário pode listar [coisa]
3. Usuário pode modificar [coisa]
4. Usuário pode deletar [coisa]
5. [Recurso específico]

## Requisitos Técnicos
- Banco: SQLite / PostgreSQL
- Framework: ASP.NET Core
- Testes: xUnit, mínimo 80% cobertura
- Documentação: README + exemplos de API

## Fora do escopo
- Autenticação complexa (JWT é opcional)
- UI (apenas API REST)
- Deploy em produção real (localhost + instrução de deploy local)

## Estimativa
Término em: [2-4 semanas, X horas/semana]
```

### Fase 2 — Estrutura Base (1-2 dias)

1. Criar projeto ASP.NET Core
2. Estrutura de pastas (Controllers, Models, Services, Data)
3. Conectar banco
4. Primeira migration
5. Primeiro controller com endpoint GET dummy
6. Confirmar que roda

### Fase 3 - Desenvolvimento (2-3 semanas)

Ciclo por feature:

1. **Escrever teste** (TDD)
2. **Implementar** (fazer teste passar)
3. **Refatorar** (se necessário)
4. **Commit** (mensagem descritiva)

Exemplo commit:
```
feat: adicionar endpoint POST /tarefas
- criar tarefa com título, descrição, categoria
- validar campos obrigatórios
- retornar HTTP 201 com tarefa criada
- teste: POST com dados válidos e inválidos
```

### Fase 4 - Documentação (2-3 dias)

**README.md:**
- O que o projeto faz (1 parágrafo)
- Como instalar (passo a passo)
- Como rodar (comando `dotnet run`)
- Como testar (comando `dotnet test`)
- Estrutura do projeto (pastas, arquivos importantes)
- Exemplos de uso (curl ou Postman)
- Limitações conhecidas

**docs/API.md:**
```markdown
# API Reference

## GET /api/tarefas
Retorna lista de tarefas.

Request:
```
GET /api/tarefas
```

Response (HTTP 200):
```json
[
  {"id": 1, "titulo": "Comprar leite", "concluido": false}
]
```

## POST /api/tarefas
...
```

### Fase 5 - Cleanup (1 dia)

1. Remover código comentado ou unused
2. Confirmar que testes passam
3. Verificar `.gitignore` (sem `bin/`, `obj/`, `.db`)
4. Último commit: "docs: finalizar projeto"

---

## 4. Checklist de Qualidade

Antes de "terminar", confirme:

- [ ] **Funcionalidade**: todos os endpoints do escopo funcionam
- [ ] **Testes**: mínimo 1 teste por endpoint (unitário ou integração)
- [ ] **Banco**: dados persistem, migrations rodam clean
- [ ] **Validação**: inputs são validados (HTTP 400 se inválido)
- [ ] **Erros**: tratamento de exceção (não retorna stack trace pro cliente)
- [ ] **Git**: commits descritivos, histórico limpo (sem "teste123", "aaa", etc)
- [ ] **README**: alguém consegue seguir o passo a passo
- [ ] **Código**: sem grandes code smells (métodos gigantes, lógica no controller, etc)
- [ ] **Execução local**: `dotnet run` funciona em máquina limpa (novo clone do repo)

---

## 5. Exemplo de Como Começar

**Dia 1 — Setup:**
```bash
# Criar repo local
mkdir meu-projeto
cd meu-projeto
git init

# Criar projeto ASP.NET Core
dotnet new webapi -n API
cd API
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package xunit
dotnet add package Moq

# Fazer primeiro commit
git add .
git commit -m "init: scaffold ASP.NET Core project"
```

**Dia 2 — Primeira feature (Tarefas - GET todos):**
1. Escrever teste que espera GET /api/tarefas retornar lista vazia
2. Criar controller com endpoint
3. Teste passa
4. Commit: `feat: GET /api/tarefas`

**Dia 3 — POST tarefas:**
1. Teste: POST com título → retorna 201
2. Implementar
3. Teste: POST sem título → retorna 400
4. Implementar validação
5. Commit: `feat: POST /api/tarefas com validação`

...e assim por diante.

---

## 6. GitHub - Tornando isso Portfólio

**Quando subir pro GitHub:**

1. Criar repositório público
2. Nome claro: `task-manager-api`, `blog-api`, etc (não `projeto`, `teste`)
3. Descrição uma linha: "REST API de gerenciador de tarefas em C# com ASP.NET Core"
4. README visível, bem formatado
5. Pins: colar link do projeto no seu perfil GitHub

**O que recrutadores veem:**
- Commit history (regularidade, mensagens descritivas)
- README (profissionalismo)
- Código (estrutura, testes, sem comentários óbvios)
- Issues/PRs (se houver — mostra iteração)

**Não precisa:**
- Ser perfeito
- Ter 10k linhas de código
- Ter todas as features possíveis

Precisa:
- Funcionar
- Estar bem documentado
- Mostrar que você entende o que fez

---

## 7. Antes de Começar

**Pense e escreva:**

1. **Qual projeto você vai fazer?** (qual das 4 opções, ou sua ideia)
2. **Qual é o MVP** (minimum viable product — o mínimo pra "pronto")?
3. **Quanto tempo você tem?** (2 semanas? 4? horas/dia?)
4. **Qual banco?** (SQLite = mais fácil, PostgreSQL = mais real)

Manda essas respostas e depois você começa o desenvolvimento.

---

## Não há [DEBUG] ou [TESTE] formal nessa etapa

O projeto TODO é seu debug + teste. Você vai ter que:
- Debugar quando coisa quebra
- Escrever testes enquanto desenvolve (TDD)
- Validar manualmente (REST Client)

Isso é prático, não exercício.

---

## Resumo

**Etapa 16 = projeto real, do zero até GitHub pronto pra mostrar.**

Não é perfeição, é **aprendizado prático e portfólio**. Você sai daqui sabendo:
- Como estruturar uma aplicação
- Como trabalhar com git de verdade
- Como documentar código
- Como testar enquanto desenvolve
- Como entregar algo "pronto"

Próxima etapa (15 ou 17) você faz depois, ou em paralelo.