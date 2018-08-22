using System;

namespace Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayAttribute : Attribute
    {
        public string Name { get; set; }
    }
}