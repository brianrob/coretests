#!/bin/bash

dotnet ~/src/aspnet-benchmarks/src/BenchmarksDriver/bin/Release/netcoreapp2.1/BenchmarksDriver.dll \
    --server $SERVER_URL \
    --client $CLIENT_URL \
    --no-clean \
    --jobs https://raw.githubusercontent.com/brianrob/benchmarks/master/src/Benchmarks/benchmarks.te.mono-jit.json \
    --scenario Plaintext-AspNetCoreMono \
