# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BarberShopApi.csproj", "./"]
RUN dotnet restore "BarberShopApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BarberShopApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BarberShopApi.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BarberShopApi.dll"]