using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editor.EditorExtensions
{
    public class EditorMenuAssets : UnityEditor.Editor
    {
        [MenuItem("Assets/Create/txt")]
        private static void CreateTxtFile()
        {
            var fileNameWithOutExt = "txt";
            var ext = "txt";
            var fileName =
                FileUtils.AutoRename(
                    Application.dataPath.Replace("Assets", "") + "/" +
                    AssetDatabase.GetAssetPath(Selection.activeObject), fileNameWithOutExt, ext);
            var currentFullPath = Application.dataPath.Replace("Assets", "") + "/" +
                                  AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + fileName;
            System.IO.File.WriteAllText(currentFullPath, "", Encoding.UTF8);
            AssetDatabase.Refresh();
            Selection.activeObject =
                AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + fileName,
                    typeof(Object));
        }

        [MenuItem("Assets/Create/lua")]
        private static void CreateLuaFile()
        {
            var fileNameWithOutExt = "lua";
            var ext = "lua";
            var fileName =
                FileUtils.AutoRename(
                    Application.dataPath.Replace("Assets", "") + "/" +
                    AssetDatabase.GetAssetPath(Selection.activeObject), fileNameWithOutExt, ext);
            var currentFullPath = Application.dataPath.Replace("Assets", "") + "/" +
                                  AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + fileName;
            System.IO.File.WriteAllText(currentFullPath, "", Encoding.UTF8);
            AssetDatabase.Refresh();
            Selection.activeObject =
                AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + fileName,
                    typeof(Object));
        }
    }
}