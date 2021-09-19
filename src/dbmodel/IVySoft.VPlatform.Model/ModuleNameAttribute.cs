using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.Model
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ModuleNameAttribute : Attribute
    {
        public string Name { get; set; }

        public ModuleNameAttribute()
        {

        }
        public ModuleNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
