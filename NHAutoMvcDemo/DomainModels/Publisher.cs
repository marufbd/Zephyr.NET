#region CODE HISTORY
/* --------------------------------------------------------------------------------
 * Client Name: 
 * Project Name: NHAutoMvcDemo.DomainModels
 * Module: Web
 * Name: Publisher
 * Purpose: 
 *                   
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/2/2012 10:34:33 AM
 *  Description:    Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyFrameWork.Domain;

#endregion REFERENCES

namespace NHAutoMvcDemo.DomainModels
{
    public class Publisher : DomainEntity
    {
        public Publisher() : base()
        {
            this.PublisherName = "New publisher";
        }
        

        public virtual string PublisherName { get; set; }

        public virtual IList<Book> Books { get; set; } 
     
        public override string ToString()
        {
            return this.PublisherName;
        }
    }
}
