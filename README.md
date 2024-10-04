# Market API

Bem-vindo à **Market API**, uma abstração que permite gerenciar produtos, categorias e suas interações de forma simples e eficaz. Esta API é projetada para desenvolvedores que desejam integrar funcionalidades de gerenciamento de supermercado em suas aplicações.

## 🛒 O que é a Market API?

A Market API fornece um conjunto de endpoints RESTful para manipulação de produtos e categorias de um mercado. Com ela, você pode facilmente:

- **Gerenciar Produtos**: Crie, leia, atualize e exclua produtos disponíveis no supermercado.
- **Gerenciar Categorias**: Organize seus produtos em categorias, facilitando a navegação e o gerenciamento.
- **Gerar Pedidos**: Monte e gerencie pedidos de clientes.
- **Gerenciar Clientes**: Adicione e gerencie informações sobre clientes.

## 🚀 Funcionalidades

- **CRUD Completo**:
  - **Produtos**: Crie, leia, atualize e exclua informações de produtos, incluindo nome, preço e categoria associada.
  - **Categorias**: Crie, leia, atualize e exclua categorias para melhor organizar seus produtos.
  - **Pedidos**: Crie e gerencie pedidos, incluindo os produtos associados e informações do cliente.
  - **Clientes**: Registre e gerencie informações sobre os clientes, como nome e informações de contato.
  - **Pagamentos**: Implemente um sistema de pagamento simples para processar as transações dos pedidos.
  - 
- **DTOs**: Utiliza Data Transfer Objects (DTOs) para garantir que apenas os dados necessários sejam enviados e recebidos, melhorando a eficiência da API.

## 📦 Estrutura da API

### Endpoints Principais

#### Produtos
- `GET /api/products` - Retorna uma lista de todos os produtos.
- `GET /api/products/{id}` - Retorna os detalhes de um produto específico.
- `POST /api/products` - Cria um novo produto.
- `PUT /api/products/{id}` - Atualiza um produto existente.
- `DELETE /api/products/{id}` - Remove um produto.

#### Categorias
- `GET /api/categories` - Retorna uma lista de todas as categorias.
- `GET /api/categories/{id}` - Retorna os detalhes de uma categoria específica.
- `POST /api/categories` - Cria uma nova categoria.
- `PUT /api/categories/{id}` - Atualiza uma categoria existente.
- `DELETE /api/categories/{id}` - Remove uma categoria.

#### Pedidos
- `GET /api/orders` - Retorna uma lista de todos os pedidos.
- `GET /api/orders/{id}` - Retorna os detalhes de um pedido específico.
- `POST /api/orders` - Cria um novo pedido.
- `PUT /api/orders/{id}` - Atualiza um pedido existente.
- `DELETE /api/orders/{id}` - Remove um pedido.

#### Clientes
- `GET /api/customers` - Retorna uma lista de todos os clientes.
- `GET /api/customers/{id}` - Retorna os detalhes de um cliente específico.
- `POST /api/customers` - Cria um novo cliente.
- `PUT /api/customers/{id}` - Atualiza um cliente existente.
- `DELETE /api/customers/{id}` - Remove um cliente.

