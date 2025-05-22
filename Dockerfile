# Etapa de construção com a imagem SDK do .NET
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar os arquivos do projeto para o container
COPY . ./

# Restaurar as dependências da aplicação
RUN dotnet restore "MottuApi.net.csproj"

# Compilar o projeto
RUN dotnet build "MottuApi.net.csproj" -c Release -o /app/build

# Publicar a aplicação
RUN dotnet publish "MottuApi.net.csproj" -c Release -o /app/publish

# Etapa final: usar a imagem ASP.NET Core para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copiar os arquivos compilados da etapa anterior
COPY --from=build /app/publish .

# Expor a porta 80 para acesso externo
EXPOSE 80

# Comando de execução da aplicação
ENTRYPOINT ["dotnet", "MottuApi.net.dll"]
