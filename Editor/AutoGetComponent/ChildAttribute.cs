using UnityEngine;

namespace Plugins.Puke.UnityUtilities.AutoGetcomponentAttributes
{
    /// <summary>
    /// 编辑器模式下自动为序列化字段赋值
    /// </summary>
    public class ChildAttribute : PropertyAttribute
    {
        public ChildAttribute(string name)
        {
        }

        public ChildAttribute()
        {
        }
    }
}