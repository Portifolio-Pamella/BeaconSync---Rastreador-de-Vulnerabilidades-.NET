# 🌐 BeaconSync — Rastreador de Vulnerabilidades & Gestão de Apoio Social

> **Global Solution 2026/2 · CitizenTech · FIAP**  

---

## 👥 Integrantes da Equipe

* **Pamella Christiny Chaves Brito** | RM565206
* **Felipe Ribeiro Salles de Camargo** | RM565224
* **João Pedro Pereira Camilo** | RM562005
* **Lucas Matsubara Reis** | RM565020

---

## 📋 Sobre o Projeto

O **BeaconSync** é uma plataforma corporativa em API RESTful desenvolvida em **.NET 10 (ASP.NET Core)** sob os princípios de **Clean Architecture**. O sistema centraliza o rastreamento de vulnerabilidades sociais, endereços, voluntários, responsáveis e respostas a alertas de emergência, garantindo alta performance, segurança robusta e integração com **Oracle Database**.

---

## 🛠️ Tecnologias e Arquitetura

* **Linguagem & Framework:** C# 12 / .NET 10 (ASP.NET Core Minimal APIs & Controllers)
* **Arquitetura:** Clean Architecture (`Domain`, `Application`, `Infrastructure`, `API`, `Tests`)
* **Banco de Dados:** Oracle Database (mapeamento explícito em maiúsculas via Entity Framework Core)
* **Segurança:** Autenticação e Autorização baseada em tokens **JWT (JSON Web Token)**
* **Resiliência & Confiabilidade:** 
  * **Idempotência:** Middleware personalizado via `IMemoryCache` para evitar duplicidade em operações de escrita (`POST`).
  * **Rate Limiting:** Proteção contra tráfego excessivo com janelas fixas e retorno customizado `429 Too Many Requests`.
* **Qualidade de Software:** Testes unitários (com **Moq**) e de integração (com **xUnit** e `WebApplicationFactory`).
* **Monitoramento:** Health Checks integrados (`/health/live`).

---

## 📂 Estrutura da Solução

```text
BeaconSync---Rastreador-de-Vulnerabilidades-.NET/
│
├── BeaconSync.API/                 # Camada de Apresentação (Controllers, Program.cs, Middlewares)
├── BeaconSync.Application/         # Casos de Uso, Serviços de Domínio e DTOs
├── BeaconSync.Domain/              # Entidades centrais do negócio
├── BeaconSync.Infrastructure/      # Repositórios, Mapeamentos EF Core e AppDbContext (Oracle)
├── BeaconSync.Tests.Unit/          # Suíte de Testes Unitários (xUnit & Moq)
└── BeaconSync.Tests.Integration/   # Suíte de Testes de Integração (WebApplicationFactory)