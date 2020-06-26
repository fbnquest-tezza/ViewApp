FROM jenkins/jenkins:lts

USER root

RUN wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb

RUN apt-get update; \
apt-get install -y apt-transport-https && \
apt-get update && \
apt-get install -y dotnet-sdk-3.1


RUN apt-get update; \
apt-get install -y apt-transport-https && \
apt-get update && \
apt-get install -y aspnetcore-runtime-3.1