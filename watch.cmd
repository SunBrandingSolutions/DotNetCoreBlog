@ECHO OFF
SetLocal
set ASPNETCORE_ENVIRONMENT=Development

if not "%1"=="" (
  set ASPNETCORE_ENVIRONMENT=%1
)

pushd .\DotNetCoreBlog
dotnet watch run
cd
exit /b