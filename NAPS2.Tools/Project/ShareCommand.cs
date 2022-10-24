using NAPS2.Tools.Project.Targets;

namespace NAPS2.Tools.Project;

public static class ShareCommand
{
    public static int Run(ShareOptions opts)
    {
        bool doIn = opts.ShareType is "both" or "in";
        bool doOut = opts.ShareType is "both" or "out";

        var localFolder = Path.Combine(Paths.Publish, ProjectHelper.GetDefaultProjectVersion());
        var syncFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OneDrive",
            "Software", "naps2", "publish");

        var l = doIn ? "<" : "";
        var r = doOut ? ">" : "";
        var arrow = $"{l}-{r}";
        Console.WriteLine($"Syncing {localFolder} {arrow} {syncFolder}");

        if (doIn)
        {
            foreach (var file in GetFiles(syncFolder))
            {
                CopyFileIfNewer(file, localFolder, opts.Verbose);
            }
        }
        if (doOut)
        {
            foreach (var file in GetFiles(localFolder))
            {
                CopyFileIfNewer(file, syncFolder, opts.Verbose);
            }
        }
        Console.WriteLine("Done.");
        return 0;
    }

    private static void CopyFileIfNewer(FileInfo file, string targetFolder, bool verbose)
    {
        var targetFile = new FileInfo(Path.Combine(targetFolder, file.Name));
        if (!targetFile.Exists || targetFile.LastWriteTimeUtc < file.LastWriteTimeUtc)
        {
            if (targetFile.Exists)
            {
                var tempPath = targetFile.FullName + ".old";
                targetFile.MoveTo(tempPath);
                try
                {
                    if (verbose)
                    {
                        Console.WriteLine($"Replacing {file.FullName} -> {targetFile.FullName}");
                    }
                    file.CopyTo(targetFile.FullName);
                    File.Delete(tempPath);
                }
                catch (Exception)
                {
                    File.Move(tempPath, targetFile.FullName);
                    throw;
                }
            }
            else
            {
                if (verbose)
                {
                    Console.WriteLine($"Copying {file.FullName} -> {targetFile.FullName}");
                }
                file.CopyTo(targetFile.FullName);
            }
        }
        else
        {
            if (verbose)
            {
                var time = targetFile.LastWriteTimeUtc == file.LastWriteTimeUtc ? "same" : "older";
                Console.WriteLine($"Ignoring {file.FullName} ({time})");
            }
        }
    }

    private static IEnumerable<FileInfo> GetFiles(string folderPath)
    {
        return new DirectoryInfo(folderPath).EnumerateFiles()
            .Where(x => x.Extension is ".exe" or ".msi" or ".zip" or ".pkg");
    }
}