#FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
#FROM mcr.microsoft.com/dotnet/core/runtime:3.1.0 AS base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
#FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app

EXPOSE 9000


#FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

#FROM microsoft/aspnetcore-build:2.0 AS build

WORKDIR /src
COPY *.sln ./
COPY ViewApplication/ViewApplication.csproj ViewApplication/

RUN dotnet restore
#RUN dotnet restore "./ViewApplication.csproj"

COPY . .

#WORKDIR "/src/."
WORKDIR "/src/ViewApplication"

#RUN dotnet build "ViewApplication.csproj" -c Release -o /app/build
RUN dotnet build  -c Release -o /app/build



FROM build AS publish

#RUN dotnet publish "ProjectName.csproj" -c Release -o /app/publish
RUN dotnet publish -c Release -o /app/publish



FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ViewApplication.dll"]