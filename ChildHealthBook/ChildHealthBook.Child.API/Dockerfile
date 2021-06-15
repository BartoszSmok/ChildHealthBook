#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ChildHealthBook.Child.API/ChildHealthBook.Child.API.csproj", "ChildHealthBook.Child.API/"]
RUN dotnet restore "ChildHealthBook.Child.API/ChildHealthBook.Child.API.csproj"
COPY . .
WORKDIR "/src/ChildHealthBook.Child.API"
RUN dotnet build "ChildHealthBook.Child.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChildHealthBook.Child.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChildHealthBook.Child.API.dll"]
