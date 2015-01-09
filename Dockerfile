From mono:3.10


ADD HealthKitServer.Host HealthKitServer.Host
ADD HealthKitServer.Server HealthKitServer.Server
ADD HealthKitServer HealthKitServer
ADD iOS iOS
ADD HealthKitServer.sln . 
#RUN xbuild HealthKitServer.sln 

EXPOSE 5000
CMD mono /HealthKitServer.Host/bin/Debug/HealthKitServer.Host.exe
