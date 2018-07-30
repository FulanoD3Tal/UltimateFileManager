using System;

namespace UltimateFileManagerCore
{
    public class FileProgressUpdatedArgs : EventArgs
    {
        public string OriginFile { get; private set; }
        public string DestinyFile { get; private set; }
        public long TotalBytes { get; private set; }
        public long BytesProcessed { get; private set; }

        public FileProgressUpdatedArgs(
            string originFile,
            string destinyFile,
            long totalBytes,
            long bytesProcessed
            )
        {
            OriginFile = originFile;
            DestinyFile = destinyFile;
            TotalBytes = totalBytes;
            BytesProcessed = bytesProcessed;
        }
    }
}