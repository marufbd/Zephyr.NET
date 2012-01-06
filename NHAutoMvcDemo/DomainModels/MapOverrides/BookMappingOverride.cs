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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

#endregion REFERENCES

namespace NHAutoMvcDemo.DomainModels.MapOverrides
{
    public class BookMappingOverride : IAutoMappingOverride<Book>
    {
        public void Override(AutoMapping<Book> mapping)
        {
            mapping.Map(m => m.BookDescription).Length(2000);
        }
    }
}
