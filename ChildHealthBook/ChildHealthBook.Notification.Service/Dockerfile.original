#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ChildHealthBook.Notification.Service/ChildHealthBook.Notification.Service.csproj", "ChildHealthBook.Notification.Service/"]
RUN dotnet restore "ChildHealthBook.Notification.Service/ChildHealthBook.Notification.Service.csproj"
COPY . .
WORKDIR "/src/ChildHealthBook.Notification.Service"
RUN dotnet build "ChildHealthBook.Notification.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChildHealthBook.Notification.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChildHealthBook.Notification.Service.dll"]
