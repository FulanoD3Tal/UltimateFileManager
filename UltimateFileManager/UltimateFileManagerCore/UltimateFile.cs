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
                return Path.Combine(Path.GetDirectoryName(file), Path.ChangeExtension(newName, Path.GetExtension(file)));
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
            if (!(((ICollection<string>)files).Count == newNames.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(newNames), "The given names are less or more that the total of the files");
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
            if (!(((ICollection<string>)files).Count == newNames.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(newNames), "The given names are less or more that the total of the files");
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ChangeDirectory(string directory, string file)
        {
            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentNullException(nameof(directory));
            }
            if (string.IsNullOrEmpty(file))
            {
                throw new ArgumentNullException(nameof(file));
            }
            return Path.Combine(directory, Path.GetFileName(file));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="newDirectory"></param>
        /// <returns></returns>
        public static List<string> ChangeDirectory(this IEnumerable<string> files, string newDirectory)
        {
            if (string.IsNullOrEmpty(newDirectory))
            {
                throw new ArgumentNullException(nameof(newDirectory));
            }
            List<string> newFiles = new List<string>();
            foreach (string file in files)
            {
                newFiles.Add(ChangeDirectory(newDirectory, file));
            }
            return newFiles;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static long Size(this IEnumerable<string> files)
        {
            long total = 0;
            foreach (string file in files)
            {
                FileInfo fileinfo = new FileInfo(file);
                total += fileinfo.Length;
            }
            return total;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ToSize(this long size)
        {
            double Kilobyte = 1000;
            double Megabyte = 1000 * Kilobyte;
            double Gibabyte = 1000 * Megabyte;
            double Terabyte = 1000 * Gibabyte;
            if (size >= Terabyte)
            {
                double result = size / Terabyte;
                return result.ToString("#.## Tb");
            }
            else if (size >= Gibabyte)
            {
                double result = size / Gibabyte;
                return result.ToString("#.## Gb");
            }
            else if(size >= Megabyte)
            {
                double result = size / Megabyte;
                return result.ToString("#.## Mb");
            }
            else if(size >= Kilobyte)
            {
                double result = size / 1000;
                return result.ToString("#.## Kb");
            }
            else
            {
                return size.ToString("#.## bytes");
            }
        }
    }
}
