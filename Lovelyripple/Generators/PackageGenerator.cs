using System;
using System.Collections.Generic;
using System.IO;
using Lovelyripple.Models;

namespace Lovelyripple.Generators
{
    [Serializable]
    public class PackageGenerator
    {
        public string Id;
        public string Description;
        public Version Version;
        public string Authors;
        public string Owners;
        public string LicenseUrl;
        public string ProjectUrl;
        public string IconUrl;
        public bool RequireLicenseAcceptance;
        public string ReleaseNotes;
        public string Copyright;
        public string Tags;
        public string ModelsAssemblyGuid;
        public List<ClassModel> ClassModels;
        public DirectorySetGenerator DirectorySet;

        string _guid;

        public PackageGenerator()
        {
            _guid = System.Guid.NewGuid().ToString();
            ClassModels = new List<ClassModel>();
        }
        public void Build()
        {
            CodeGenerator.SaveTo = new System.IO.FileInfo(Path.Combine(DirectorySet.WorkingDirectory,Id+".cs"));
            CodeGenerator.CreateSourceCode(ClassModels);
            
        }

        public static Version CreateFullVersionFromMajorMinor(int majorVersion, int minorVersion)
        {
            var cachedNow = DateTime.Now;
            int buildNumber = (cachedNow.Date - new DateTime(2000, 1, 1)).Days;
            int revisionNumber = (int)(cachedNow - cachedNow.Date).TotalSeconds / 2;
            return new Version(majorVersion, minorVersion, buildNumber, revisionNumber);
        }
    }
}
