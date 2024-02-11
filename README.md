API feita para cadastro de produtos para serem vendidos.

Nesta API é possível cadastrar um usuário, o seu produto e a categoria desse produto. 

A pasta Model contém todos os requisitos para o cadastramento da classe, porém, o retorno dessas informações estão na pasta ViewModel, que retorna apenas informações necessárias após o cadastro, omitindo, por exemplo, **a senha do usuário**. 

A pasta **Repository** é a pasta responsável por fazer conexão com o banco de dados e armazenar as informações, o banco de dados utilizado nesse projeto foi o **SQL Server 2019**.

A classe **ConfigureSwaggerOptions** é responsável pelas configurações do Swagger para abrir as versões diferentes da API.

Na classe **appsettings.json** está configurado a rota do banco de dados para que ele possa ser trabalhado juntamente com o **.NET**. 

Por fim, na classe **Program** estão todas as configurações da API para que ela possa funcionar de maneira correta. 

A linguagem de programção usada para a criação dessa API foi o **C#**.
