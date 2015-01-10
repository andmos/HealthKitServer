From mono:latest

ADD HealthKitServer.Host HealthKitServer.Host
ADD HealthKitServer.Server HealthKitServer.Server
ADD HealthKitServer HealthKitServer
ADD iOS iOS
ADD HealthKitServer.sln . 
# For the time Mono does not contain the correct PCL Assemblies. Make sure to build outside of container. 
# RUN xbuild HealthKitServer.sln 

EXPOSE 5000
CMD mono /HealthKitServer.Host/bin/Debug/HealthKitServer.Host.exe
