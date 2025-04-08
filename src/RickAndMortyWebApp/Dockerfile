FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy all files and restore as distinct layers. Use of the same COPY command
# for every dockerfile in the project to take advantage of docker caching
COPY src src

# restore package deps
RUN dotnet restore src/Brainbay.sln


WORKDIR /app/src/RickAndMortyWebApp
RUN dotnet publish -c Release -o /app/src/out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet","RickAndMortyWebApp.dll" ]