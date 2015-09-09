# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant::Config.run do |config|
  config.vm.box = "ubuntu/trusty64"

  config.vm.box_url = "ubuntu/trusty64"
  config.vm.provision :shell, :inline => "sudo apt-get update"
  config.vm.provision :shell, :inline => "sudo apt-get install curl -y"
  config.vm.provision :shell, :inline => "sudo apt-get install vim -y"
  
  config.vm.provision :shell, :inline => "curl -sSL https://get.docker.com/gpg | sudo apt-key add -"
  config.vm.provision :shell, :inline => "curl -sSL https://get.docker.com/ | sh"
  config.vm.provision :shell, :inline => "curl -L https://github.com/docker/compose/releases/download/1.2.0/docker-compose-`uname -s`-`uname -m` > docker-compose; chmod +x docker-compose; sudo mv docker-compose /usr/local/bin/docker-compose"

  #config.vm.forward_port 5002, 5000 # HealthKitServer
  config.vm.forward_port 8983, 8983 # Solr
  config.vm.forward_port 6379, 6379 # Redis
  config.vm.forward_port 3306, 3306 # MySQL
  config.vm.forward_port 5432, 5432 # PostgreSQL
end

Vagrant.configure("2") do |config|

config.vm.provider :virtualbox do |virtualbox|
            
virtualbox.customize ["modifyvm", :id, "--memory", "1024"]     
                 
end
end
