using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zephyr.Domain;

namespace Zephyr.Web.Api
{
    public interface IApiService<TEntity> where TEntity : DomainEntity
    {

    }
}
