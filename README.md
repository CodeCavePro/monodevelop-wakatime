MonoDevelop/Xamarin Studio add-in for WakaTime
==============================================

WakaTime is a productivity & time tracking tool for programmers. Once the WakaTime plugin is installed, you get a dashboard with reports about your programming by time, language, project, and branch.

Installation
------------

Heads Up! WakaTime depends on [Python](http://www.python.org/getit/) being installed to work correctly.

1. Inside Visual Studio, navigate to `Tools` -> `Add-in Manager`

2. Click the `Gallery` tab, then search for `wakatime`.

3. Click the `Install` button and then when add-in installation dialog popups click `Install`.

4. On MonoDevelop/Xamarin Studio versions prior to 5.10 you might get an error message, just ignore it, it's a Mono.Addin bug, it has been already solved in latest releases.

3. Enter your [api key](https://wakatime.com/settings#apikey) from [https://wakatime.com/settings#apikey](https://wakatime.com/settings#apikey), then click `Apply` button. If you don't get any error message click close the dialog ([#1 issue](https://github.com/salaros/monodevelop-wakatime/issues/1) I'm still trying to figure out how to close modal GTK# windows).

4. Use MonoDevelop/Xamarin Studio like you normally do and your time will be tracked for you automatically.

5. Visit https://wakatime.com to see your logged time.

Build manually
------------
coming soon..

TODO
----
 * Create a shared library with common (for both Visual Studio and MonoDevelop/Xamarin Studio) C# code
 * Fix API Key and Settings windows [#1 issue](https://github.com/salaros/monodevelop-wakatime/issues/1)
 * Run some tests on Microsoft Windows + XamarinStudio
 * Port [WakaTime Cli](https://github.com/wakatime/wakatime) to C# to avoid installing and running Python

Credits
-------

Most of the code has been taken from [Visual Studio WakaTime](https://github.com/wakatime/visualstudio-wakatime) extension originally developed by WakaTime team
