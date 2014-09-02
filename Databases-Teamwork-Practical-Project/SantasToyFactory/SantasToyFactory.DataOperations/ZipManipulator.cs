namespace SantasToyFactory.DataOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;

    using Ionic.Zip;

    public static class ZipManipulator
    {
        public static ICollection<string> ExtractFile(string fileLocation, string exctractLocation)
        {
            ICollection<string> filesPaths = new List<string>();

            using(ZipFile archive = ZipFile.Read(fileLocation))
            {
                archive.ExtractAll(exctractLocation, ExtractExistingFileAction.OverwriteSilently);
            }

            return filesPaths;
        }
    }
}
