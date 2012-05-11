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
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Zephyr.Data.Models;
using Zephyr.Data.NHib.UoW;
using Zephyr.Data.UnitOfWork;
using Zephyr.Extensions;
using System.Linq;
using Zephyr.Data.NHib;
using Zephyr.Data.Repository.Contract;
using Zephyr.Domain;
using Zephyr.Specification;

#endregion REFERENCES

namespace Zephyr.Data.Repository
{
    public class NhRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {

        public ISession Session { get; private set; }

        public NhRepository()
        {
            //Session = session;
            if(UnitOfWorkScope.IsStarted)
            {
                var uow = UnitOfWorkScope.Current;
                Session = ((NhUnitOfWork)uow).CurrentSession;
            }
            else
            {
                Session = ServiceLocator.Current.GetInstance<ISession>();
            }
                

            Session.EnableFilter("DeletedFilter").SetParameter("IsDeleted", false);
        }

        public virtual IList<TEntity> GetAll()
        {
            var query = this.Session.Query<TEntity>();
            
            return query.ToList();
        }

        public IList<TEntity> GetAllPaged(int pageIndex, int pageItems, SortOptions sortOptions)
        {
            var query = this.Session.Query<TEntity>();

            //here the paging is done using linq to object applying OrderBy
            //on the whole list retrieved from database as MsSqlCe does not support variable limit query
            //on production database use query.Skip() instead
            return query.ToList().Skip((pageIndex-1)*pageItems).Take(pageItems).ToList();
        }


        public TEntity Get(Guid guid)
        {
            return this.Session.Get<TEntity>(guid);
        }

        public TEntity SaveOrUpdate(TEntity entity)
        {
            Session.SaveOrUpdate(entity);            

            return entity;
        }

        public void Delete(TEntity entity)
        {
            Session.Delete(entity); 
        }

        public void Delete(Guid guid)
        {
            Session.Delete(Session.Get<TEntity>(guid));
        } 


        //Queries
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> specification)
        {
            return Session.Query<TEntity>().Where(specification);
        }

        public IQueryable<TEntity> Query(ISpecification<TEntity> specification)
        {
            return Session.Query<TEntity>().Where(specification.Predicate);
        }
    }
}