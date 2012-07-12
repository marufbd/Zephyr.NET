/*****
 * Code taken from sharparchitecture
 * https://github.com/sharparchitecture/Sharp-Architecture
 * 
 */
using System.Collections.Generic;
using System.Reflection;

namespace Zephyr.Domain
{
    /// <summary>
    ///     This serves as a base interface for <see cref="EntityWithTypedId{TId}" /> and 
    ///     <see cref = "Entity" />. Also provides a simple means to develop your own base entity.
    /// </summary>
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }

        IEnumerable<PropertyInfo> GetSignatureProperties();

        bool IsTransient();
    }
}