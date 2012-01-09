#region CODE HISTORY
/* --------------------------------------------------------------------------------
 * Client Name: 
 * Project Name: NHAutoMvcDemo.DomainModels
 * Module: Web
 * Name: Book
 * Purpose: 
 *                   
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/2/2012 10:31:31 AM
 *  Description:    Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyFrameWork.Domain;

#endregion REFERENCES

namespace NHAutoMvcDemo.DomainModels
{
    public class Book : DomainEntity
    {
        public  Book()
        {
            this.PublishedDate = DateTime.Now;
        }


        
        [Required]
        public virtual string BookName { get; set; }

        public virtual string BookDescription { get; set; }
        
        public virtual DateTime PublishedDate { get; set; }
        
        public virtual Publisher Publisher { get; set; }

        public virtual IList<Author> Authors { get; set; }
    }
}
