using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using download.itstar.io.Data;

namespace download.itstar.io.Data
{
    public class FileReader
    {
        private const string dataFile = "data.txt";

        public static string[] GetLatestVersionFolder(string directoryPath)
        {
            var versionFolders = Directory.GetDirectories(directoryPath);
            string? latestVersionFolder = versionFolders.OrderByDescending(f => Path.GetFileName(f)).FirstOrDefault();
            return File.ReadAllLines(Path.Combine(latestVersionFolder ?? string.Empty, dataFile));
        }
    // public static string GetFileSize(long? byteCount)
    // {
    //     string size = "0 Bytes";
    //     if (byteCount >= 1073741824.0)
    //         size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
    //     else if (byteCount >= 1048576.0)
    //         size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
    //     else if (byteCount >= 1024.0)
    //         size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
    //     else if (byteCount > 0 && byteCount < 1024.0)
    //         size = byteCount.ToString() + " Bytes";

    //     return size;
    // }

        
    }
        
}



