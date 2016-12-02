#!/bin/bash
find . \
  -type d \
  -name "day*" \
  -exec sh -c \
  "echo '*** {} ***' && cd {} && dotnet restore && dotnet run" \;
