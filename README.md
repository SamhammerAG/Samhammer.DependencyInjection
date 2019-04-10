# Samhammer.DependencyInjection

### Nuget Package Install
https://www.nuget.org/packages/Samhammer.DependencyInjection/

### Nuget Package Publish
- dotnet pack -c Release
- nuget push .\bin\Release\Samhammer.DependencyInjection.*.nupkg NUGET_API_KEY -src https://api.nuget.org/v3/index.json
- (optional) nuget setapikey NUGET_API_KEY -source https://api.nuget.org/v3/index.json

