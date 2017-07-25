@ECHO OFF
SetLocal
set ASPNETCORE_ENVIRONMENT=Production

pushd .\DotNetCoreBlog
dotnet watch run
cd
exit /b