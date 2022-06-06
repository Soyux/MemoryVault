#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MemoryVault/MemoryVault.csproj", "MemoryVault/"]
RUN dotnet restore "MemoryVault/MemoryVault.csproj"
COPY . .
WORKDIR "/src/MemoryVault"
RUN dotnet build "MemoryVault.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MemoryVault.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MemoryVault.dll"]