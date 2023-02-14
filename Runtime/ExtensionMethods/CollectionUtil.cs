using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods
{
    public static class CollectionUtil
    {
        /// <summary>
        /// 将子类数组转换为父类数组
        /// 无法强制转换，请使用此函数进行转换
        /// </summary>
        public static List<T2> Convert2ParentList<T1, T2>(this List<T1> objects)
        {
            return objects.Cast<T2>().ToList();
        }

        /// <summary>
        /// 获取二维数组[,]的列数，x
        /// </summary>
        public static int GetColumnLength<T>(this T[,] t)
        {
            // 总行数,row
            return t.GetLength(0);
        }

        /// <summary>
        /// 获取二维数组[,]的行数，y
        /// </summary>
        public static int GetRowLength<T>(this T[,] t)
        {
            return t.Length / t.GetColumnLength();
        }
    }
}