#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ChildHealthBook.Gateway.API/ChildHealthBook.Gateway.API.csproj", "ChildHealthBook.Gateway.API/"]
RUN dotnet restore "ChildHealthBook.Gateway.API/ChildHealthBook.Gateway.API.csproj"
COPY . .
WORKDIR "/src/ChildHealthBook.Gateway.API"
RUN dotnet build "ChildHealthBook.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChildHealthBook.Gateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChildHealthBook.Gateway.API.dll"]
