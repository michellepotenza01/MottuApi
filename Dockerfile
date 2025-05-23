# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia csproj e restaura pacotes
COPY *.csproj ./
RUN dotnet restore

# Copia o restante e publica a aplicação
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia arquivos publicados da etapa build
COPY --from=build /app/publish .

# Exponha a porta usada pela sua API
EXPOSE 7205

# Variável de ambiente para o ASP.NET Core ouvir na porta correta
ENV ASPNETCORE_URLS=http://+:7205

# Cria um usuário não privilegiado e usa ele para rodar a aplicação
RUN adduser --disabled-password --gecos '' myuser
USER myuser

# Define o ponto de entrada (mude para o nome correto do seu dll)
ENTRYPOINT ["dotnet", "MottuApi.dll"]
