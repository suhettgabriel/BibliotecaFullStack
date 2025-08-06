# Projeto Biblioteca Full Stack

Este é um projeto de aplicação Full Stack para gerenciamento de uma biblioteca, permitindo o cadastro e manutenção de Gêneros, Autores e Livros. O sistema foi desenvolvido como parte de um desafio técnico, utilizando as melhores práticas de mercado e tecnologias modernas.

## 🚀 Tecnologias Utilizadas

- **Backend:**
  - .NET 8
  - API REST com ASP.NET Core
  - Entity Framework Core 8
  - Padrão de Autenticação JWT (JSON Web Tokens)

- **Frontend:**
  - Angular 17+
  - TypeScript
  - Componentização e Reatividade com RxJS

- **Banco de Dados:**
  - SQL Server

- **DevOps:**
  - Docker & Docker Compose
  - GitHub Actions para Integração Contínua (CI)

## 🏛️ Arquitetura

O backend do projeto foi estruturado seguindo os princípios da **Clean Architecture**, visando a separação de responsabilidades, alta coesão, baixo acoplamento e testabilidade. A solução está dividida nos seguintes projetos:

- **`Biblioteca.Domain`**: Camada mais interna, contendo as entidades de negócio (`Book`, `Author`, `Genre`) e as interfaces dos repositórios. Não possui dependências de outras camadas.

- **`Biblioteca.Application`**: Contém a lógica de negócio, serviços, DTOs (Data Transfer Objects), validações e as interfaces dos serviços. Orquestra o fluxo de dados entre a API e a camada de infraestrutura.

- **`Biblioteca.Infrastructure`**: Implementa as interfaces definidas na camada de domínio, sendo responsável pelo acesso a dados (usando EF Core), migrations e comunicação com outras infraestruturas externas.

- **`Biblioteca.API`**: Camada de entrada da aplicação. Responsável por expor os endpoints REST, configurar middlewares (como Swagger e Autenticação), gerenciar a injeção de dependência e lidar com as requisições e respostas HTTP.

## 🏁 Como Executar o Projeto (Em Breve)

Esta seção será detalhada com as instruções para configurar e executar a aplicação localmente.

### Pré-requisitos

- .NET 8 SDK
- Node.js e Angular CLI
- SQL Server
- Docker Desktop

### Configuração

*(Instruções para configurar strings de conexão e outras variáveis de ambiente serão adicionadas aqui.)*

### Executando com Docker

```bash
# Comando para iniciar todos os serviços (backend, frontend, banco de dados)
docker-compose up -d