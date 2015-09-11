#! /bin/bash
# Work in progress. Will be done with FAKE and Paket.  

nuget restore HealthKitServer.sln 

xbuild HealthKitServer.Host/HealthKitServer.Host.csproj
