#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StateManagement.Api/StateManagement.Api.csproj", "StateManagement.Api/"]
COPY ["StateManagement.Application/StateManagement.Application.csproj", "StateManagement.Application/"]
COPY ["StateManagement.Contract/StateManagement.Contract.csproj", "StateManagement.Contract/"]
COPY ["StateManagement.SharedKernel/StateManagement.SharedKernel.csproj", "StateManagement.SharedKernel/"]
COPY ["StateManagement.Domain/StateManagement.Domain.csproj", "StateManagement.Domain/"]
COPY ["StateManagement.Infrastructure/StateManagement.Infrastructure.csproj", "StateManagement.Infrastructure/"]
RUN dotnet restore "StateManagement.Api/StateManagement.Api.csproj"
COPY . .
WORKDIR "/src/StateManagement.Api"
RUN dotnet build "StateManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StateManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StateManagement.Api.dll"]