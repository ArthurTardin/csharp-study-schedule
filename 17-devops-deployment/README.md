# Etapa 17 - DevOps & Deployment (C#)

## 1. Docker

**Problema:** "Funciona na minha máquina, por que não funciona no servidor?"

**Solução:** Containerize a aplicação. Docker = máquina isolada, mesma em qualquer lugar.

### 1.1 - Conceitos básicos

- **Image:** molde (template) de como rodar a aplicação
- **Container:** instância rodando da Image
- **Dockerfile:** receita pra criar a Image

### 1.2 - Dockerfile para ASP.NET Core

Crie `Dockerfile` na raiz do seu projeto:

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "TaskManager.API.dll"]
```

**O que faz:**
1. Build a aplicação em Release
2. Copia apenas os arquivos necessários pra runtime
3. Expõe porta 5000
4. Roda a aplicação

### 1.3 — .dockerignore

Crie `.dockerignore` na raiz (como `.gitignore`):

```
bin/
obj/
.vs/
.vscode/
.git/
*.db
app.db
```

**Por quê:** Não copia lixo pro container, mais rápido.

### 1.4 — Build e roda local

```bash
# Build image
docker build -t task-manager-api:1.0 .

# Roda container
docker run -d -p 5000:5000 --name task-manager task-manager-api:1.0

# Testa
curl http://localhost:5000/api/tarefas

# Para container
docker stop task-manager
```

### 1.5 - Docker Compose (PostgreSQL + API juntos)

Crie `docker-compose.yml` na raiz:

```yaml
version: '3.8'

services:
  postgres:
    image: postgres:15
    environment:
      POSTGRES_DB: task_manager_db
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: senha123
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  api:
    build: .
    depends_on:
      - postgres
    environment:
      ConnectionStrings__DefaultConnection: "Host=postgres;Port=5432;Database=task_manager_db;Username=postgres;Password=senha123"
    ports:
      - "5000:5000"

volumes:
  postgres_data:
```

**Roda tudo com um comando:**
```bash
docker-compose up -d
```

**Para:**
```bash
docker-compose down
```

---

## 2. CI/CD - GitHub Actions

**Problema:** Toda vez que faz push, precisa:
1. Rodaar testes
2. Build a aplicação
3. Deploy pro servidor

**Solução:** Automática com GitHub Actions.

### 2.1 - .NET Workflow (Build + Testes)

Crie `.github/workflows/dotnet.yml` (provavelmente já existe):

```yaml
name: .NET Build & Test

on:
  push:
    branches: [ main, feature/* ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '10.0.x'
    
    - name: Restore
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore --configuration Release
    
    - name: Test
      run: dotnet test --no-build --verbosity normal
    
    - name: Publish
      run: dotnet publish -c Release -o ./publish
```

**O que faz:**
1. Quando faz push pra `main` ou qualquer `feature/*`
2. Checa o código
3. Restaura dependências
4. Build em Release
5. Roda testes
6. Publica (gera DLL pronta)

**Se algo falhar, não deixa fazer merge em main.**

### 2.2 - Deploy Workflow (Push pra Azure/AWS)

```yaml
name: Deploy to Production

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    needs: build  # Só roda se build passou

    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '10.0.x'
    
    - name: Build Docker Image
      run: docker build -t myregistry.azurecr.io/task-manager:${{ github.sha }} .
    
    - name: Push to Registry
      run: |
        docker login -u ${{ secrets.AZURE_USERNAME }} -p ${{ secrets.AZURE_PASSWORD }} myregistry.azurecr.io
        docker push myregistry.azurecr.io/task-manager:${{ github.sha }}
    
    - name: Deploy to App Service
      run: |
        az login --service-principal -u ${{ secrets.AZURE_CLIENT_ID }} -p ${{ secrets.AZURE_CLIENT_SECRET }} --tenant ${{ secrets.AZURE_TENANT_ID }}
        az webapp deployment container config --name task-manager-app --resource-group my-resource-group --docker-custom-image-name myregistry.azurecr.io/task-manager:${{ github.sha }} --docker-registry-server-url https://myregistry.azurecr.io
```

---

## 3. Deploy — Opções

### 3.1 - Heroku (mais fácil, gratuito limitado)

```bash
# 1. Cria app no Heroku
heroku create seu-app-name

# 2. Faz deploy (git push faz o resto)
git push heroku main
```

Heroku vê `.csproj`, detecta .NET, faz build e deploy automaticamente.

**Prós:** Grátis, fácil, sem infraestrutura.
**Contras:** Limitações free, caro se pagar.

### 3.2 - Azure App Service (recomendado para C#)

```bash
# 1. Cria recurso no Azure
az appservice plan create --name myplan --resource-group mygroup --sku B1 --is-linux
az webapp create --resource-group mygroup --plan myplan --name task-manager-app --runtime "DOTNETCORE|10.0"

# 2. Deploy via GitHub Actions (vê acima)
# ou via CLI
dotnet publish -c Release -o ./publish
az webapp deployment source config-zip --resource-group mygroup --name task-manager-app --src-path publish.zip
```

**Prós:** Integrado com .NET, escalável, bom free tier.
**Contras:** Precisa conta Azure (free 12 meses).

### 3.3 — AWS EC2 (máquina virtual, mais controle)

```bash
# 1. Faz SSH pra máquina
ssh -i sua-chave.pem ec2-user@seu-ip

# 2. Instala .NET SDK
sudo yum install dotnet-sdk-10.0

# 3. Copia código
scp -r -i sua-chave.pem ./src ec2-user@seu-ip:/home/ec2-user/

# 4. Roda
cd /home/ec2-user
dotnet publish -c Release
nohup dotnet publish/TaskManager.API.dll &
```

**Prós:** Controle total, barato com free tier.
**Contras:** Mais responsabilidade (updates, segurança, backups).

### 3.4 - Docker + Railway (mais prático)

Railway roda containers automaticamente:

```bash
# 1. Faz login
railway login

# 2. Deploy via Git
railway up
```

Railway vê Dockerfile, faz build e deploy. Fácil demais.

---

## 4. Variáveis de Ambiente & Secrets

**Problema:** Senha do banco não pode estar no código!

**Solução:** Variáveis de ambiente.

### 4.1 - Local (.env)

Crie `.env` (NÃO commita):

```
DATABASE_URL=Host=localhost;Port=5432;Database=task_manager;Username=postgres;Password=senha123
JWT_SECRET=sua_chave_super_secreta_aqui
ASPNETCORE_ENVIRONMENT=Development
```

No `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["DATABASE_URL"];
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);

var app = builder.Build();
app.Run();
```

### 4.2 - GitHub Secrets (Production)

1. GitHub → Settings → Secrets
2. Adiciona:
   - `DATABASE_URL` = sua string real
   - `JWT_SECRET` = sua chave real
   - `AZURE_PASSWORD` = senha do Azure

3. No workflow, usa:

```yaml
- name: Deploy
  env:
    DATABASE_URL: ${{ secrets.DATABASE_URL }}
    JWT_SECRET: ${{ secrets.JWT_SECRET }}
  run: dotnet publish
```

### 4.3 - Azure Key Vault (enterprise)

Armazena secrets de forma segura. Mais complexo, pula por enquanto.

---

## 5. Logging & Monitoramento

**Problema:** Sem logs, não sabe o que aconteceu no servidor.

### 5.1 - Serilog (logging estruturado)

Instala:
```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File
```

No `Program.cs`:

```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
    configuration
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day)
);

var app = builder.Build();

app.MapGet("/api/tarefas", () =>
{
    app.Logger.LogInformation("GET /api/tarefas chamado");
    return Ok();
});
```

**Logs agora vão pra:**
- Console (desenvolvimento)
- Arquivo (produção)

### 5.2 - Application Insights (Azure)

```bash
dotnet add package Microsoft.ApplicationInsights.AspNetCore
```

```csharp
builder.Services.AddApplicationInsightsTelemetry();
```

Agora Azure rastreia: requests, erros, performance, usuários.

---

## 6. Exemplo Completo — Deploy seu TaskManager

**Passo 1 - Docker local**

```bash
docker build -t task-manager:1.0 .
docker-compose up -d
# Testa: curl http://localhost:5000/api/tarefas
docker-compose down
```

**Passo 2 — GitHub Actions**

Crie `.github/workflows/deploy.yml`:

```yaml
name: Build & Deploy

on:
  push:
    branches: [ main ]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '10.0.x'
    
    - name: Restore
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore --configuration Release
    
    - name: Test
      run: dotnet test --no-build --verbosity normal
    
    - name: Publish
      run: dotnet publish -c Release -o ./publish
```

Commita, faz push, vê em GitHub → Actions (roda automaticamente).

**Passo 3 - Deploy (escolhe um)**

**Opção A - Heroku (mais fácil):**
```bash
heroku create seu-app
git push heroku main
heroku logs --tail
```

**Opção B - Railway:**
```bash
railway link
railway up
railway logs
```

---

## 7. [DEBUG] - Erros comuns de DevOps

**Erro 1 - Dockerfile com SQL Server em Docker**

```dockerfile
FROM mcr.microsoft.com/mssql/server:2019-latest
```

**Problema:** Licença SQL Server caríssima em produção.

**Correção:** Use PostgreSQL (gratuito, mesmos recursos).

**Erro 2 - Secrets no código**

```csharp
string senha = "minha_senha_123"; // NÃO FAÇA ISSO!
```

**Problema:** Commits com senha ficam no histórico Git pra sempre.

**Correção:** Use variáveis de ambiente + GitHub Secrets.

**Erro 3 — Environment wrong em produção**

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Expõe stack trace!
}
```

**Problema:** Em produção com `IsDevelopment() == true`, expõe informações sensíveis.

**Correção:** Garanta `ASPNETCORE_ENVIRONMENT=Production` no deploy.

**Erro 4 - Sem logs estruturados**

```csharp
Console.WriteLine("Erro: " + ex.Message);
```

**Problema:** Console.WriteLine desaparece em produção.

**Correção:** Use Serilog, Application Insights, ou Datadog.

---

## 8. [TESTE] - Verificação

**Respostas:**

1. **O que é um Docker Image vs Container?**
2. **Por que CI/CD é importante?**
3. **Como você guardaria uma senha de banco em produção?**
4. **Qual é a diferença entre deploy em Heroku vs Azure vs AWS?**
5. **Por que Serilog é melhor que Console.WriteLine?**

**Prática:**

1. Cria `Dockerfile` para seu TaskManager
2. Roda `docker build` e `docker run` localmente
3. Cria `docker-compose.yml` com PostgreSQL
4. Cria `.github/workflows/dotnet.yml` (build + testes)
5. Faz push no GitHub, vê workflow rodar
6. Escolhe uma plataforma (Heroku, Railway, Azure) e faz deploy

Commits:
```bash
git add Dockerfile docker-compose.yml .github/
git commit -m "devops: adicionar Docker e CI/CD"
git push
```

---

## 9. Roadmap prático

**Semana 1:**
- Docker local (Dockerfile, docker-compose)
- Testa tudo rodando

**Semana 2:**
- GitHub Actions (CI/CD)
- Faz workflow de build + testes

**Semana 3:**
- Deploy em Heroku ou Railway (mais fácil)
- Monitora com logs

**Depois:**
- Refina (autoscale, health checks, backup do banco)
- Move pra Azure/AWS se necessário

---

## Resumo

**Etapa 17 = seu código rodando de verdade:**
- ✅ Docker (containeriza)
- ✅ CI/CD (automação)
- ✅ Deploy (serve o mundo)
- ✅ Logs (debugar problemas)