dotnet build -c Docker
docker build -t licensemanager-web .
docker run -p 5050:5050 licensemanager-web