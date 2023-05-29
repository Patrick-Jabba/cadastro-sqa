FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /build

COPY back-cadastroSqa .

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=falase

RUN cd cadastroSqa && \
    dotnet restore && \
    dotnet build -c Release --no-restore && \
    dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

ENV CONNECTION_STRINGS="user ID=postgres;Password=minhasenhasecreta;Host=db;Port=5432;Database=cadastrosqadb;"

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT [ "dotnet", "cadastroSqa.dll" ]
