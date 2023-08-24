#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IAMRefactor/IAMRefactor.csproj", "IAMRefactor/"]
COPY ["IAMRefactor.Application/IAMRefactor.Application.csproj", "IAMRefactor.Application/"]
COPY ["IAMRefactor.Common/IAMRefactor.Common.csproj", "IAMRefactor.Common/"]
COPY ["IAMRefactor.Core/IAMRefactor.Core.csproj", "IAMRefactor.Core/"]
COPY ["IAMRefactor.Infrastructure/IAMRefactor.Infrastructure.csproj", "IAMRefactor.Infrastructure/"]
RUN dotnet restore "IAMRefactor/IAMRefactor.csproj"
COPY . .
WORKDIR "/src/IAMRefactor"
RUN dotnet build "IAMRefactor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IAMRefactor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IAMRefactor.dll"]