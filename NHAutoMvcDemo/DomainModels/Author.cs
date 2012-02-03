#region CODE HISTORY
/* --------------------------------------------------------------------------------
 * Client Name: 
 * Project Name: NHAutoMvcDemo.DomainModels
 * Module: Web
 * Name: Author
 * Purpose: 
 *                   
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/2/2012 10:36:48 AM
 *  Description:    Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zephyr.Domain;

#endregion REFERENCES

namespace NHAutoMvcDemo.DomainModels
{
    public class Author : Entity
    {
        [Required]
        public virtual string AuthorName { get; set; }

        public virtual DateTime AuthorBirthDate { get; set; }

        public virtual IList<Book> WrittenBooks { get; protected set; } 
    }
}
