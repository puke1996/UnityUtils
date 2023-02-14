using System;
using System.Collections.Generic;
using System.IO;
using Plugins.Puke.Engine.CSharpExtensions;
using Plugins.Puke.UnityUtilities.UnityUtils;
using UnityEditor;

namespace Plugins.Puke.Engine.UnityMappingGenerator.Editor.CodeGenerator
{
    /// <summary>
    /// 几个区别
    /// 1 生成文件的路径
    /// 2 去掉get
    /// 3 去掉类型
    /// 4 目录
    /// 5 函数名
    /// 6 加到编辑器
    /// </summary>
    public static class EditorResourceMappingGenerator
    {
        public static void GenerateResourceMapping(string path, string @namespace)
        {
            DirectoryInfo root = new DirectoryInfo("Assets/");
            // 获取所有子文件夹
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            root.DoRecursively(dir => { dirs.Add(dir); });
            // 筛选出所有名字为Resources的文件夹
            dirs = dirs.FilterFolderName("Resources");
            // 遍历每个Resources文件夹,执行相同的操作
            List<string> relativePaths = new List<string>();
            foreach (var dir in dirs)
            {
                // 一个Resources文件夹下的所有文件夹的集合(包括自己)
                List<DirectoryInfo> dirGroup = new List<DirectoryInfo>();
                dir.DoRecursively(param => { dirGroup.Add(param); });
                // 获取所有的文件
                IEnumerable<FileInfo> files = dirGroup.GetAllFileInDirList();
                // 文件已经全部添加完毕
                foreach (var file in files)
                {
                    // meta是unity缓存文件
                    if (!file.FullName.Contains("meta"))
                    {
                        int index = file.FullName.IndexOf("Resources\\", StringComparison.Ordinal);
                        string relativePath = file.FullName.Substring(index + 10);
                        relativePaths.Add(relativePath);
                    }
                }
            }

            string content = GenerateCode(relativePaths, @namespace);
            FileUtils.Write(content, path);
            AssetDatabase.Refresh();
        }

        private static string GenerateCode(List<string> strs, string @namespace)
        {
            string content = "";
            content += "using UnityEngine;";
            content += "using System.Diagnostics.CodeAnalysis;";
            content += "namespace " + @namespace + "{";
            // content += "namespace " + Config.projectFolderName + ".Scripts.Mapping{";
            // content += "namespace enginepatch.config{";
            content += "[SuppressMessage(\"ReSharper\", \"InconsistentNaming\")]" +
                       "[SuppressMessage(\"ReSharper\", \"UnusedMember.Global\")]" +
                       "[SuppressMessage(\"ReSharper\",\"IdentifierTypo\")]" +
                       "[SuppressMessage(\"ReSharper\", \"StringLiteralTypo\")]";
            content += "public static class EditorResourceMapping{";
            // 还没处理同名的情况
            foreach (var str in strs)
            {
                string ext = StringExtensionMethods.GetFileExt(str);
                string fieldName = null;
                fieldName = str.Replace("\\", "");
                // fieldName = EMString.去掉后缀(fieldName);
                fieldName = fieldName.Replace(".", "_");
                fieldName = fieldName.Replace("(", "_");
                fieldName = fieldName.Replace(")", "_");
                fieldName = fieldName.Replace(" ", "_");
                fieldName = fieldName.Replace("-", "_");
                fieldName = fieldName.Replace("&", "_");
                fieldName = fieldName.Replace("/", "_");
                string path;
                path = str.Replace("\\", "/");
                // 去掉后缀名
                path = StringExtensionMethods.RmExt(path);
                // content += "public static GameObject Get" + fieldName + "(){" + "return Resources.LoadAllAsset<GameObject>(\"" +
                //            path + "\");}";
                content += "public static ";
                content += "string ";
                content += "_" + fieldName + "(){" + "return " + "\"" + path + "\"" + ";}";
                // content += path + "\");}";
            }

            content += "}}";
            return content;
        }
    }
}