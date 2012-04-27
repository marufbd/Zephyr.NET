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

using System;
using System.Collections.Generic;
using Zephyr.Data.Models;
using Zephyr.Domain;

namespace Zephyr.Data.Repository.Contract
{
    public interface IRepository<T> where T : Entity
    {
        IList<T> GetAll();

        IList<T> GetAllPaged(int pageIndex, int pageItems, SortOptions sortOptions=null);

        T Get(string guid);

        T Get(Guid guid);

        T Get(long id);
        
        T SaveOrUpdate(T entity);
        
        void Delete(T entity);
        void Delete(Guid id);
        void Delete(long id);
    }
}