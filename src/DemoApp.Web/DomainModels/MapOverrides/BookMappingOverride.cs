#region CODE HISTORY
/* --------------------------------------------------------------------------------
 * Client Name: 
 * Project Name: NHAutoMvcDemo.DomainModels.MapOverrides
 * Module: Web
 * Name: BookMap
 * Purpose: 
 *                   
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/3/2012 10:42:09 AM
 *  Description:    Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

#endregion REFERENCES

namespace DemoApp.Web.DomainModels.MapOverrides
{
    public class BookMappingOverride : IAutoMappingOverride<Book>
    {
        public void Override(AutoMapping<Book> mapping)
        {
            mapping.Map(x => x.BookDescription).Length(5000);            
        }
    }
}
