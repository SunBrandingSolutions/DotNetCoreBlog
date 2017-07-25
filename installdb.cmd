@ECHO OFF
SetLocal

pushd .\DotNetCoreBlog
dotnet ef database update
cd
exit /b