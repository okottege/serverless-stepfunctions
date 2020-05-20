dotnet restore PolicyCreator.Application.sln
dotnet tool install -g Amazon.Lambda.Tools --framework netcoreapp3.1
dotnet lambda package -c Release -f netcoreapp3.1 -o dist\policy-creator.zip -pl PolicyCreator.Functions\
sls deploy -s dev