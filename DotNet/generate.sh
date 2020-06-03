#!/bin/sh
find ../MessageStream/ -iname *.cs > generate-cs.txt
sed "/.*Compile Include.*/d" -i MessageWriter.csproj
sed "s/^/    <Compile Include=\"/g" -i generate-cs.txt
sed "s/$/\"\/>/g" -i generate-cs.txt
sed "/<ItemGroup>/r generate-cs.txt" -i MessageWriter.csproj
rm generate-cs.txt
