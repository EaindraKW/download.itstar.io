using System;
using System.IO;
using System.Globalization;
using download.itstar.io.Models;

namespace download.itstar.io.Data
{
    public class AppData : FileReader
    {
        private const string download = "download";
        private readonly string dataFolderPath = Path.Combine(Directory.GetCurrentDirectory(), download);

        private const string logo = "logo.png";

        private const string about = "about.txt";

        private const string version = "versions";

        private const string screen_shots = "screenshots";

        private const string dataFile ="data.txt";


        public List<App> AvailableBundlesList()
        {
            List<App> appList = new();
            var directories = Directory.GetDirectories(dataFolderPath);
            foreach (var directory in directories)
            {
                App app = new App();
                app.Name = Path.GetFileName(directory);
                app.Logo = Path.Combine(directory, logo);
                app.Software_about = string.Join(Environment.NewLine,File.ReadAllLines(Path.Combine(directory, about)));
                string[] data = GetLatestVersionFolder(Path.Combine(directory, version));
                if (data.Length >= 3)
                {
                    app.Path = data[0];
                    app.Count = int.TryParse(data[1], out int index) ? index : 0;
                    app.Release_notes = string.Join(Environment.NewLine, data.Skip(3));
                }

                appList.Add(app);
            }
            return appList;
        }

        public AppDetail OneBundle(string bundleName)
        {
            AppDetail appDetail = new AppDetail();
            var bundleDirectory = Path.Combine(Directory.GetCurrentDirectory(), download, bundleName);
            appDetail.Name = bundleName;
            appDetail.Logo = Path.Combine(bundleDirectory, logo);
           
            appDetail.Software_about = string.Join(Environment.NewLine,File.ReadAllLines(Path.Combine(bundleDirectory, about)));
            string[] data = GetLatestVersionFolder(Path.Combine(bundleDirectory, version));
            if (data.Length >= 3)
            {
                appDetail.Path = data[0];
                appDetail.Count = int.TryParse(data[1], out int index) ? index : 0;
                appDetail.Release_notes = string.Join(Environment.NewLine, data.Skip(3));

            }

            // using HttpClient client = new HttpClient();
            // appDetail.File_size =GetFileSize(client.GetFileSize("https://b.itstar.io/app/ebs_en_mobile_2_431.apk"));
            appDetail.File_size="";

            var versionFolders = Directory.GetDirectories(Path.Combine(bundleDirectory, version)).OrderByDescending(f => Path.GetFileName(f));
             string? LatestVersion=versionFolders.FirstOrDefault();
            
            appDetail.LatestVersionName=Path.GetFileName(LatestVersion);
            var appScreenshoots = Directory.GetFiles(Path.Combine(bundleDirectory, screen_shots));
            foreach (var appScreenshoot in appScreenshoots)
            {
                appDetail.Screen_shot.Add(appScreenshoot);
            }
            foreach (var versionName in versionFolders)
            {
                if (versionName !=LatestVersion )
                {
                    AppOtherVersion appOtherVersion = new AppOtherVersion();
                    appOtherVersion.versionName = Path.GetFileName(versionName);
                    string[] versionData = File.ReadAllLines(Path.Combine(Path.Combine(versionName),dataFile ));
                    if (versionData.Length >= 3)
                    {
                        if (DateOnly.TryParseExact(versionData[2], "yyyy-MM-dd", CultureInfo.InvariantCulture,DateTimeStyles.None, out DateOnly date))
                        {
                            appOtherVersion.ReleaseDate = date;
                        }
                        else
                        {
                            appOtherVersion.ReleaseDate = null;
                        }
                        appOtherVersion.DownloadLink = versionData[0];
                    }

                    appDetail.OtherVersions.Add(appOtherVersion);

                }

            }
            return appDetail;


        }

        public VersionHistory GetVersionHistory(string bundleName){
            
            List<ReleasedVersion> versions = new();
            VersionHistory versionHistory = new VersionHistory();
            var bundleDirectory = Path.Combine(Directory.GetCurrentDirectory(), download, bundleName);
            versionHistory.Name = bundleName;
            versionHistory.Logo = Path.Combine(bundleDirectory, logo);
            var versionFolders = Directory.GetDirectories(Path.Combine(bundleDirectory, version)).OrderByDescending(f => Path.GetFileName(f));
            // var versionFolders2= versionFolders.OrderByDescending(f => Path.GetFileName(f));
            foreach (var versionName in versionFolders)
            {
                    ReleasedVersion releasedVersion = new ReleasedVersion();
                    releasedVersion.versionName = Path.GetFileName(versionName);
                    string[] versionData = File.ReadAllLines(Path.Combine(Path.Combine(versionName),dataFile ));
                    if (versionData.Length >= 3)
                    {
                        if (DateOnly.TryParseExact(versionData[2], "yyyy-MM-dd", CultureInfo.InvariantCulture,DateTimeStyles.None, out DateOnly date))
                        {
                            releasedVersion.ReleaseDate = date;
                        }
                        else
                        {
                            releasedVersion.ReleaseDate = null;
                        }
                        releasedVersion.DownloadLink = versionData[0];
                        releasedVersion.Release_notes=string.Join(Environment.NewLine, versionData.Skip(3));
                    }

                   versionHistory.ReleasedVersions.Add(releasedVersion);

                

            }
            return versionHistory;

        }



    }
}
