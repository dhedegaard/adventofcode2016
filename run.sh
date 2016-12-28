#!/bin/bash
find . \
  -type d \
  -name "day*" | grep -v day11 | grep -v day23 | sort | while read fname
do
    echo "*** $fname ***"
    cd $fname
    dotnet restore
    dotnet run
    cd ..
done
