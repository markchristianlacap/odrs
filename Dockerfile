FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS server
WORKDIR /app
COPY *.sln .
COPY backend/*.csproj ./backend/
RUN dotnet restore
COPY backend/. ./backend/
WORKDIR /app
RUN dotnet publish -c Release -o out

FROM node:iron-slim AS client
WORKDIR /app
RUN corepack enable
COPY client/package.json ./client/
COPY ./pnpm-lock.yaml ./pnpm-workspace.yaml ./package.json ./
RUN --mount=type=cache,id=pnpm-store,target=/root/.pnpm-store \
  pnpm install --frozen-lockfile
COPY client client
RUN pnpm --filter client genarate 

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
RUN apk add --no-cache icu-libs tzdata msttcorefonts-installer fontconfig && \
  update-ms-fonts && \
  fc-cache -f
COPY --from=server /app/out ./
COPY --from=client /app/client/.output/public ./dist
ENV TZ=Asia/Manila
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENTRYPOINT ["dotnet", "Backend.dll"]

