FROM microsoft/dotnet:2.0.0-sdk-2.0.2-stretch

ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-netcore20-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-netcore20-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=fded627d6fac27254bd6fbc39997fdb00614bddd \
NEW_RELIC_APP_NAME=NewrelicTest

WORKDIR /app

ARG runtimeIdentifier=debian-x64
ARG files=./bin/Release/netcoreapp2.0/publish
COPY $files ./appcode

ARG NewRelic=./newrelic
COPY $NewRelic ./newrelic

RUN dpkg -i ./newrelic/newrelic-netcore20-agent_8.8.83.0_amd64.deb

ENV ASPNETCORE_URLS http://+:80
EXPOSE 80

WORKDIR /app/appcode
ENTRYPOINT ["dotnet", "./NewrelicTest.dll"]