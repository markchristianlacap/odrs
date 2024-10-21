pnpm --filter client generate
dotnet publish
cp -rf client/.output/public backend/bin/Release/net8.0/publish/dist 
