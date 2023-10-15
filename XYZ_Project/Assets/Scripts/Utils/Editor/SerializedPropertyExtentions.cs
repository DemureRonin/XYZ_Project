using System;
using UnityEditor;

namespace Utils.Editor
{
    public static class SerializedPropertyExtentions
    {
        public static bool GetEnum<TEumType>(this SerializedProperty property, out TEumType retValue) 
            where TEumType : Enum
        {
            retValue = default;
            var names = property.enumNames;
            if (names == null || names.Length == 0)
            {
                return false;
            }

            var enumName = names[property.enumValueIndex];
            retValue = (TEumType) Enum.Parse(typeof(TEumType), enumName);
            return true;
        }
    }
}