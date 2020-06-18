# Images optimizer

## Description

The reason why you should resize images if you plan to load images to the internet for display on a web page or for inserting into a presentation is that image file size matters.

On the web, the larger the image size the longer it will take the web page to load. For users who have slower connections (f.e in mobiles), this matters a lot. Image size also matters a lot when you insert images into presentations such as PowerPoint, because if you keep the image at its original size, and insert 10-20 images into your presentation, your final PowerPoint size would be huge!

This simple CLI application allows you to modify all the images you have in a certain directory, resulting in a **new** directory with the **optimized images**.

![alt text](https://raw.githubusercontent.com/unarix/images-optimizer/master/screenshot.png?raw=true)

## How to Compile

    dotnet publish -c release -r ubuntu.16.04-x64 --self-contained

## How to Run

    chmod 777 images-optimizer
    ./images-optimizer

## How to make excecutable globally

    sudo ln -s '/home/your_user/your_path_sourcecode/images-optimizer/bin/release/netcoreapp2.1/ubuntu.16.04-x64/images-optimizer' /usr/bin/images-optimizer

## Arguments

                    ¯\_(ツ)_/¯

-   -h Help
-   -r Recursive (800px of width and 90% of quality by default)
-   -w Size width in pixels (and keeps the aspect ratio), example: -w 150
-   -q Quiality in percentage, example: -q 90

## Basic usage

Try to optimize all image files in current working directory

        images-optimizer -r

Try to optimize all image files in current working directory, with quality at 90%:

        images-optimizer -r -q 90

Try to optimize all image files in current working directory, with quality at 90% and width of 600px keeping the original aspect ratio

        images-optimizer -r -q 90 -w 600


## You have a suggestion?

Let me know, by opening a new issue
