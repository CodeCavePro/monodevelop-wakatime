MonoDevelop/Xamarin Studio add-in for WakaTime v5.x
==============================================

WakaTime is a productivity & time tracking tool for programmers. Once the WakaTime plugin is installed, you get a dashboard with reports about your programming by time, language, project, and branch.

Installation
------------

Heads Up! WakaTime depends on [Python](http://www.python.org/getit/) being installed to work correctly.

1. Inside MonoDevelop/Xamarin Studio, navigate to `Tools` -> `Add-in Manager`

2. Click the `Gallery` tab, then search for `wakatime`.

3. Click the `Install` button and then when add-in installation dialog popups click `Install`.

4. On MonoDevelop/Xamarin Studio versions prior to 5.10 you might get an error message, just ignore it, it's a Mono.Addin bug, it has been already solved in latest releases.

5. Enter your [api key](https://wakatime.com/settings#apikey) from [https://wakatime.com/settings#apikey](https://wakatime.com/settings#apikey), then click `Apply` button.

6. You might have to restart your MonoDevelop/Xamarin Studio

7. Use MonoDevelop/Xamarin Studio like you normally do and your time will be tracked for you automatically.

8. Visit https://wakatime.com to see your logged time.

Installing manually
------------
You can build and install this addin manually. On Linux you can skip the first step.

1. On Mac OS X and Windows you might have to make `mdtool` globally accessible by..
    * On Mac OS X: `ln -sv /usr/bin/mdtool /Applications/Xamarin Studio.app/Contents/MacOS/mdtool`
    * On Windows: add `%ProgramFiles%"\Xamarin Studio\bin` or `%ProgramFiles(x86)%"\Xamarin Studio\bin` append to PATH environment variable

2. Just open the solution in MonoDevelop/Xamarin Studio and build it using the appropriate configuration (`Debug` for Linux and Mac and `DebugWin32` for Windows).
 
3. Inside MonoDevelop/Xamarin Studio, navigate to `Tools` -> `Add-in Manager`

4. Click the `Install from file...` button and browse to `/path/to/monodevelop-wakatime/bin/Debug` or `DebugWin32` folder, depending on your OS and install MonoDevelop.WakaTime_x.x.mpack

5. Click the `Install` button and follow the installation manual above starting from step 4.

TODO
-------

 * Port [WakaTime Cli](https://github.com/wakatime/wakatime) to C# to avoid installing and running Python

Credits
-------

Most of the code has been taken from [Visual Studio WakaTime](https://github.com/wakatime/visualstudio-wakatime) extension originally developed by WakaTime team
Hovewer it was made cross-platform and soon will be heavily refactored, including the complete porting of [WakaTime Cli](https://github.com/wakatime/wakatime) from Python to C#
