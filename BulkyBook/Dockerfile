#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base #base class libaries
WORKDIR /app
EXPOSE 80
EXPOSE 443
  
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build #Framework class libaries
WORKDIR /src
COPY ["BulkyBook/BulkyBook.csproj", "BulkyBook/"]
COPY ["BulkyBook.Utilities/BulkyBook.Utilities.csproj", "BulkyBook.Utilities/"]
COPY ["BullyBook.DataAccess/BullyBook.DataAccess.csproj", "BullyBook.DataAccess/"]
COPY ["BullyBook.Models/BullyBook.Models.csproj", "BullyBook.Models/"]
RUN dotnet restore "BulkyBook/BulkyBook.csproj"
COPY . .
WORKDIR "/src/BulkyBook"
RUN dotnet build "BulkyBook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BulkyBook.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BulkyBook.dll"]