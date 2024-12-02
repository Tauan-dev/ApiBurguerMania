# ApiBurguerMania

A **ApiBurguerMania** é uma API RESTful desenvolvida para gerenciar pedidos de hambúrgueres, incluindo a criação de produtos, realização de pedidos e integração com banco de dados MySQL. A API foi construída utilizando **ASP.NET Core** com **Entity Framework Core** e a base de dados é gerenciada através do **Pomelo.EntityFrameworkCore.MySql**.

## Sumário

- [Descrição](#descrição)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Requisitos](#requisitos)
- [Instalação](#instalação)
- [Configuração do Banco de Dados](#configuração-do-banco-de-dados)
- [Uso](#uso)
- [Endpoints da API](#endpoints-da-api)
- [Licença](#licença)

## Descrição

O projeto **ApiBurguerMania** é uma aplicação backend que permite gerenciar hambúrgueres e pedidos em uma loja de fast food. A API possibilita a criação, atualização, remoção e consulta de produtos, além de realizar e listar pedidos de clientes.

A API foi construída com base nas melhores práticas de desenvolvimento, garantindo a escalabilidade, segurança e manutenção simplificada.

## Tecnologias Utilizadas

- **ASP.NET Core 8.0**
- **Entity Framework Core 8.0** (com **Pomelo.EntityFrameworkCore.MySql** para conexão com MySQL)
- **Swashbuckle.AspNetCore** (para documentação da API via Swagger)
- **Newtonsoft.Json** (para serialização JSON)
- **Docker** (opcional para containerização do ambiente de desenvolvimento)

## Requisitos

Antes de rodar o projeto, é necessário ter instalado:

- **.NET 8.0 SDK** (ou versão superior)
- **MySQL** (ou MariaDB) instalado e rodando, com as permissões de acesso configuradas
- **Visual Studio** (ou qualquer editor de código de sua preferência, como VSCode)
- (Opcional) **Docker** para rodar o banco de dados em container

## Instalação

1. **Clone o repositório:**

```bash
git clone https://github.com/Tauan-dev/ApiBurguerMania.git
cd ApiBurguerMania
```

2.**Restaurar as dependências do projeto:**

```bash
dotnet restore
```

3.**Aplicar as migrações no banco de dados:**

Se ainda não tiver feito as migrações, execute o seguinte comando para criar a base de dados:

```bash
dotnet ef migrations add InitialCreate
```

Para aplicar as migrações ao banco de dados:

```bash
dotnet ef database update
```

4.**Compilar e rodar a aplicação:**

```bash
dotnet build
dotnet run
```

A API estará disponível em http://localhost:5299/swagger/index.html


## Configuração do Banco de Dados

O banco de dados é configurado no arquivo `appsettings.json`. Abaixo está um exemplo de configuração para o MySQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ApiBurguerManiaDB;User=root;Password=senha"
  }
}
``` 



## Licença
Este projeto está distribuído sob a licença MIT. Veja o arquivo LICENSE para mais informações.


