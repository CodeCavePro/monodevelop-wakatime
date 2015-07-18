
using Mono.Addins;

[assembly:Addin ("WakaTime", 
        Namespace = "MonoDevelop",
        Version = "0.1.3",
        Category = "IDE extensions")]

[assembly: AddinName("WakaTime")]
[assembly: AddinDescription("WakaTime is a productivity & time tracking tool for programmers. Once the WakaTime plugin is installed, you get a dashboard with reports about your programming by time, language, project, and branch.")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinAuthor("Zhmayev Yaroslav (salaros)")]

[assembly: AddinDependency("::MonoDevelop.Core", MonoDevelop.BuildInfo.Version)]
[assembly: AddinDependency("::MonoDevelop.Ide", MonoDevelop.BuildInfo.Version)]
