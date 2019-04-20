FROM mcr.microsoft.com/dotnet/core/runtime:2.2
COPY /bin/publish . 
WORKDIR /
EXPOSE 8099

ENTRYPOINT ["dotnet", "donetcore.dll"]