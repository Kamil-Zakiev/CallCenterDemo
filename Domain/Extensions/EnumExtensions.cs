using System;
using System.Linq;
using Domain.Attributes;

namespace Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplay(this Enum @enum)
        {
            return @enum.GetType()
                .GetField(@enum.ToString())
                .GetCustomAttributes(false)
                .Cast<DisplayAttribute>()
                .SingleOrDefault()
                ?.Name;
        }
    }
}