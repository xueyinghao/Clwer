using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WJ.Infrastructure.Extension
{
    /// <summary>
    /// 字段或属性的中文解释属性 
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class Desc : Attribute
    {
        /// <summary>    
        /// 获得字段或属性的中文解释.    
        /// </summary>    
        /// <value>字段或属性的中文解释.</value>    
        public string Value
        {
            get;
            private set;
        }
        /// <summary>    
        /// 初始化创建一个 <see cref="Desc"/> 类的实例, 用于指示字段或属性的解释说明.    
        /// </summary>    
        /// <param name="value">
        /// 字段或属性的解释说明.
        /// </param>    
        public Desc(string value)
        {
            Value = value;
        }
    }
    public static class EnumExtension
    {

        /// <summary>
        /// 获得枚举字段的特性(Attribute)，该属性不允许多次定义。
        /// </summary>/// 
        /// <typeparam name="T">特性类型。</typeparam>
        /// <param name="value">一个枚举的实例对象。</param>
        /// <returns>枚举字段的扩展属性。如果不存在则返回 <c>null</c> 。</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(T)) as T;
        }
    }
    public class Enum<T>
    {
        public static void Foreach(Action<T> action)
        {
            Array values = Enum.GetValues(typeof(T));
            foreach (var value in values)
            {
                action((T)value);
            }
        }
    }
}
