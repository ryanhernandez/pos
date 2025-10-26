FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY ./Server.csproj ./
RUN dotnet restore "Server.csproj"

# copy everything else and build
COPY . ./
RUN dotnet publish "Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create directory for persisted data
RUN mkdir -p /app/data

ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish ./

VOLUME ["/app/data"]

EXPOSE 80

ENTRYPOINT ["dotnet", "Server.dll"]
