using System.Collections.Generic;
using System.Linq;
using Plugins.Puke.UnityUtilities.UnityUtils;
using UnityEditor;
using UnityEngine;

namespace Plugins.Puke.Engine.UnityMappingGenerator.Editor.CodeGenerator
{
    public static class TransformMappingGenerator
    {
        /// <summary>
        /// 通常没有必要使用这个
        /// 当处理场景极度复杂的他人项目时，使用此工具
        /// </summary>
        public static void GenerateTransformMapping(string path, string @namespace)
        {
            List<GameObject> gameObjects = Finder.FindAllObjects<GameObject>().ToList();
            List<string> names = new List<string>();
            for (int i = 0; i < gameObjects.Count; i++)
            {
                string name = gameObjects[i].name;
                if (names.Contains(name))
                {
                    continue;
                }

                names.Add(name);
            }

            // 生成代码
            string content = GenerateCode(names, @namespace);
            FileUtils.Write(content, path);
            AssetDatabase.Refresh();
        }

        private static string GenerateCode(List<string> names, string @namespace)
        {
            // using
            // string content = "using Me.Engine.Scripts.Finder;";
            string content = ""; // 上面一行换成这个了
            content += "using UnityEngine;";
            content += "using System.Diagnostics.CodeAnalysis;";
            // 命名空间
            content += "namespace " + @namespace + "{";
            // 关闭编辑器智能提示
            content += "[SuppressMessage(\"ReSharper\", \"InconsistentNaming\")]" +
                       "[SuppressMessage(\"ReSharper\", \"UnusedMember.Global\")]" +
                       "[SuppressMessage(\"ReSharper\",\"IdentifierTypo\")]" +
                       "[SuppressMessage(\"ReSharper\", \"StringLiteralTypo\")]";
            // 写类
            content += "public static class RefMgr{";
            // 写每个函数
            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];
                string varName = name.Replace(" ", "");
                varName = varName.Replace("(", "");
                varName = varName.Replace(")", "");
                content += "private static Transform _" + varName + ";";
                content += "public static Transform Get" + varName + "(){";
                content += "if (_" + varName + "==null){_" + varName + "=Finder.FindActiveObjByName(\"" + name +
                           "\").transform;}";
                content += "return _" + varName + ";";
                content += "}";
            }

            // 结束
            content += "}}";
            return content;
        }
    }
}