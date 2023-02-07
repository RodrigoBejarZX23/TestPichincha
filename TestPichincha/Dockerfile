FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS build
WORKDIR apiapp

EXPOSE 80
EXPOSE 5024

#COPY PROJECT FILE
COPY ./*.csproj ./
RUN dotnet restore

#COPY EVERYTHING ELSE
COPY . .
RUN dotnet publish -c Release -o out

#BUILD IMAGE
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /apiapp
COPY --from=build /TestPichincha/out .
ENTRYPOINT ["dotnet","TestPichincha.dll"]