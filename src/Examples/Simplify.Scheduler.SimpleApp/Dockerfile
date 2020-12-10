# build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /src

COPY . .
RUN dotnet publish -f netcoreapp3.1 -c release -o out

# launch

FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app

COPY --from=build /src/out /app
CMD dotnet Simplify.Scheduler.SimpleApp.dll
