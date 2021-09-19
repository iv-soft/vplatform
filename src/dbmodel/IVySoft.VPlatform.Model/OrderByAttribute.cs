using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderByAttribute : Attribute
    {
        public OrderByAttribute()
        {

        }

        public OrderByAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
