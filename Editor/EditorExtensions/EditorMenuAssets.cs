using System.Text;
using Plugins.Puke.Engine.UnityEditorAPI.Editor;
using Plugins.Puke.UnityUtilities.UnityUtils;
using UnityEditor;
using UnityEngine;

namespace Plugins.Puke.UnityUtilities.UnityEditorExtensions.Editor
{
    public class EditorMenuAssets : UnityEditor.Editor
    {
        [MenuItem("Assets/Create/txt")]
        private static void CreateTxtFile()
        {
            var fileNameWithOutExt = "txt";
            var ext = "txt";
            var fileName = FileUtils.AutoRename(EditorAPIPath.GetCurrentFullPath(), fileNameWithOutExt, ext);
            var currentFullPath = EditorAPIPath.GetCurrentFullPath() + "/" + fileName;
            System.IO.File.WriteAllText(currentFullPath, "", Encoding.UTF8);
            AssetDatabase.Refresh();
            Selection.activeObject =
                AssetDatabase.LoadAssetAtPath(EditorAPIPath.GetCurrentRelativePath() + "/" + fileName, typeof(Object));
        }

        [MenuItem("Assets/Create/lua")]
        private static void CreateLuaFile()
        {
            var fileNameWithOutExt = "lua";
            var ext = "lua";
            var fileName = FileUtils.AutoRename(EditorAPIPath.GetCurrentFullPath(), fileNameWithOutExt, ext);
            var currentFullPath = EditorAPIPath.GetCurrentFullPath() + "/" + fileName;
            System.IO.File.WriteAllText(currentFullPath, "", Encoding.UTF8);
            AssetDatabase.Refresh();
            Selection.activeObject =
                AssetDatabase.LoadAssetAtPath(EditorAPIPath.GetCurrentRelativePath() + "/" + fileName, typeof(Object));
        }
    }
}