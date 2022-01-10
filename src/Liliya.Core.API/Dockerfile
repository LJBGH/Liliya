#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#SDK镜像版本
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base 
WORKDIR /app
EXPOSE 80
EXPOSE 443

# 设置ssl证书
ENV ASPNETCORE_URLS https://*:443;http://*:80
ENV ASPNETCORE_Kestrel__Certificates__Default__Path /app/liliya.work.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password "134116"

ENV CATALINA_HOME /usr/local/tomcat

#设置时区
RUN /bin/cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone

COPY . .

ENTRYPOINT ["dotnet", "Liliya.Core.API.dll"]