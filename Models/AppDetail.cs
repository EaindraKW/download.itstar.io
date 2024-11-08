using System;

namespace download.itstar.io.Models;

public class AppDetail : App
{
    public string? File_size{get;set;}
    public List<AppOtherVersion> OtherVersions{ get; set; }= new List<AppOtherVersion>();
    public List<string> Screen_shot{get;set; }= new List<string>();

}

public class AppOtherVersion
{
    public string? versionName { get; set; }
    public DateOnly? ReleaseDate {get;set;}
    public string? DownloadLink {get;set;}
}
