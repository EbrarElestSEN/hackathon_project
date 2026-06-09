# 1. Aþama: Uygulamayý Derleme
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Proje dosyalarýný kopyala ve restore et
COPY . ./
RUN dotnet restore

# Yayýnlama aþamasý
RUN dotnet publish -c Release -o out

# 2. Aþama: Çalýþma Zamaný (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Render'ýn dinamik port atamasý için gerekli ayar
ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "hackathon_project.dll"]