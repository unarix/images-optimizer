# Images Optimizer

## Description
CLI utility to resize and reduce images massively

## Compile
dotnet publish -c release -r ubuntu.16.04-x64 --self-contained

## Run
chmod 777 images-optimizer
./images-optimizer

## Make excecutable globaly
sudo ln -s '/home/your_user/your_path_sourcecode/images-optimizer/bin/release/netcoreapp2.1/ubuntu.16.04-x64/images-optimizer' /usr/bin/images-optimizer

## Arguments
                           ¯\_(ツ)_/¯
   -h Help
   -r Recursive
   -s Size in pixels, example: -r 150 (800px by default)
   -q Quiality in percentage, example: -q 90 (100% by default)


