using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/*
 * Repository: Mediates between the domain and data mapping layers 
 * using a collection-like interface for accessing domain objects. 
 * https://martinfowler.com/eaaCatalog/repository.html
 * 
 * This is a generic interface for repositories to be used
 */

namespace Dotnet.Onion.Template.Domain
{
    public interface IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        Task<TEntity> FindById(Guid id);
        Task<List<TEntity>> FindAll();
        Task<TEntity> Add(TEntity entity);
        Task Remove(Guid id);
    }
}
