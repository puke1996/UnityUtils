using Plugins.Puke.Engine.UnityMappingGenerator.Editor.CodeGenerator;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Plugins.Puke.Engine.UnityMappingGenerator.Editor
{
    public class MappingGenerator : OdinEditorWindow
    {
        [MenuItem("Window/Mapping Generator")]
        private static void OpenWindow()
        {
            GetWindow<MappingGenerator>().Show();
        }

        [FolderPath] public string resourceMappingPath = "Assets/Scripts/Mapping";
        private string nameSpace = "Mapping";

        [Button]
        private void CreateResourceMapping()
        {
            ResourceMappingGenerator.GenerateResourceMapping(resourceMappingPath + "/ResourceMapping.cs", nameSpace);
        }

        [Button]
        private void CreateLayerMapping()
        {
            LayerMappingGenerator.CreateLayerMapping(resourceMappingPath + "/LayerMapping.cs", nameSpace);
        }

        [Button]
        private void CreateRefMgr()
        {
            TransformMappingGenerator.GenerateTransformMapping(resourceMappingPath + "/RefMgr.Instance.cs", nameSpace);
        }

        [Button]
        private void CreateEditorResourceMapping()
        {
            EditorResourceMappingGenerator.GenerateResourceMapping(resourceMappingPath + "/EditorResourceMapping.cs",
                nameSpace);
        }
    }
}