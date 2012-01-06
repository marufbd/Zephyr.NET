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



#endregion REFERENCES

using System.Collections.Generic;
using MyFrameWork.Domain;
using NHibernate;

namespace MyFrameWork.Repository.Contract
{
    public interface IRepository<T> where T : DomainEntity
    {        
        IList<T> GetAll();

        T Get(long id);
        
        T SaveOrUpdate(T entity);
        void Delete(T entity);

        void Delete(long id);
    }
}