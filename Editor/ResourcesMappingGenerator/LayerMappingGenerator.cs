using Plugins.Puke.UnityUtilities.UnityUtils;
using UnityEditor;
using UnityEngine;

namespace Plugins.Puke.Engine.UnityMappingGenerator.Editor.CodeGenerator
{
    public static class LayerMappingGenerator
    {
        /// <summary>
        /// 由于逻辑比较简单,生成代码就不单独作为一个函数了
        /// </summary>
        public static void CreateLayerMapping(string relativePath, string @namespace)
        {
            string content = "using System.Diagnostics.CodeAnalysis;";
            content += "namespace " + @namespace + "{";
            content += "[SuppressMessage(\"ReSharper\", \"InconsistentNaming\")]" +
                       "[SuppressMessage(\"ReSharper\", \"UnusedMember.Global\")]" +
                       "[SuppressMessage(\"ReSharper\",\"IdentifierTypo\")]" +
                       "[SuppressMessage(\"ReSharper\", \"StringLiteralTypo\")]";
            content += "public static class LayerMapping{";
            for (int i = 0; i < 32; i++)
            {
                if (LayerMask.LayerToName(i) != null && LayerMask.LayerToName(i) != "")
                {
                    var varName = LayerMask.LayerToName(i).Replace(" ", "");
                    content += "public static string " + varName + "=\"" + LayerMask.LayerToName(i) + "\";";
                }
            }

            content += "}}";
            FileUtils.Write(content, relativePath);
            AssetDatabase.Refresh();
        }
    }
}