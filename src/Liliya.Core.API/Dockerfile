#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#SDK镜像版本
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base 
WORKDIR /app
EXPOSE 80
EXPOSE 443


ENV CATALINA_HOME /usr/local/tomcat


#设置时区
RUN /bin/cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone

COPY . .

ENTRYPOINT ["dotnet", "Liliya.Core.API.dll"]