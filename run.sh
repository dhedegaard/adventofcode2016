#!/bin/bash
find . -type d -name "day*" -exec sh -c "echo '*** {} ***' && cd {} && dotnet run" \;
