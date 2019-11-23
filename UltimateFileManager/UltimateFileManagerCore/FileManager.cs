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
        public bool CopyFile(string origin, string destiny)
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
        public async Task<bool> CopyFileAsync(string origin, string destiny)
        {
            return await Task.Run(() => CopyFile(origin, destiny));
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

        public async Task<bool> MoveFileAsync(string origin, string destiny)
        {
            return await Task.Run(() => MoveFile(origin, destiny));
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
                using (FileStream fsorigin = new FileStream(origin[i], FileMode.Open), fsdestiny = new FileStream(destiny[i], FileMode.Create))
                {
                    byte[] buffer = new byte[1048756];
                    int readBytes;
                    while ((readBytes = fsorigin.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fsdestiny.Write(buffer, 0, readBytes);
                        progress += readBytes;
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
        public async Task<bool> CopyFilesAsync(List<string> origin, List<string> destiny)
        {
            return await Task.Run(() => CopyFiles(origin,destiny));
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
                        progress += readBytes;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public async Task<bool> MoveFilesAsync(List<string> origin, List<string> destiny)
        {
            return await Task.Run(() => MoveFiles(origin, destiny));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <param name="createDestiny"></param>
        /// <returns></returns>
        public bool CopyDirectory(string origin, string destiny)
        {
            if (!Directory.Exists(origin))
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (!Directory.Exists(destiny))
            {
                throw new ArgumentNullException(nameof(destiny));
            }
            IEnumerable<string> origin_files = Directory.EnumerateFiles(origin, "*", SearchOption.AllDirectories);
            List<string> new_files = new List<string>();
            foreach (string origin_file in origin_files)
            {
                string directory = new FileInfo(origin_file).DirectoryName.Replace(origin,destiny);
                string new_file = UltimateFile.ChangeDirectory(directory, Path.GetFileName(origin_file));
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory); 
                }
                new_files.Add(new_file);
            }
            
            return CopyFiles(new List<string>(origin_files),new_files);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public async Task<bool> CopyDirectoryAsync(string origin, string destiny)
        {
            return await Task.Run(() => CopyDirectory(origin,destiny));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public bool MoveDirectory(string origin, string destiny)
        {
            if (!Directory.Exists(origin))
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (!Directory.Exists(destiny))
            {
                throw new ArgumentNullException(nameof(destiny));
            }
            IEnumerable<string> origin_files = Directory.EnumerateFiles(origin, "*", SearchOption.AllDirectories);
            List<string> new_files = new List<string>();
            foreach (string origin_file in origin_files)
            {
                string directory = new FileInfo(origin_file).DirectoryName.Replace(origin, destiny);
                string new_file = UltimateFile.ChangeDirectory(directory, Path.GetFileName(origin_file));
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                new_files.Add(new_file);
            }
            if(MoveFiles(new List<string>(origin_files), new_files))
                Directory.Delete(origin,true);
            if (Directory.Exists(origin))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public async Task<bool> MoveDirectoryAsync(string origin, string destiny)
        {
            return await Task.Run(() => MoveDirectory(origin, destiny));
        }
    }
}
