#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Dotnet.Onion.Template.API/Dotnet.Onion.Template.API.csproj", "Dotnet.Onion.Template.API/"]
RUN dotnet restore "Dotnet.Onion.Template.API/Dotnet.Onion.Template.API.csproj"
COPY . .
WORKDIR "/src/Dotnet.Onion.Template.API"
RUN dotnet build "Dotnet.Onion.Template.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet.Onion.Template.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.Onion.Template.API.dll"]