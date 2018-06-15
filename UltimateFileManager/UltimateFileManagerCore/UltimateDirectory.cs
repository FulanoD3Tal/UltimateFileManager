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
            long total = 0;
            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo fileinfo in files)
            {
                total += fileinfo.Length;
            }
            return total;
        }
    }
}
