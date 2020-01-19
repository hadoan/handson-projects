FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["CqrsTodo.csproj", ""]
RUN dotnet restore "CqrsTodo.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "CqrsTodo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CqrsTodo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CqrsTodo.dll"]