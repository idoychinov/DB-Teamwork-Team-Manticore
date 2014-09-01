namespace SantasToyFactory.DataOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ionic.Zip;
    public static class ZipManipulator
    {
        public static ICollection<string> ExtractFile(string fileLocation, string exctractLocation)
        {
            ICollection<string> filesPaths = new List<string>();

            using(ZipFile archive = ZipFile.Read(fileLocation))
            {
                foreach (var entry in archive)
                {
                    entry.Extract(exctractLocation, ExtractExistingFileAction.OverwriteSilently);
                    string fileName = entry.FileName.Trim();
                    if (fileName.IndexOf('/') != fileName.Length - 1)
                    {
                        filesPaths.Add(exctractLocation + fileName);
                    }
                }
            }

            return filesPaths;
        }
    }
}
