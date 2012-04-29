#region CODE HISTORY
/* -------------------------------------------------------------------------------- 
 * Client Name: 
 * Project Name: MyFrameWork.Repository
 * Module: 
 * Name: IRepository
 * Purpose:              
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/2/2012 12:39:02 PM
 *  Description: Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES

using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;

#endregion REFERENCES

using System;
using System.Collections.Generic;
using Zephyr.Data.Models;
using Zephyr.Domain;

namespace Zephyr.Data.Repository.Contract
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IList<TEntity> GetAll();

        IList<TEntity> GetAllPaged(int pageIndex, int pageItems, SortOptions sortOptions=null);        

        TEntity Get(Guid guid);        
        
        TEntity SaveOrUpdate(TEntity entity);
        
        void Delete(TEntity entity);
        void Delete(Guid id);


        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> specification);
    }
}