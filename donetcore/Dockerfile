FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["donetcore/donetcore.csproj", "donetcore/"]
COPY ["CAP/src/DotNetCore.CAP.SqlServer/DotNetCore.CAP.SqlServer.csproj", "CAP/src/DotNetCore.CAP.SqlServer/"]
COPY ["CAP/src/DotNetCore.CAP/DotNetCore.CAP.csproj", "CAP/src/DotNetCore.CAP/"]
RUN dotnet restore "donetcore/donetcore.csproj"
COPY . .
WORKDIR "/src/donetcore"
RUN dotnet build "donetcore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "donetcore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "donetcore.dll"]