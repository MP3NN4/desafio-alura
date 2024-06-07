# desafio-alura
Antes de executar o projeto, é necessário rodar o migrations com os comandos abaixo:

dotnet ef migrations add InitialCreate --project "Desafio.csproj"
dotnet ef database update --project "Desafio.csproj"