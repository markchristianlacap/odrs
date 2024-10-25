pnpm --filter client generate
dotnet publish
rm backend/bin/Release/net8.0/publish/dist -rf
cp -r client/.output/public backend/bin/Release/net8.0/publish/dist 
