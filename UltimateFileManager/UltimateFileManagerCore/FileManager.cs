using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UltimateFileManagerCore
{
    public class FileManager
    {
        public event EventHandler<FileProgressUpdatedArgs> ProgressUpdatedEvent;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public bool CopyFile(string origin,string destiny)
        {
            using (FileStream fsorigin = new FileStream(origin, FileMode.Open), fsdestiny = new FileStream(destiny, FileMode.Create))
            {
                byte[] buffer = new byte[1048756];
                int readBytes;
                while ((readBytes = fsorigin.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fsdestiny.Write(buffer, 0, readBytes);
                    ProgressUpdatedEvent?.Invoke(this, new FileProgressUpdatedArgs(origin, destiny, fsorigin.Length, fsorigin.Position));
                } 
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public async Task<bool> CopyFileAsync(string origin,string destiny)
        {
            return await Task<bool>.Run(() => CopyFile(origin, destiny));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public bool MoveFile(string origin, string destiny)
        {
            CopyFile(origin, destiny);
            if (File.Exists(destiny))
            {
                File.Delete(origin);
            }
            return true;
        }

        public async Task<bool> MoveFileAsync(string origin,string destiny)
        {
            return await Task<bool>.Run(() => MoveFile(origin,destiny));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public bool CopyFiles(List<string> origin, List<string> destiny)
        {

            if (!(origin.Count == destiny.Count))
            {
                throw new ArgumentOutOfRangeException($"{nameof(origin)} - {nameof(destiny)} ", "The destiny files are less or more that the total of the origin files");
            }
            long progress = 0;
            long total = origin.Size();
            for (int i = 0; i < origin.Count; i++)
            {
                using (FileStream fsorigin = new FileStream(origin[i],FileMode.Open), fsdestiny = new FileStream(destiny[i], FileMode.Create))
                {
                    byte[] buffer = new byte[1048756];
                    int readBytes;
                    while ((readBytes = fsorigin.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsdestiny.Write(buffer, 0, readBytes);
                        progress += fsorigin.Position;
                        ProgressUpdatedEvent?.Invoke(this, new FileProgressUpdatedArgs(origin[i], destiny[i], total, progress));
                    }  
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public bool MoveFiles(List<string> origin, List<string> destiny)
        {

            if (!(origin.Count == destiny.Count))
            {
                throw new ArgumentOutOfRangeException($"{nameof(origin)} - {nameof(destiny)} ", "The destiny files are less or more that the total of the origin files");
            }
            long progress = 0;
            long total = origin.Size();
            for (int i = 0; i < origin.Count; i++)
            {
                using (FileStream fsorigin = new FileStream(origin[i], FileMode.Open), fsdestiny = new FileStream(destiny[i], FileMode.Create))
                {
                    byte[] buffer = new byte[1048756];
                    int readBytes;
                    while ((readBytes = fsorigin.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsdestiny.Write(buffer, 0, readBytes);
                        progress += fsorigin.Position;
                        ProgressUpdatedEvent?.Invoke(this, new FileProgressUpdatedArgs(origin[i], destiny[i], total, progress));
                    }
                }
                if (File.Exists(destiny[i]))
                {
                    File.Delete(origin[i]);
                }
            }
            return true;
        }
        public bool CopyDirectory(string origin, string destiny)
        {

            return true;
        }
    }
}
