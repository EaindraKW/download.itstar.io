using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace download.itstar.io.Models
{
    public class VersionHistory
    {
    public string? Name{get;set;}
    public string? Logo{get;set;}
    public List<ReleasedVersion> ReleasedVersions{ get; set; }= new List<ReleasedVersion>();
    }

    public class ReleasedVersion:AppOtherVersion
    {
     
    public string? Release_notes{get;set;}
    }
}