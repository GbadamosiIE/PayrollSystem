#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PayrollSystem/PayRollSystem.Api.csproj", "PayrollSystem/"]
COPY ["PayRollSystem.Data/PayRollSystem.Data.csproj", "PayRollSystem.Data/"]
COPY ["PayRollSystem.Application/PayRollSystem.Application.csproj", "PayRollSystem.Application/"]
COPY ["PayRollSystem.Domain/PayRollSystem.Domain.csproj", "PayRollSystem.Domain/"]
COPY ["PayRollSystem.Service/PayRollSystem.Service.csproj", "PayRollSystem.Service/"]
RUN dotnet restore "PayrollSystem/PayRollSystem.Api.csproj"
COPY . .
WORKDIR "/src/PayrollSystem"
RUN dotnet build "PayRollSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PayRollSystem.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PayRollSystem.Api.dll"]