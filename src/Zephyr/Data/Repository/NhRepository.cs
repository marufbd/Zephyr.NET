#region CODE HISTORY
/* -------------------------------------------------------------------------------- 
 * Client Name: 
 * Project Name: MyFrameWork.Repository
 * Module: 
 * Name: NHRepository
 * Purpose:              
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/2/2012 12:48:26 PM
 *  Description: Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Zephyr.Data.Models;
using Zephyr.Extensions;
using System.Linq;
using Zephyr.Data.NHib;
using Zephyr.Data.Repository.Contract;
using Zephyr.Domain;

#endregion REFERENCES

namespace Zephyr.Data.Repository
{
    public class NhRepository<T> : IRepository<T> where T : Entity
    {

        public ISession Session;

        public NhRepository(ISession session)
        {
            Session = session;
            Session.EnableFilter("DeletedFilter").SetParameter("IsDeleted", false);
        }

        public virtual IList<T> GetAll()
        {
            var query = this.Session.Query<T>();
            
            return query.ToList();
        }

        public IList<T> GetAllPaged(int pageIndex, int pageItems, SortOptions sortOptions)
        {
            var query = this.Session.Query<T>();

            //here the paging is done using linq to object applying OrderBy
            //on the whole list retrieved from database as MsSqlCe does not support variable limit query
            //on production database use query.Skip() instead
            return query.ToList().Skip((pageIndex-1)*pageItems).Take(pageItems).ToList();
        }


        public T Get(long id)
        {
            return this.Session.Get<T>(id);
        }

        public T SaveOrUpdate(T entity)
        {                        
            Session.SaveOrUpdate(entity);
            
            //line below triggers a NHibernate.AssertionFailure
            //Session.Flush();           

            return entity;
        }

        public void Delete(T entity)
        {
            Session.Delete(entity);            
        }

        public void Delete(long id)
        {
            Session.Delete(Session.Get<T>(id));            
        } 
    }
}