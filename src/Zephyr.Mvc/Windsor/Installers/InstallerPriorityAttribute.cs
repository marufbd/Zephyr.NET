using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zephyr.Web.Mvc.Windsor.Installers
{
    [AttributeUsage(AttributeTargets.Class)]
    sealed class InstallerPriorityAttribute : Attribute
    {
        public const int DefaultPriority = 100;
        public int Priority { get; private set; }
        
        public InstallerPriorityAttribute(int priority)
        {
            this.Priority = priority;
        }
    }
}
