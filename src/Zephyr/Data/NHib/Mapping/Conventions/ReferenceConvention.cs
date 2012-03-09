#region CODE HISTORY
/* -------------------------------------------------------------------------------- 
 * Client Name: 
 * Project Name: MyFrameWork.NHib.Conventions
 * Module: 
 * Name: ReferencesConvention
 * Purpose:              
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/5/2012 3:00:13 PM
 *  Description: Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES

using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

#endregion REFERENCES

namespace Zephyr.Data.NHib.Mapping.Conventions
{
    internal class ReferenceConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(instance.Property.PropertyType.Name+"Fk");
        }
    }
}