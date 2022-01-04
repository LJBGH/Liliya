#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#SDK镜像版本
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base 
WORKDIR /app
EXPOSE 80
EXPOSE 443

#ENV ASPNETCORE_URLS https://*:443;http://*:80
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path /home/ssl/liliya.work.pfx
#ENV ASPNETCORE_Kestrel__Certificates__Default__Password 134116

# 设置ssl证书
#ENV certPassword 123456
#RUN openssl genrsa -des3 -passout pass:${certPassword} -out server.key 2048
#RUN openssl rsa -passin pass:${certPassword} -in server.key -out server.key
#RUN openssl req -sha256 -new -key server.key -out server.csr -subj '/CN=localhost'
#RUN openssl x509 -req -sha256 -days 365 -in server.csr -signkey server.key -out server.crt
#RUN openssl pkcs12 -export -out liliya.work.pfx -inkey server.key -in server.crt -certfile server.crt -passout pass:${certPassword}

ENV CATALINA_HOME /usr/local/tomcat


#设置时区
RUN /bin/cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone

COPY . .

ENTRYPOINT ["dotnet", "Liliya.Core.API.dll"]