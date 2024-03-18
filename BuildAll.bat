dotnet publish -r win-x64 -c Release -p:PublishReadyToRun=true -p:PublishSingleFile=true --self-contained true -p:IncludeNativeLibrariesForSelfExtract=true
dotnet publish -r osx-x64 -c Release /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained
dotnet publish -r linux-arm -c Release /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained
dotnet publish -r linux-x64 -c Release /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained

cd \Stuff\Repos\FotMobXmltv\Build
copy /Y "C:\Stuff\Repos\FotMobXmltv\bin\Release\net8.0\win-x64\publish\FotMobXmltv.exe" .
"C:\Program Files\7-Zip\7z" a -tzip FotMobXmltv-WIN.zip FotMobXmltv.exe FotMobXmltv_sample.dat FotMobXmltv.pdf

copy /Y "C:\Stuff\Repos\FotMobXmltv\bin\Release\net8.0\linux-arm\publish\FotMobXmltv" .
"C:\Program Files\7-Zip\7z" a -t7z FotMobXmltv-RasPi.7z FotMobXmltv FotMobXmltv_sample.dat FotMobXmltv.pdf

copy /Y "C:\Stuff\Repos\FotMobXmltv\bin\Release\net8.0\osx-x64\publish\FotMobXmltv" .
"C:\Program Files\7-Zip\7z" a -t7z FotMobXmltv-OSX.7z FotMobXmltv FotMobXmltv_sample.dat FotMobXmltv.pdf

copy /Y "C:\Stuff\Repos\FotMobXmltv\bin\Release\net8.0\linux-x64\publish\FotMobXmltv" .
"C:\Program Files\7-Zip\7z" a -t7z FotMobXmltv-LIN64.7z FotMobXmltv FotMobXmltv_sample.dat FotMobXmltv.pdf
