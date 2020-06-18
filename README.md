# Images optimizer

## Description
CLI utility to resize and reduce images massively

![alt text](https://raw.githubusercontent.com/unarix/images-optimizer/master/screenshot.png?raw=true)

## How to Compile
    dotnet publish -c release -r ubuntu.16.04-x64 --self-contained

## How to Run
    chmod 777 images-optimizer
    ./images-optimizer

## How to make excecutable globally
    sudo ln -s '/home/your_user/your_path_sourcecode/images-optimizer/bin/release/netcoreapp2.1/ubuntu.16.04-x64/images-optimizer' /usr/bin/images-optimizer

## Arguments / Usage

                    ¯\_(ツ)_/¯

-   -h Help
-   -r Recursive (800px of width and 90% of quality by default)
-   -w Size width in pixels (and keeps the aspect ratio), example: -w 150
-   -q Quiality in percentage, example: -q 90



