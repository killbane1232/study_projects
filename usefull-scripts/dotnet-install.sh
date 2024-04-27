sudo apt-get -y update
sudo apt-get -y install libunwind8 gettext
wget https://download.visualstudio.microsoft.com/download/pr/2c8da3b8-66b9-4906-ba3c-cb536f5c46eb/b5c8900abce86499ce826798eb2fbef7/dotnet-sdk-6.0.416-linux-arm.tar.gz
sudo mkdir /opt/dotnet
sudo tar -xvf dotnet-sdk-6.0.416-linux-arm.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/local/bin
dotnet --info