HealthKitServer
===
HealthKitServer is a REST-based server that uses Apple's new hub for health-realted information, HealthKit, to store a persons health related data.

The project includes the server itself written in .NET (compiled with Mono for cross-platform support) and an iOS app written with [Xamarin Forms](http://xamarin.com/forms).

### Technology stack
* Nancy.Selfhost
* TopShelf
* Xamarin.Forms
* Dapper

### Datastorage

I like to let users choose datastorage, so support for the following datastorage methods is created:
* Local Cache (in memory)
* MySQL
* ~~PostgreSQL~~
* ~~Redis~~
* ~~Solr~~

 ### Future plans
* Save all data as [OpenEHR](http://www.openehr.org/) archetypes.
* Separate core project from Xamarin Forms to make building on Linux easier (xbuild can only be run on OSX or Windows).

### Build and try

    # Run local with data stored in memory:  
    git clone https://github.com/andmos/HealthKitServer.git
    cd HealthKitServer/
    xbuild .
    mono HealthKitServer.Host/bin/Debug/HealthKitServer.Host.exe

A Vagrant-file is provided for running on a Linux VM with Docker preinstalled. Example with MySQLdatabase:

    vagrant up
    vagrant ssh
    cd /vagrant/Database
    ./setUpDockerMysqlDatabase
    cd ..
    # Edit HealthKitData.Host/bin/Debug/HealthKitServer.Host.exe.config and chose 'database' as source.
    # Enter healthkitserverdb as database in connectionstring!
    docker build -t healthkitserver .
    docker run -p 5000:5000 --link healthkitserverdb:healthkitserverdb -i -t healthkitserver

    curl -G http://localhost:5000/api/v1/getAllHealthKitData
