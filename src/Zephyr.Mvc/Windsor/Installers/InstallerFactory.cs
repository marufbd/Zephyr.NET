using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor.Installer;

namespace Zephyr.Web.Mvc.Windsor.Installers
{
    public class WindsorBootstrap : InstallerFactory
    {
        public override IEnumerable<Type> Select(IEnumerable<Type> installerTypes)
        {
            var retval = installerTypes.OrderBy(this.GetPriority);
            return retval;
        }

        private int GetPriority(Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(InstallerPriorityAttribute), false).FirstOrDefault() as InstallerPriorityAttribute;
            return attribute != null ? attribute.Priority : InstallerPriorityAttribute.DefaultPriority;
        }

    }
}