# build

FROM mcr.microsoft.com/dotnet/sdk:10.0 as build
WORKDIR /src

COPY . .
RUN dotnet publish -f net10.0 -c release -o out

# launch

FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app

COPY --from=build /src/out /app
CMD dotnet Simplify.Scheduler.SimpleApp.dll
