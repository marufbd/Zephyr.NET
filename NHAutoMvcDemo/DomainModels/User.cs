#region CODE HISTORY
/* --------------------------------------------------------------------------------
 * Client Name: 
 * Project Name: NHAutoMvcDemo.DomainModels
 * Module: Web
 * Name: User
 * Purpose: 
 *                   
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/11/2012 9:15:36 AM
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
    public class User : DomainEntity
    {
        [RegularExpression(@"[^A-Za-z0-9_@\.]|@{2,}|\.{5,}")]
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }

        protected User()
        {
            this.Username = "newuser";
            this.Password = "";
        }

        public User(string uname, string pwd)
        {
            this.Username = uname;
            this.Password = pwd;
        }
    }
}
