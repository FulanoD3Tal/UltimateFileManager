using System;
using System.Collections.Generic;
using System.IO;

namespace UltimateFileManagerCore
{
    public static class UltimateFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public static string RenameFile(this string file, string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                new ArgumentNullException(nameof(newName));
            }
            if (string.IsNullOrEmpty(Path.GetExtension(newName)))
            {
                return Path.Combine(Path.GetDirectoryName(file),Path.ChangeExtension(newName,Path.GetExtension(file)));
            }
            else
            {
                return Path.Combine(Path.GetDirectoryName(file), newName);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="newNames"></param>
        /// <returns></returns>
        public static List<string> RenameFiles(this IEnumerable<string> files, List<string> newNames)
        {
            if (newNames == null)
            {
                throw new ArgumentNullException(nameof(newNames));
            }
            if (((ICollection<string>)files).Count != newNames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(newNames), "The given names are less that the total of the files");
            }
            int i = 0;
            List<string> newFiles = new List<string>();
            foreach (string file in files)
            {
                RenameFile(file, newNames[i]);
                i++;
            }
            return newFiles;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="newNames"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static List<string> RenameFiles(this IEnumerable<string> files, List<string> newNames, string extension)
        {
            if (newNames == null)
            {
                throw new ArgumentNullException(nameof(newNames));
            }
            if (string.IsNullOrEmpty(extension))
            {
                throw new ArgumentNullException(nameof(extension), "The extension can not be empty or null");
            }
            if (((ICollection<string>)files).Count != newNames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(newNames), "The given names are less that the total of the files");
            }
            int i = 0;
            List<string> newFiles = new List<string>();
            foreach (string file in files)
            {
                newFiles.Add(Path.ChangeExtension(Path.Combine(Path.GetDirectoryName(file), newNames[i]), extension));
                i++;
            }
            return newFiles;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="newExtension"></param>
        /// <returns></returns>
        public static List<string> ChangeExtension(this IEnumerable<string> files, string newExtension)
        {
            if (string.IsNullOrEmpty(newExtension))
            {
                throw new ArgumentNullException(nameof(newExtension));
            }
            List<string> newFiles = new List<string>();
            foreach (string file in files)
            {
                newFiles.Add(Path.ChangeExtension(file, newExtension));
            }
            return newFiles;
        }
    }
}
