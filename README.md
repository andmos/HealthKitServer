HealthKitServer
===
HealthKitServer is a RESTfull server that uses Apple's new hub for health-realted information, HealthKit, to store a persons health related data.

With HealthKit, Apple has created a hub for health data from devices like [The Apple Watch](https://www.apple.com/watch/),
[FitBit](https://www.fitbit.com/no) and [Jawbone](https://jawbone.com/up). Why not store all that data outside the iPhone?


The project includes the server itself written in .NET (compiled with Mono for cross-platform support) and an iOS app written with [Xamarin Forms](http://xamarin.com/forms).

>Note: The Server API supports data from other sources as well, not just HealthKit.  

### Technology stack
* Nancy.Selfhost
* TopShelf
* Xamarin.Forms
* Dapper
* SimpleContainer

### Datastorage
i
I like to let users choose datastorage, so support for the following datastorage methods is created:
* Local Cache (in memory)
* MySQL
* ~~PostgreSQL~~
* ~~Redis~~
* ~~Solr~~

### Future plans
* Save all data as [OpenEHR](http://www.openehr.org/) archetypes.
* Separate core project from Xamarin Forms to make building on Linux easier (xbuild can only be run on OSX or Windows).
* More general interfaces for importing data from other sources than HealthKit. (deserialization of archetypes?)
* Move from SimpleContainer to [LightInject](https://github.com/seesharper/LightInject) when the codebase gets bigger.

### Build and try

    # Run local with data stored in memory:  
    git clone https://github.com/andmos/HealthKitServer.git
    cd HealthKitServer/
    ./buildServer run-local

A Vagrant-file is provided for running on a Linux VM with Docker preinstalled. Example with MySQLdatabase:

    vagrant up
    vagrant ssh
    cd /vagrant/Database
    ./setUpDockerMysqlDatabase
    cd ..
    # Edit HealthKitData.Host/bin/Debug/HealthKitServer.Host.exe.config and chose 'database' as source.
    # Enter healthkitserverdb as database in connectionstring!
    docker build -t healthkitserver .
    docker run -p 5000:5000 --link healthkitserverdb:healthkitserverdb -it healthkitserver

    curl http://localhost:5000/api/v1/ping

### Status

[![Build Status](https://travis-ci.org/andmos/HealthKitServer.svg?branch=master)](https://travis-ci.org/andmos/HealthKitServer)
