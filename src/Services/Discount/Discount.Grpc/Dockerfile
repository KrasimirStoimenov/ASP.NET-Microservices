#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/Discount.Grpc/Discount.Grpc.csproj", "Services/Discount/Discount.Grpc/"]
COPY ["Services/Discount/Discount.Service/Discount.Service.csproj", "Services/Discount/Discount.Service/"]
COPY ["Services/Discount/Discount.Repository/Discount.Repositories.csproj", "Services/Discount/Discount.Repository/"]
COPY ["Services/Discount/Discount.Data/Discount.Data.csproj", "Services/Discount/Discount.Data/"]
COPY ["Services/Discount/Discount.Extensions/Discount.Extensions.csproj", "Services/Discount/Discount.Extensions/"]
RUN dotnet restore "Services/Discount/Discount.Grpc/Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.Grpc"
RUN dotnet build "Discount.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]
