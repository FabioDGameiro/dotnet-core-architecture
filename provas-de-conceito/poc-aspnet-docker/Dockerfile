# Build Stage
FROM microsoft/aspnetcore-build:2 as build-env
WORKDIR /api

COPY poc-aspnet-docker.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -o /publish

# Runtime Stage
FROM microsoft/aspnetcore-build:2
WORKDIR /publish
COPY --from=build-env /publish .

WORKDIR /publish
ENTRYPOINT [ "dotnet", "/publish/poc-aspnet-docker.dll" ]