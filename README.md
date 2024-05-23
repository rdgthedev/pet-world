# Pet World 🐾

O Pet World é uma aplicação web, cujo sua finalidade é gerenciar as atividades de um Pet Shop, oferecer a posibilidade dos clientes realizeram compra de produtos para os seus pets e agendamento dos serviços que o Pet Shop oferece.    

## Funcionalidades 🛠️

O sistema tem as seguintes funcionalidades:

### Admin 👨‍💼

- **CRUD de Produtos:** Permite ao administrador criar, visualizar, atualizar e excluir produtos do Pet Shop.
- **CRUD de Serviços:** Permite ao administrador gerenciar os serviços oferecidos pelo Pet Shop.
- **CRUD de Agendamentos:** Permite ao administrador agendar e gerenciar os agendamentos dos clientes.
- **CRUD de Animais:** Permite ao administrador cadastrar, visualizar, atualizar e excluir informações sobre os animais dos clientes.
- **CRUD de Usuários:** Permite ao administrador gerenciar os usuários do sistema, incluindo clientes e outros administradores.

### Cliente 🐶
- **Se cadastrar:** Permite ao cliente se cadastrar no site do Pet Shop.
- **Compra de Produtos:** Permite ao cliente navegar pelo catálogo de produtos e realizar compras.
- **Reaizar Agendamentos:** Permite ao cliente realizar agendamentos dos serviços do Pet Shop para seus Pets.
- **Gerenciamento de Agendamentos:** Permite ao cliente visualizar e gerenciar seus agendamentos com o Pet Shop.
- **Cadastro de Pets:** Permite ao cliente cadastrar informações sobre seus animais de estimação.

## Arquitetura 🏛️

O sistema segue a arquitetura Clean Architecture, que promove a separação clara das responsabilidades em camadas:

- **Domain Layer (Camada de Domínio):** Define os modelos de domínio e as regras de negócio da aplicação.
- **Application Layer (Camada de Aplicação):** Contém a lógica de negócios da aplicação. 
- **Infrastructure Layer (Camada de Infraestrutura):** Responsável por lidar com detalhes técnicos, como acesso a banco de dados, autenticação e autorização, mapeamento de objetos, etc.
- **Presentation Layer (Camada de Apresentação):** Utiliza o padrão MVC para fornecer uma interface de usuário amigável. Aqui, os usuários interagem com a aplicação.

## Tecnologias Utilizadas 🛠️

- **ASP.NET Identity**: Para autenticação e autorização de usuários.
- **ASP.NET Core MVC**: Framework web para desenvolvimento de aplicações web.
- **Entity Framework Core**: ORM para mapeamento objeto-relacional.
- **AutoMapper**: Biblioteca para mapeamento de objetos.
- **SQL Server**: Banco de dados utilizado para armazenamento de dados.
- **Code First**: Abordagem de desenvolvimento orientada a banco de dados, onde o banco é gerado automaticamente com base no modelo de domínio.
