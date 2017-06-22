using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace InvestNinja.Core.Utils
{
    public static class EnumExtender
    {
        public static string GetDescription(this Enum enumerationValue)
        {
            Type type = enumerationValue.GetType();
            MemberInfo member = type.GetMembers().Where(w => w.Name == Enum.GetName(type, enumerationValue)).FirstOrDefault();
            var attribute = member?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute?.Description != null ? attribute.Description : enumerationValue.ToString();
        }
    }
}
