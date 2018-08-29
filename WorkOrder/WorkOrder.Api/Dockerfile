FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 56781
EXPOSE 44325

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Sass/Sass.csproj Sass/
RUN dotnet restore Sass/Sass.csproj
COPY . .
WORKDIR /src/Sass
RUN dotnet build Sass.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Sass.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sass.dll"]
