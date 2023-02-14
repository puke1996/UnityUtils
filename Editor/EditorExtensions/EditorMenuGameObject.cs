using System;
using Plugins.Puke.UnityUtilities.UnityExtensionMethods;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

namespace Plugins.Puke.UnityUtilities.UnityEditorExtensions.Editor
{
    public class EditorMenuGameObject : UnityEditor.Editor
    {
        private const string Rename = "GameObject/Rename/";

        /// <summary>
        /// 批量重命名， 将所有选中的节点的名字重命名为第一个选中的节点的名字
        /// </summary>
        [MenuItem(Rename + "Rename2First", false, 10)]
        static void RenameAll(MenuCommand menuCommand)
        {
            string name = Selection.activeGameObject.name;
            GameObject gameObject = (GameObject) menuCommand.context;
            gameObject.name = name;
        }

        /// <summary>
        /// 将按钮名重命名为文本名
        /// </summary>
        [MenuItem(Rename + "RenameBtn2Text", false, 10)]
        static void RenameButtonNameToTextName(MenuCommand menuCommand)
        {
            GameObject gameObject = (GameObject) menuCommand.context;
            Text text = gameObject.GetComponentInChildren<Text>();
            gameObject.name = text.text;
        }

        /// <summary>
        /// 将文本名重命名为按钮名
        /// </summary>
        [MenuItem(Rename + "RenameTxt2Btn", false, 10)]
        static void RenameTextNameToButtonName(MenuCommand menuCommand)
        {
            GameObject gameObject = (GameObject) menuCommand.context;
            Text text = gameObject.GetComponentInChildren<Text>();
            text.text = gameObject.name;
        }

        private const string ModelProcessing = "GameObject/ModelProcessing/";

        [MenuItem(ModelProcessing + "Delete Camera Recursively", false, 10)]
        private static void DeleteCamera(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            gameObject.DestroyComponents(new Type[] {typeof(Camera)});
        }

        [MenuItem(ModelProcessing + "Delete LookAtConstraint Recursively", false, 10)]
        private static void DeleteLookAtConstraint(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            gameObject.DestroyComponents(new Type[] {typeof(LookAtConstraint)});
        }

        [MenuItem(ModelProcessing + "Delete Light Recursively", false, 10)]
        private static void DeleteLight(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            gameObject.DestroyComponents(new Type[] {typeof(Light)});
        }

        [MenuItem(ModelProcessing + "Create MeshCollider Recursively", false, 10)]
        public static void CreateMeshCollider(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            gameObject.CreateMeshCollider();
        }

        [MenuItem(ModelProcessing + "Reset Min Bounds Of MeshRenderer to 000", false, 10)]
        private static void ResetPosToFirstOctant(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            gameObject.transform.SetBoundsMin000();
        }

        [MenuItem(ModelProcessing + "Reset MeshRenderer center to 000", false, 10)]
        public static void ResetPos(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            gameObject.transform.SetBoundsCenter000();
        }

        [MenuItem(ModelProcessing + "Set Convex Recursively", false, 10)]
        private static void SetConvex(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            gameObject.SetConvex(true);
        }

        [MenuItem(ModelProcessing + "Cancel Set Convex Recursively", false, 10)]
        private static void CancelSetConvex(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            gameObject.SetConvex(false);
        }

        [MenuItem(ModelProcessing + "Log MeshRenderer Center Pos", false, 10)]
        public static void LogCenterPos(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            var centerPos = gameObject.Center();
            Debug.Log("中心坐标" + centerPos);
        }

        private static string materialPath = null; // 位于Resources文件夹下,示例:"木头材质"

        [MenuItem(ModelProcessing + "Replace Material Recursively", false, 10)]
        private static void ReplaceMissingMaterials(MenuCommand menuCommand)
        {
            var material = Resources.Load<Material>(materialPath);
            ((GameObject) menuCommand.context).ReplaceAllMaterials(null, material);
        }

        /// <summary>
        /// 主相机,游戏对象已处于合适的位置,缩放尺寸以使得刚好可见
        /// 如果相机是透视相机,缩放到距离相机位置的视距大小
        /// 游戏对象的高度用Bounds.Y代替
        /// </summary>
        [MenuItem(ModelProcessing + "Zoom To Camera Size", false, 10)]
        private static void OptimizeSize(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
            var frustum = Camera.main.GetFrustumByDistance(System.Math.Abs(Camera.main.transform.position.z));
            var deltaScale = frustum / (gameObject.GetRendererBounds().size.y);
            gameObject.transform.localScale *= deltaScale;
        }

        // 放在地上
        [MenuItem(ModelProcessing + "Put On The Ground", false, 10)]
        private static void PutOnTheGround(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            var bounds = gameObject.GetRendererBounds();
            gameObject.transform.position += new Vector3(0, 0 - bounds.min.y, 0);
            // var frustum = Camera.main.GetFrustumByDistance(EMMath.求绝对值(Camera.main.transform.position.z));
            // var deltaScale = frustum / (gameObject.GetBounds().size.y);
            // gameObject.transform.localScale *= deltaScale;
        }

        [MenuItem(ModelProcessing + "Fix BoxCollider Pos", false, 10)]
        private static void FixColliderPos(MenuCommand menuCommand)
        {
            var gameObject = (GameObject) menuCommand.context;
            var bounds = gameObject.GetRendererBounds();
            var collider = gameObject.GetComponent<BoxCollider>();
            var colliderCenterX = bounds.center.x / gameObject.transform.localScale.x;
            var colliderCenterY = bounds.center.y / gameObject.transform.localScale.y;
            var colliderCenterZ = bounds.center.z / gameObject.transform.localScale.z;
            collider.center = new Vector3(colliderCenterX, colliderCenterY, colliderCenterZ);
        }
    }
}