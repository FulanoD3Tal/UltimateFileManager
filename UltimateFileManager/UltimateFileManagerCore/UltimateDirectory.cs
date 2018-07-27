using System.IO;

namespace UltimateFileManagerCore
{
    public static class UltimateDirectory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static long Size(this string directory)
        {
            return Size(new DirectoryInfo(directory));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        public static long Size(this DirectoryInfo directoryInfo)
        {
            return Directory.EnumerateFiles(directoryInfo.FullName, "*.*", SearchOption.AllDirectories).Size();
        }
    }
}
