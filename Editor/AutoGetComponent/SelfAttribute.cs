using UnityEngine;

namespace Plugins.Puke.UnityUtilities.AutoGetcomponentAttributes
{
    /// <summary>
    /// 编辑器模式下自动为序列化字段赋值
    /// </summary>
    public class SelfAttribute : PropertyAttribute
    {
        public SelfAttribute()
        {
        }
    }
}


// [System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
// class GetComponentAttribute : System.Attribute
// {
// }