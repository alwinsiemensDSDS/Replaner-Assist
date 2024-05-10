# Get Project working on Windows

- Install VS-Code
- Install .NET MAUI Extension in VS-Code
- Install .NET SDK from here: https://dotnet.microsoft.com/en-us/download
- Install maui workload in dotnet with
```
dotnet workload install maui
```
- clone this project, wait until project is ready and run with f5 (note the curly braces {} for startup options)
- running the project should run the app in a adjustable window (on windows)

# Get Android Emulator running

- install Android Studio from here: https://developer.android.com/studio?hl=de
- make initial android project to see emulator is running with hardware acceleration
- in android studio, install cmd-line tools for android sdk
- with cmdline-tools accept android licenses (note: must be in folder where sdkmanager file is located)```sdkmanager --licenses```
- maybe adjust sdk paths under "configure android" in vscode
- you should see the installed android emulator as a startup option
