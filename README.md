# 🌐 BeaconSync — Rastreador de Vulnerabilidades & Gestão de Apoio Social

> **Global Solution 2026/2 · CitizenTech · FIAP**  
> **API Online (Render):** [Acessar Swagger UI](https://beaconsync-rastreador-de.onrender.com/index.html)

---

## 🌿 Estratégia de Branches do Repositório

Para garantir a organização e o sucesso da entrega da Global Solution, este repositório utiliza uma divisão clara de ambientes:

- **`main` (Branch Principal):** Destinada à integração oficial do ecossistema multiplataforma. Nela, o fluxo de autenticação e emissão de tokens JWT é centralizado na **API em Java**, servindo diretamente para o consumo do **front-end / aplicativo mobile** gerenciado pela equipe[cite: 3].
- **`jwt-dotnet` (Branch de Desenvolvimento e Validação):** Considerada a branch principal de engenharia e testes avançados do .NET. Nela, validamos os fluxos de segurança, contratos e comportamentos desacoplados para assegurar a autonomia e a robustez do microsserviço de voluntariado.

---

## 👥 Integrantes da Equipe

- **Pamella Christiny Chaves Brito** | RM565206
- **Felipe Ribeiro Salles de Camargo** | RM565224
- **João Pedro Pereira Camilo** | RM562005
- **Lucas Matsubara Reis** | RM565020

---

## 💡 O que é o BeaconSync e o que ele faz?

O **BeaconSync** é uma plataforma corporativa desenvolvida em **API RESTful com .NET 10 (ASP.NET Core)** sob os rigorosos princípios de **Clean Architecture**. O principal objetivo do sistema é centralizar, gerenciar e otimizar o ecossistema de apoio social e rastreamento de vulnerabilidades.

### 🎯 Principais Funcionalidades e Domínio de Negócio:
- **Cadastro e Gestão de Pessoas Vulneráveis:** Permite registrar e monitorar cidadãos em situação de vulnerabilidade social, mapeando suas necessidades específicas e perfis de risco.
- **Mapeamento de Endereços:** Associa localizações geográficas precisas aos cadastros para otimizar o direcionamento de equipes e recursos de ajuda humanitária.
- **Gestão de Voluntários e Responsáveis:** Cadastra e gerencia os dados de voluntários e responsáveis técnicos/sociais atuantes nas frentes de apoio.
- **Respostas a Alertas de Emergência:** Sistema dinâmico para emissão, rastreio e registro de respostas a chamados e alertas gerados na comunidade.

---

## 🛠️ Arquitetura e Engenharia de Software

A solução resolve desafios críticos de engenharia através de:
- **Arquitetura Modular:** Separação estrita de responsabilidades entre `Domain`, `Application`, `Infrastructure`, `API` e camadas de teste.
- **Segurança e Resiliência:** Mecanismos robustos de autenticação baseada em **JWT** (integrados ao ecossistema Java na branch `main`), proteção contra ataques de força bruta e uso excessivo através de **Rate Limiting** (retornando `429 Too Many Requests`), além de **Idempotência** via cache em memória para prevenir operações duplicadas em requisições de escrita (`POST`).
- **Persistência Corporativa:** Integração nativa com **Oracle Database** utilizando mapeamentos em maiúsculas via Entity Framework Core para total compatibilidade com os padrões de banco de dados corporativos da FIAP.
- **Qualidade Assegurada:** Suíte robusta de **testes unitários** (com Moq) e **testes de integração** (com xUnit e WebApplicationFactory) cobrindo o comportamento dos serviços e endpoints.

---

## 🔒 Segurança e Credenciais (Modelo de Exemplo)

Por questões de segurança e boas práticas de desenvolvimento, **todas as credenciais sensíveis (como strings de conexão do Oracle Database e chaves secretas do JWT)** foram removidas do código-fonte e do arquivo de configuração versionado (`appsettings.json`). 

Este repositório serve como um **template/exemplo de arquitetura** e para executá-lo localmente, é necessário configurar as suas próprias credenciais seguindo os passos da seção abaixo.

---

## ⚙️ Como Configurar e Executar o Projeto

### 1. Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado na sua máquina.
- Acesso a uma instância ativa do **Oracle Database** (ou Oracle XE).

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
    "Key": "SUA_CHAVE_SECRETA_JWT_SUPER_SEGURA_COM_NO_MINIMO_32_CARACTERES",
    "Issuer": "BeaconSync",
    "Audience": "BeaconSyncApp"
  }
}

```

*Substitua `SEU_USUARIO`, `SUA_SENHA`, o host do banco de dados e a chave secreta do JWT pelos valores reais do seu ambiente Oracle e da sua política de segurança.*

### 3. Aplicando as Migrações no Banco de Dados

Com as credenciais configuradas, abra o terminal na raiz do projeto e execute o comando do Entity Framework para gerar as tabelas no seu Oracle:

```bash
dotnet ef database update --project BeaconSync.Infrastructure/BeaconSync.Infrastructure.csproj --startup-project BeaconSync.API/BeaconSync.API.csproj

```

### 4. Executando a API

Inicie a aplicação executando:

```bash
dotnet run --project BeaconSync.API/BeaconSync.API.csproj

```

Acesse a documentação interativa do **Swagger** diretamente no navegador através de: `http://localhost:5006/`.

---

## 🧪 Executando os Testes Automatizados

O projeto conta com validações automatizadas cobrindo regras de negócio e contratos de endpoints. Para rodar toda a suíte de testes unitários e de integração:

```bash
dotnet test

```

---

## 📋 Exemplos de Payloads para Teste (JSON)

### 1. Cadastrar uma Nova Pessoa Vulnerável (Primeiro passo)

* **Rota:** `POST /api/v1/pessoas-vulneraveis`

```json
{
  "nome": "Ana Beatriz Silva",
  "idade": 45,
  "tipoVulnerabilidade": "Mãe solo com lactente em área de encosta",
  "necessidadeEspecial": "Nenhuma",
  "enderecoId": 0
}

```

*(Após executar este, anote o id que a API retornar, por exemplo, ID 2).*

### 2. Cadastrar um Novo Voluntário (Independente)

* **Rota:** `POST /api/v1/voluntarios`

```json
{
  "nome": "Lucas Gabriel",
  "email": "lucas.gabriel@beaconsync.com",
  "telefone": "11988889999",
  "especialidade": "Logística e Distribuição de Alimentos",
  "disponivelParaAjuda": true
}

```

### 3. Cadastrar o Endereço (Vinculado à pessoa criada)

* **Rota:** `POST /api/v1/enderecos` *(Substitua o `pessoaVulneravelId` pelo ID real que você obteve no passo 1)*

```json
{
  "id": 0,
  "pessoaVulneravelId": 2,
  "logradouro": "Rua das Comunidades",
  "numero": "120",
  "bairro": "Heliópolis",
  "distrito": "Sacomã",
  "tipoImovel": "Casa",
  "temLajeParaHelicoptero": false,
  "andar": 0,
  "eEnderecoAtual": true
}

```

### 4. Cadastrar uma Resposta a Alerta (Vinculando Voluntário e Pessoa)

* **Rota:** `POST /api/v1/respostas-alerta` *(Certifique-se de usar IDs de voluntários e pessoas que você sabe que já existem no banco)*

```json
{
  "descricao": "Visita de monitoramento e entrega de kits de higiene realizada com sucesso.",
  "status": "Concluído",
  "voluntarioId": 1,
  "pessoaVulneravelId": 2
}

```

---

© 2026 BeaconSync · Todos os direitos reservados.

```

```
