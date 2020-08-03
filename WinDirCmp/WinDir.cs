using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDirCmp
{
    class WinDir
    {
        private int depth;
        private String filePath;
        private String fileName;
        private String folder;
        private String extension;
        private bool isDirectory;
        private long size;
        private String date;
        private String[] subDirectoryPaths;
        private String[] subdirectoryNames;
        private String[] filePaths;
        private String[] fileNames;
        private List<WinDir> subdirectories = new List<WinDir>(); 


        public WinDir(int depth, String FilePath, string[] subdirectories, string[] files)
        {
            this.depth = depth;
            this.filePath = FilePath;
            this.subdirectoryNames = subdirectories;
            this.fileNames = files;
            this.isDirectory = File.GetAttributes(@FilePath).HasFlag(FileAttributes.Directory);
            this.fileName = isDirectory ? "" : Path.GetFileNameWithoutExtension(filePath);
            this.folder = isDirectory ? Path.GetFileName(filePath) : "";
            this.extension = new FileInfo(FilePath).Extension;
        }

        public bool TraverseAndFind(WinDir dir)
        {
            if (this.fileName.Equals(dir.FileName))
                return true;
            foreach (WinDir s in subdirectories)
            {
                if (s.TraverseAndFind(dir))
                    return true;
            }
            return false;
        }

        public string[] SubdirectoryNames { get => subdirectoryNames; set => subdirectoryNames = value; }
        public string[] FileNames { get => fileNames; set => fileNames = value; }
        public string FilePath { get => filePath; set => filePath = value; }
        public bool IsDirectory { get => isDirectory; set => isDirectory = value; }
        public long Size { get => size; set => size = value; }
        public string Date { get => date; set => date = value; }
        public string Folder { get => folder; set => folder = value; }
        public string Extension { get => extension; set => extension = value; }
        public string FileName { get => fileName; set => fileName = value; }
        internal List<WinDir> Subdirectories { get => subdirectories; set => subdirectories = value; }
    }
}
