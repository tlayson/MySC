@SET FrameworkDir=C:\Windows\Microsoft.NET\Framework\v4.0.30319
@SET FrameworkVersion=v4.0.30319
@SET FrameworkSDKDir=
@SET PATH=%FrameworkDir%;%FrameworkSDKDir%;%PATH%
@SET LANGDIR=EN

msbuild.exe YetAnotherForum.NET.sln /p:Configuration=Deploy /p:Platform="Any CPU" /t:Clean;Build /p:WarningLevel=0