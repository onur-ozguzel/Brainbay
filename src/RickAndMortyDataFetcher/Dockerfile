FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80

# copy all .csporj files and restore as distinct layers. Use of the same COPY command
# for every dockerfile in the project to take advantage of docker caching
COPY Brainbay.sln Brainbay.sln
COPY src/RickAndMortyDataFetcher/RickAndMortyDataFetcher.csproj src/RickAndMortyDataFetcher/RickAndMortyDataFetcher.csproj

# restore package deps
RUN dotnet restore Brainbay.sln

# Copy the app folders over
COPY src/RickAndMortyDataFetcher src/RickAndMortyDataFetcher

WORKDIR /app/src/RickAndMortyDataFetcher
RUN dotnet publish -c Release -o /app/src/out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet","RickAndMortyDataFetcher.dll" ]