using System;
using System.Collections.Generic;
using System.Text;

/*
 * Repository: Mediates between the domain and data mapping layers 
 * using a collection-like interface for accessing domain objects. 
 * 
 * https://martinfowler.com/eaaCatalog/repository.html
 * 
 * This is the interface of the repository for tasks which uses the
 * generic repository for all actions IRepository<Task>
 * To be implemented in Infrastructure layer
 */

namespace Dotnet.Onion.Template.Domain.Tasks
{
    public interface ITaskRepository : IRepository<Task>
    {
    }
}
