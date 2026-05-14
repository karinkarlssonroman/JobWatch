FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY JobWatch.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ARG BUILD_SHA=local
ENV BUILD_SHA=$BUILD_SHA
EXPOSE 8080
ENTRYPOINT ["dotnet", "JobWatch.dll"]
