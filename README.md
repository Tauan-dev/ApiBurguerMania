ApiBurguerMania
A ApiBurguerMania é uma API RESTful desenvolvida para gerenciar pedidos de hambúrgueres, incluindo a criação de produtos, realização de pedidos e integração com banco de dados MySQL. A API foi construída utilizando ASP.NET Core com Entity Framework Core e a base de dados é gerenciada através do Pomelo.EntityFrameworkCore.MySql.

Sumário
Descrição
Tecnologias Utilizadas
Requisitos
Instalação
Configuração do Banco de Dados
Uso
Endpoints da API
Licença
Descrição
O projeto ApiBurguerMania é uma aplicação backend que permite gerenciar hambúrgueres e pedidos em uma loja de fast food. A API possibilita a criação, atualização, remoção e consulta de produtos, além de realizar e listar pedidos de clientes.

A API foi construída com base nas melhores práticas de desenvolvimento, garantindo a escalabilidade, segurança e manutenção simplificada.

Tecnologias Utilizadas
ASP.NET Core 8.0
Entity Framework Core 8.0 (com Pomelo.EntityFrameworkCore.MySql para conexão com MySQL)
Swashbuckle.AspNetCore (para documentação da API via Swagger)
Newtonsoft.Json (para serialização JSON)
Docker (opcional para containerização do ambiente de desenvolvimento)
Requisitos
Antes de rodar o projeto, é necessário ter instalado:

.NET 8.0 SDK (ou versão superior)
MySQL (ou MariaDB) instalado e rodando, com as permissões de acesso configuradas
Visual Studio (ou qualquer editor de código de sua preferência, como VSCode)
(Opcional) Docker para rodar o banco de dados em container
Instalação
Clone o repositório:

bash
Copiar código
git clone https://github.com/SeuUsuario/ApiBurguerMania.git
cd ApiBurguerMania
Restaurar as dependências do projeto:

bash
Copiar código
dotnet restore
Aplicar as migrações no banco de dados:

Se ainda não tiver feito as migrações, execute o seguinte comando para criar a base de dados:

bash
Copiar código
dotnet ef migrations add InitialCreate
Para aplicar as migrações ao banco de dados:

bash
Copiar código
dotnet ef database update
Compilar e rodar a aplicação:

bash
Copiar código
dotnet build
dotnet run
A API estará disponível em http://localhost:5000.

Configuração do Banco de Dados
O banco de dados é configurado no arquivo appsettings.json. Um exemplo de configuração para o MySQL pode ser:

json
Copiar código
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ApiBurguerManiaDB;User=root;Password=senha"
  }
}
A string de conexão pode ser ajustada conforme o seu ambiente e as credenciais do banco de dados.

Uso
Após rodar o projeto, a API estará disponível em http://localhost:5000. Você pode interagir com ela utilizando ferramentas como Postman ou Insomnia.

A documentação da API será automaticamente gerada pelo Swagger e pode ser acessada através do endereço:

http://localhost:5000/swagger

Endpoints da API
Produtos
GET /api/products
Retorna todos os produtos cadastrados.

POST /api/products
Cria um novo produto. Exemplo de corpo da requisição:

json
Copiar código
{
  "name": "Cheeseburger",
  "description": "Hambúrguer com queijo",
  "price": 15.50
}
GET /api/products/{id}
Retorna um produto específico pelo ID.

PUT /api/products/{id}
Atualiza um produto existente.

DELETE /api/products/{id}
Remove um produto do sistema.

Pedidos
GET /api/orders
Retorna todos os pedidos realizados.

POST /api/orders
Cria um novo pedido. Exemplo de corpo da requisição:

json
Copiar código
{
  "customerName": "João Silva",
  "products": [
    { "productId": 1, "quantity": 2 },
    { "productId": 3, "quantity": 1 }
  ]
}
GET /api/orders/{id}
Retorna detalhes de um pedido específico.

DELETE /api/orders/{id}
Cancela um pedido.

Licença
Distribuído sob a licença MIT. Veja o arquivo LICENSE para mais informações.

Esse modelo segue um padrão bem simples e organizado, proporcionando informações essenciais como tecnologias, requisitos, instruções de instalação, uso da API e exemplos de endpoints.

Você pode ajustar conforme a necessidade do seu projeto, incluindo detalhes sobre autenticação, validação, testes, entre outros pontos importantes.

Se precisar de algo mais, é só avisar!
