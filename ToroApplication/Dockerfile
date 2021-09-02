#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ToroApplicationTests/ToroApplicationTests.csproj", "ToroApplicationTests/"]
COPY ["ToroApplication/ToroApplication.csproj", "ToroApplication/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Domain.Model/Domain.Model.csproj", "Domain.Model/"]
COPY ["Domain.Service/Domain.Service.csproj", "Domain.Service/"]
COPY ["Security/Security.csproj", "Security/"]
RUN dotnet restore "ToroApplication/ToroApplication.csproj"
COPY . .
WORKDIR "/src/ToroApplication"
RUN dotnet build "ToroApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToroApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToroApplication.dll"]