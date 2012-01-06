#region CODE HISTORY
/* --------------------------------------------------------------------------------
 * Client Name: 
 * Project Name: NHAutoMvcDemo.DomainModels.MapOverrides
 * Module: Web
 * Name: PublisherMappingOverride
 * Purpose: 
 *                   
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/6/2012 5:12:36 PM
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
    public class PublisherMappingOverride: IAutoMappingOverride<Publisher>
    {
        public void Override(AutoMapping<Publisher> mapping)
        {
            //demonstrate domain specific requirement to not save books from publisher
            //as the dit publisher screen does not show the books
            //it says to save publisher from the book entity as edit book contains publisher to select
            mapping.HasMany<Book>(x => x.Books).Inverse();
        }
    }
}
