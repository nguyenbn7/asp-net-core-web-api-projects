FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5140

ENV ASPNETCORE_URLS=http://+:5140

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Ecommerce.csproj", "./"]
RUN dotnet restore "Ecommerce.csproj"
COPY . .
# https://docs.render.com/docker-secrets
RUN --mount=type=secret,id=appsettings_json,dst=/etc/secrets/appsettings.json cp /etc/secrets/appsettings.json .
RUN chown -R app ./*
RUN chmod 700 -R ./*
WORKDIR "/src/."
RUN dotnet build "Ecommerce.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Ecommerce.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.dll"]
