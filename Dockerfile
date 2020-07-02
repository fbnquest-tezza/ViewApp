FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# COPY *.csproj ./


COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore

COPY . ./

RUN dotnet test --verbosity=normal --results-directory /UnitTests/ --logger "trx;LogFileName=test_results.xml" ./UnitTests/UnitTests.csproj

RUN dotnet publish ./ViewApplication/ViewApplication.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .
# COPY --from=build-env /UnitTests /UnitTests
ENTRYPOINT ["dotnet", "ViewApplication.dll"]