# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia o .csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante do código e publica a aplicação
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Copia os arquivos da build
COPY --from=build /app/publish .

# Expõe a porta que sua API usará
EXPOSE 7205

# Define a URL que o ASP.NET Core vai escutar
ENV ASPNETCORE_URLS=http://+:7205

# (Opcional) Cria um usuário sem privilégios e o utiliza
RUN adduser --disabled-password --gecos '' myuser
USER myuser

# Define o ponto de entrada para a aplicação
ENTRYPOINT ["dotnet", "MottuApi.dll"]