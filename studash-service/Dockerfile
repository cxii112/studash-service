FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["studash-service/studash-service.csproj", "studash-service/"]
RUN dotnet restore "studash-service/studash-service.csproj"
COPY . .
WORKDIR "/src/studash-service"
RUN dotnet build "studash-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "studash-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "studash-service.dll"]
