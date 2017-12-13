MonoDevelop/Xamarin Studio add-in for WakaTime v5.x
==============================================

[![MonoDevelop Version](https://img.shields.io/badge/MonoDevelop-v5.x-3CA0DE.svg)](http://www.monodevelop.com/download/)
[![XamarinStudio Version](https://img.shields.io/badge/XamarinStudio-v5.x-9E72C9.svg)](https://www.xamarin.com/download/)
[![GitHub license](https://img.shields.io/github/license/CodeCavePro/monodevelop-wakatime.svg)](https://github.com/CodeCavePro/monodevelop-wakatime/blob/master/LICENSE.md)
[![GitHub top language](https://img.shields.io/github/languages/top/CodeCavePro/monodevelop-wakatime.svg)](https://github.com/CodeCavePro/monodevelop-wakatime/search?l=C%23)
[![Github all releases](https://img.shields.io/github/downloads/CodeCavePro/monodevelop-wakatime/total.svg)](https://github.com/CodeCavePro/monodevelop-wakatime/releases/)

[![Linux/macOS Builds via Travis CI](https://travis-ci.org/CodeCavePro/monodevelop-wakatime.svg?branch=5.x)](https://travis-ci.org/CodeCavePro/monodevelop-wakatime)
[![Linux/macOS Builds via AppVeyor](https://ci.appveyor.com/api/projects/status/etc2j9e3ptg2vr1i/branch/5.x?svg=true)](https://ci.appveyor.com/project/salaros/monodevelop-wakatime/branch/5.x)

[WakaTime](https://wakatime.com/features) is a productivity & time tracking tool for programmers. Once the WakaTime plugin is installed, you get a dashboard with reports about your programming by time, language, project, and branch.

# Installation

The latests stable versions of WakaTime addin are **[available via GitHub Releases](https://github.com/CodeCavePro/monodevelop-wakatime/releases)**

Heads Up! WakaTime depends on [Python](http://www.python.org/getit/) being installed to work correctly. On macOS and Linux Python is usually pre-installed, while on Windows WakaTime tries to install automatically a portable version of Python.

1. Inside MonoDevelop/Xamarin Studio/Visual Studio for Mac, navigate to `Tools` -> `Add-in Manager`

2. Click the `Install from file...` button and browse to `/path/to/monodevelop-wakatime/bin/Debug` or `DebugWin32` folder, depending on your OS and install MonoDevelop.WakaTime_x.x.mpack

3. Click the `Install` button and then when add-in installation dialog popups click `Install`.

4. On MonoDevelop/Xamarin Studio/Visual Studio for Mac versions prior to 5.10 you might get an error message, just ignore it, it's a Mono.Addin bug, it has been already solved in latest releases.

5. Enter your [api key](https://wakatime.com/settings#apikey) from [https://wakatime.com/settings#apikey](https://wakatime.com/settings#apikey), then click `Apply` button.

6. You might have to restart your MonoDevelop/Xamarin Studio/Visual Studio for Mac

7. Use MonoDevelop/Xamarin Studio/Visual Studio for Mac like you normally do and your time will be tracked for you automatically.

8. Visit [Wakatime Dashboard](http://wakatime.com/dashboard) to see your logged time.

# Installing via Addin Gallery

## !!! Unfortunately MonoDevelop / Xamarin Studio / Visual Studio for Mac adding gallery is currently broken at the moment (it's not building/publishing fresh copies of addins automatically) !!!

1. Inside MonoDevelop/Xamarin Studio/Visual Studio for Mac, navigate to `Tools` -> `Add-in Manager`

2. Click the `Gallery` tab, then search for `wakatime`.

3. Click the `Install` button and follow the [installation guide above](#installation) starting from step #4.

## Build & Install Manually

You can build and install this addin manually. On Linux you can skip the first step.

1. Make make `mdtool` globally accessible.

    * On macOS open the Terminal and run the following command:
    ```bash
    sudo ln -sv /Applications/Xamarin Studio.app/Contents/MacOS/mdtool /usr/bin/
    ```
    * On Windows just add `%ProgramFiles%"\Xamarin Studio\bin` or `%ProgramFiles(x86)%"\Xamarin Studio\bin` append to PATH environment variable
    * On Linux `mdtool` is usually globally accessible, otherwise locate it and symlink it to `/usr/local/bin/` or similar

2. Just open the solution in MonoDevelop/Xamarin Studio/Visual Studio for Mac and build it using the appropriate configuration (`Debug` for Linux and macOS and `DebugWin32` for Windows).
Or use [NuGet](https://www.nuget.org/downloads) + [XBuild](http://www.mono-project.com/docs/tools+libraries/tools/xbuild/) / [MSBuild](https://en.wikipedia.org/wiki/MSBuild) in order to build it from the command-line:
```bash
nuget restore ./src
msbuild /p:Configuration=<Debug or DebugWin32 here> /t:Build ./src
```

3. Inside MonoDevelop/Xamarin Studio/Visual Studio for Mac, navigate to `Tools` -> `Add-in Manager`

4. Click the `Install from file...` button and browse to `/path/to/monodevelop-wakatime/bin/Debug` or `DebugWin32` folder, depending on your OS and install MonoDevelop.WakaTime_x.x.mpack

5. Click the `Install` button and follow the [installation guide above](#installation) starting from step #4.

## Credits

Some code has been taken from [Visual Studio WakaTime](https://github.com/wakatime/visualstudio-wakatime) extension originally developed by WakaTime team. Hovewer that code has been heavily refactored, made cross-platform etc.

## TODO

Try to port WakaTime to C# to avoid relying on Python