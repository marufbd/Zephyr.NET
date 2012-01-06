#region CODE HISTORY
/* --------------------------------------------------------------------------------
 * Client Name: 
 * Project Name: NHAutoMvcDemo.Models
 * Module: Web
 * Name: VmBook
 * Purpose: 
 *                   
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/2/2012 5:05:25 PM
 *  Description:    Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHAutoMvcDemo.DomainModels;

#endregion REFERENCES

namespace NHAutoMvcDemo.Models
{
    public class VmBook
    {
        public Book Book { get; set; }

        public SelectList PublisherList { get; set; }

        
        [Required(ErrorMessage = "A Publisher is required")]
        [DisplayName("Publisher")]
        public long SelectPublisherId { get; set; } 
    }
}
