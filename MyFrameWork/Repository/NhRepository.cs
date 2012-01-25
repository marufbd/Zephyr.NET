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
using System.Collections.Generic;
using MyFrameWork.Domain;
using MyFrameWork.NHib;
using MyFrameWork.Repository.Contract;
using NHibernate;

#endregion REFERENCES

namespace MyFrameWork.Repository
{
    public class NhRepository<T> : IRepository<T> where T:DomainEntity
    {
        public ISession Session { 
            get {
                ISession session= NHibernateSession.Initialize(null);
                // set tenant global filter
                //session.EnableFilter("TenantFilter").SetParameter("TenantId", "1");
                return session;
            } 
        }

        public virtual IList<T> GetAll()
        {
            ICriteria criteria = this.Session.CreateCriteria(typeof(T));
            return criteria.List<T>();
        }

        public T Get(long id)
        {
            return this.Session.Get<T>(id);
        }        

        public T SaveOrUpdate(T entity)
        {
            ISession session = this.Session;
            session.SaveOrUpdate(entity);
            session.Flush();

            return entity;
        }

        public void Delete(T entity)
        {
            this.Session.Delete(entity);
        }

        public void Delete(long id)
        {
            ISession session = this.Session;
            
            session.Delete(session.Get<T>(id));
            session.Flush();
        } 
    }
}