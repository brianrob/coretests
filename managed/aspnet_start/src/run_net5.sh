#!/bin/bash

iterations=10
echo "----------------------------------------------------------------"
echo "Running $iterations iterations of .NET 5 (Plus 1 warm-up iteration.)."
echo "----------------------------------------------------------------"

for (( i=0; i<=$iterations; i++ ))
do
    echo "Iteration $i"
    $MONOCMD bin/Release/netcoreapp3.0/linux-x64/publish/aspnet_start.exe
done

echo "----------------------------------------------------------------"
echo "Finished."
echo "----------------------------------------------------------------"
