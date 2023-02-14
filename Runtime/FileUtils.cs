using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Plugins.Puke.UnityUtilities.UnityUtils
{
    /// <summary>
    /// 文件与文件夹的创建,建议就使用C#的API,不使用Unity的API
    /// 使用Unity的API好处是更快,立即刷新,但是写的代码只能运行在非运行状态的Unity编辑器下,过于局限
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// 创建路径
        /// </summary>
        public static void CreatePath(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
        }

        /// <summary>
        /// 将Texture保存为PNG,没有目录则递归创建目录
        /// @"/home/puke/
        /// </summary>
        public static void Write(Texture2D texture, string path)
        {
            texture.Apply();
            var bytes = texture.EncodeToPNG();
            // 创建路径
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            // 创建文件
            File.Create(path).Dispose();
            // 写入数据
            File.WriteAllBytes(path, bytes);
        }

        // 创建路径,再保存文件
        public static void Write(string contents, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            File.Create(path).Dispose();
            File.WriteAllText(path, contents);
        }

        public static string Read(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 重命名
        /// </summary>
        public static string AutoRename(string folder, string fileName, string ext)
        {
            int number = 2;
            while (File.Exists(folder + "/" + fileName + "." + ext))
            {
                fileName += number;
                number++;
            }

            return fileName + "." + ext;
        }

        /// <summary>
        /// 加载PNG，只支持JPG和PNG
        /// </summary>
        public static Texture2D LoadPNG(string filePath)
        {
            Texture2D texture2D = null;
            byte[] fileData;
            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                texture2D = new Texture2D(3840, 2160, TextureFormat.RGBA32, default);
                texture2D.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }

            return texture2D;
        }

        /// <summary>
        /// 加载PNG，只支持JPG和PNG
        /// 避免反复开辟内存空间版
        /// 不要通过texture2D是否为null来判断是否加载成功
        /// </summary>
        public static void LoadPNG(Texture2D texture2D, string filePath)
        {
            // Texture2D texture2D = null;
            // byte[] fileData;
            if (texture2D == null)
            {
                throw new Exception("请先初始化texture2D");
            }

            if (File.Exists(filePath))
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                // texture2D = new Texture2D(3840, 2160, TextureFormat.RGBA32, default);
                texture2D.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            // return texture2D;
        }

        public delegate void DirInfoHandler(DirectoryInfo dirInfo);

        /// <summary>
        /// 递归,深度优先
        /// </summary>
        public static void DoRecursively(this DirectoryInfo dirInfo, DirInfoHandler dirInfoHandler)
        {
            dirInfoHandler(dirInfo);
            foreach (var subDirInfo in dirInfo.GetDirectories())
            {
                DoRecursively(subDirInfo, dirInfoHandler);
            }
        }

        /// <summary>
        /// 根据文件夹名筛选文件夹
        /// </summary>
        public static List<DirectoryInfo> FilterFolderName(this IEnumerable<DirectoryInfo> dirInfos, string folderName)
        {
            return dirInfos.Where(dirInfo => dirInfo.Name.Equals(folderName)).ToList();
        }

        /// <summary>
        /// 获取DirInfoList下的所有FileInfo,不递归
        /// </summary>
        public static List<FileInfo> GetAllFileInDirList(this IEnumerable<DirectoryInfo> dirInfos)
        {
            var fileInfos = new List<FileInfo>();
            foreach (var dir in dirInfos)
            {
                fileInfos.AddRange(dir.GetFiles());
            }

            return fileInfos;
        }

        /// <summary>
        /// 将源文件夹下的所有文件复制到目标文件夹
        /// </summary>
        public static void CopyFiles(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    CopyFiles(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        /// <summary>
        /// 将源文件夹复制到目标文件夹
        /// </summary>
        public static int CopyFolder(string sourceFolder, string destFolder)
        {
            try
            {
                string folderName = Path.GetFileName(sourceFolder);
                string destfolderdir = Path.Combine(destFolder, folderName);
                string[] filenames = Directory.GetFileSystemEntries(sourceFolder);
                foreach (string file in filenames) // 遍历所有的文件和目录
                {
                    if (Directory.Exists(file))
                    {
                        string currentdir = Path.Combine(destfolderdir, Path.GetFileName(file));
                        if (!Directory.Exists(currentdir))
                        {
                            Directory.CreateDirectory(currentdir);
                        }

                        CopyFolder(file, destfolderdir);
                    }
                    else
                    {
                        string srcfileName = Path.Combine(destfolderdir, Path.GetFileName(file));
                        if (!Directory.Exists(destfolderdir))
                        {
                            Directory.CreateDirectory(destfolderdir);
                        }

                        File.Copy(file, srcfileName);
                    }
                }

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}