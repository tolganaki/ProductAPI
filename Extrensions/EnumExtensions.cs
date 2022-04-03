using System.ComponentModel;
using System.Reflection;

namespace ProductAPI.Extrensions
{
    public static class EnumExtensions
    {
        public static string ToDescription<TEnum>(this TEnum @enum)
        {
            FieldInfo fieldInfo = @enum.GetType().GetField(@enum.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?[0].Description ?? @enum.ToString();
        }
    }
}