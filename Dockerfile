<<<<<<< HEAD
﻿# Use a imagem oficial do SDK para build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
=======
# Etapa de construção com a imagem SDK do .NET
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
>>>>>>> 62f9b743ffb9732fdc1a0ab84ee219aab1dbd018

WORKDIR /src

# Copia o .csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia o restante do código e publica a aplicação
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

# Define a porta exposta
EXPOSE 7205

# Configura o ASP.NET para escutar na porta 7205
ENV ASPNETCORE_URLS=http://+:7205

# Permite rodar com um usuário não root (opcional)
RUN adduser --disabled-password --gecos '' myuser
USER myuser

# Comando de entrada
ENTRYPOINT ["dotnet", "MottuApi.dll"]