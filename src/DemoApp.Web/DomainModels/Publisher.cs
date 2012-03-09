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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zephyr.Domain;

#endregion REFERENCES

namespace DemoApp.Web.DomainModels
{
    public class Publisher : Entity
    {
        public Publisher()
        {
            this.PublisherName = "New publisher";
            this.Books=new List<Book>();
        }
        

        [Required]
        public virtual string PublisherName { get; set; }

        public virtual IList<Book> Books { get; protected set; }
     
        public override string ToString()
        {
            return this.PublisherName;
        }        
    }
}
