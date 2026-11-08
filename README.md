# 🌐 BeaconSync — Rastreador de Vulnerabilidades & Gestão de Apoio Social

> **Global Solution 2026/2 · CitizenTech · FIAP**  

---

## 👥 Integrantes da Equipe

* **Pamella Christiny Chaves Brito** | RM565206
* **Felipe Ribeiro Salles de Camargo** | RM565224
* **João Pedro Pereira Camilo** | RM562005
* **Lucas Matsubara Reis** | RM565020

---

## 💡 Resumo da Solução

O **BeaconSync** é uma plataforma corporativa desenvolvida em **API RESTful com .NET 10 (ASP.NET Core)** sob os rigorosos princípios de **Clean Architecture**. O principal objetivo do sistema é centralizar, gerenciar e otimizar o ecossistema de apoio social e rastreamento de vulnerabilidades. 

A solução resolve desafios críticos de engenharia de software corporativa através de:
* **Arquitetura Modular:** Separação estrita de responsabilidades entre `Domain`, `Application`, `Infrastructure`, `API` e camadas de teste.
* **Segurança e Resiliência:** Mecanismos robustos de autenticação baseada em **JWT**, proteção contra ataques de força bruta e uso excessivo através de **Rate Limiting** (retornando `429 Too Many Requests`), além de **Idempotência** via cache em memória para prevenir operações duplicadas em requisições de escrita (`POST`).
* **Persistência Corporativa:** Integração nativa com **Oracle Database** utilizando mapeamentos em maiúsculas via Entity Framework Core para total compatibilidade com os padrões de banco de dados corporativos da FIAP.
* **Qualidade Assegurada:** Suíte robusta de **testes unitários** (com Moq) e **testes de integração** (com xUnit e WebApplicationFactory) cobrindo o comportamento dos serviços e endpoints.

---

## 🔒 Segurança e Credenciais (Modelo de Exemplo)

Por questões de segurança e boas práticas de desenvolvimento, **todas as credenciais sensíveis (como strings de conexão do Oracle Database e chaves secretas do JWT)** foram removidas do código-fonte e do arquivo de configuração versionado (`appsettings.json`). 

Este repositório serve como um **template/exemplo de arquitetura** e para executá-lo localmente, é necessário configurar as suas próprias credenciais seguindo os passos da seção abaixo.

---

## ⚙️ Como Configurar e Executar o Projeto

### 1. Pré-requisitos
* [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado na sua máquina.
* Acesso a uma instância ativa do **Oracle Database** (ou Oracle XE).

### 2. Configurando as Credenciais Locais
Abra o arquivo **`appsettings.json`** (localizado no projeto `BeaconSync.API`) e configure os seus dados de acesso nos campos correspondentes:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/XEPDB1"
  },
  "JwtSettings": {
    "Key": "SUA_CHAVE_SECRETA_JWT_SUPER_SEGURA_COM_NO_MINIMO_32_CARACTERES"
  }
}

Substitua SEU_USUARIO, SUA_SENHA, o host do banco de dados e a chave secreta do JWT pelos valores reais do seu ambiente Oracle e da sua política de segurança.

3. Aplicando as Migrações no Banco de Dados
Com as credenciais configuradas, abra o terminal na raiz do projeto e execute o comando do Entity Framework para gerar as tabelas no seu Oracle:

Bash
dotnet ef database update --project BeaconSync.Infrastructure/BeaconSync.Infrastructure.csproj --startup-project BeaconSync.API/BeaconSync.API.csproj
4. Executando a API
Inicie a aplicação executando:

Bash
dotnet run --project BeaconSync.API/BeaconSync.API.csproj
Acesse a documentação interativa do Swagger diretamente no navegador através de: http://localhost:5006/.

🧪 Executando os Testes Automatizados
O projeto conta com validações automatizadas cobrindo regras de negócio e contratos de endpoints. Para rodar toda a suíte de testes unitários e de integração:

Bash
dotnet test
© 2026 BeaconSync · Todos os direitos reservados.