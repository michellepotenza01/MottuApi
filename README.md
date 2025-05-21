                                        # Mottu API - Sistema de Gestão de Motos em Pátios

Integrantes do grupo: 
- **Ana Carolina de Castro Gonçalves** - RM 554669
- **Luisa Danielle** - RM 555292
- **Michelle Marques Potenza** - RM 557702
2TDSPW


    ## Descrição do Projeto

A **Mottu API** é uma aplicação RESTful desenvolvida utilizando **ASP.NET Core** para gerenciar motos, funcionários, clientes e pátios.
A API tem como objetivo centralizar e organizar o controle de motos nos pátios, incluindo a gestão das vagas e status das motos. 
A aplicação permite que os funcionários administrem os pátios, associando motos a eles, e permite que os clientes, opcionalmente, 
sejam associados a motos.

## Funcionalidades:
- **Cadastro de Pátios**: Pátios onde as motos são alocadas.
- **Cadastro de Funcionários**: Funcionários que trabalham em pátios.
- **Cadastro de Motos**: Motos que podem ser alocadas nos pátios.
- **Cadastro de Clientes**: Clientes que podem alugar motos (opcional).
- **CRUD completo**: Criar, ler, atualizar e excluir registros de pátios, funcionários, motos e clientes.
- **Validações e mensagens de erro**: Garantindo que os dados sejam consistentes e que as operações falhem com clareza quando necessário.
- **Documentação da API**: Utiliza o Swagger para documentação interativa da API.

## Recomendações de uso:
- Primeiramente, crie um pátio, que é o local onde as motos serão armazenadas. O nomePatio e a localização são obrigatórios, e também deve 
definir o número de vagas totais e vagas ocupadas.
- Depois de cadastrar o pátio, crie um funcionário, associando-o ao pátio onde ele trabalha. O nome, senha, e o nomePatio 
(associado a um pátio previamente cadastrado) são obrigatórios.
- Em seguida, cadastre uma moto, associando-a a um funcionário (que já deve estar cadastrado) e a um pátio (que também já deve estar cadastrado). 
Além disso, a placa, modelo, status, setor, nomePatio, e usuarioFuncionario são obrigatórios.
- O cadastro de clientes é opcional. Caso um cliente queira alugar uma moto, crie um cliente e associe a placa da moto (caso ele tenha alugado uma).
----------------------------------------------------------------------------------------------------------------------------------------------

## Rotas da API

### **1. Pátios**
- **GET** `/api/patios`
  - Retorna todos os pátios cadastrados.
  
- **GET** `/api/patios/{nomePatio}`
  - Retorna um pátio específico, com base no nome do pátio.
  
- **POST** `/api/patios`
  - Cria um novo pátio. 
  - **Exemplo JSON:**
    ```json
    {
      "nomePatio": "Patio Curitiba",
      "localizacao": "Centro Curitiba 45",
      "vagasTotais": 50,
      "vagasOcupadas": 30
    }
    ```
  
- **PUT** `/api/patios/{nomePatio}`
  - Atualiza um pátio específico, com base no nome do pátio.
  
- **DELETE** `/api/patios/{nomePatio}`
  - Exclui um pátio específico.

### **2. Funcionários**
- **GET** `/api/funcionarios`
  - Retorna todos os funcionários cadastrados.
  
- **GET** `/api/funcionarios/{usuarioFuncionario}`
  - Retorna um funcionário específico, com base no nome de usuário do funcionário.
  
- **POST** `/api/funcionarios`
  - Cria um novo funcionário.
  - O **nomePatio** deve ser válido, ou seja, o pátio já deve estar cadastrado.
  - **Exemplo JSON:**
    ```json
    {
      "usuarioFuncionario": "FeSilva",
      "nome": "Felipe Silva",
      "senha": "F12k3",
      "nomePatio": "Patio Osasco"
    }
    ```
  
- **PUT** `/api/funcionarios/{usuarioFuncionario}`
  - Atualiza um funcionário específico, com base no nome de usuário do funcionário.
  
- **DELETE** `/api/funcionarios/{usuarioFuncionario}`
  - Exclui um funcionário específico.

### **3. Motos**
- **GET** `/api/motos`
  - Retorna todas as motos cadastradas.
  
- **GET** `/api/motos/{placa}`
  - Retorna uma moto específica, com base na placa.
  
- **POST** `/api/motos`
  - Cria uma nova moto.
  - O **modelo**, **status**, **setor**, **nomePatio** e **usuarioFuncionario** devem ser válidos.
  - **Exemplo JSON:**
    ```json
    {
      "placa": "JEM9081",
      "modelo": "MottuSport",
      "status": "Disponível",
      "setor": "Bom",
      "nomePatio": "Patio Osasco",
      "usuarioFuncionario": "MichellePtz"
    }
    ```

- **PUT** `/api/motos/{placa}`
  - Atualiza uma moto específica, com base na placa.
  
- **DELETE** `/api/motos/{placa}`
  - Exclui uma moto específica.

### **4. Clientes** (Opcional)
- **GET** `/api/clientes`
  - Retorna todos os clientes cadastrados.
  
- **GET** `/api/clientes/{usuarioCliente}`
  - Retorna um cliente específico, com base no nome de usuário do cliente.
  
- **POST** `/api/clientes`
  - Cria um novo cliente.
  - **Exemplo JSON:**
    ```json
    {
      "usuarioCliente": "GuiSouza",
      "nome": "Guilherme Oliveira Souza",
      "senha": "senha123",
      "motoPlaca": ""
    }
    ```
  - (a placa da moto é opcional, pois o cliente pode não ter uma moto associada no momento do cadastro)

- **PUT** `/api/clientes/{usuarioCliente}`
  - Atualiza um cliente específico, com base no nome de usuário do cliente.
  
- **DELETE** `/api/clientes/{usuarioCliente}`
  - Exclui um cliente específico.

---

## Instalação e Dependências

### **Pré-requisitos**
Certifique-se de ter os seguintes programas instalados em sua máquina:
- **.NET 8.0 ou superior** (para rodar a aplicação).
- **Oracle Database** (para o banco de dados).
- **Visual Studio** ou **Visual Studio Code** (para edição do código).

### **Passos para rodar o projeto**

1. ** repositório**
   - Clone o repositório para sua máquina local:
     ```bash
     git clone https://github.com/michellepotenza01/MottuApi.git
     ```

2. **Configuração do banco de dados**
   - Aplique as credenciais de conexão no arquivo `appsettings.json`:
     ```json
     {
       "ConnectionStrings": {
         "OracleConnection": "User Id=RM557702;Password=040106;Data Source=oracle.fiap.com.br:1521/orcl;"
       }
     }
     ```

3. **Instalar dependências**
   - No terminal, dentro da pasta do projeto, instale as dependências do EF Core e Oracle:
     ```bash
     dotnet add package Microsoft.EntityFrameworkCore.Tools
     dotnet add package Oracle.EntityFrameworkCore
     ```

4. **Aplicar as migrações**
   - Caso necessario para garantir que o banco de dados esteja atualizado, execute as migrações:
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

5. **Rodar a aplicação**
   - Execute o projeto:
     ```bash
     dotnet run
     ```

6. **Acessar o Swagger**
   - Abra o Swagger na URL: `https://localhost:7205/index.html`

---


