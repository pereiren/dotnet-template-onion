using Dotnet.Onion.Template.Domain.Tasks;
using Dotnet.Onion.Template.Domain.Tasks.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Onion.Template.Infrastructure.Repositories
{
    /*
     * Repository: Mediates between the domain and data mapping layers 
     * using a collection-like interface for accessing domain objects. 
     * https://martinfowler.com/eaaCatalog/repository.html
     * 
     * This is the implementation for ITaskRepository which needs to
     * implement the generic IRepository actions. 
     * Both are located in Domain layer given that they are interfaces
     * attached to Task domain (these interfaces are called ports in
     * hexagonal architecture and the implementation in this class is
     * called adapter)
     * 
     * With this architecture pattern your data access code can be changed
     * easily only performing the changes in this class. 
     * You may want to use a MongoDB, SQL or whatever, you just need to 
     * change it here.
     */

    public class TaskRepository : ITaskRepository
    {
        private readonly ITaskFactory _taskFactory;

        public TaskRepository(ITaskFactory taskFactory)
        {
            _taskFactory = taskFactory;
        }

        public Task<Domain.Tasks.Task> Add(Domain.Tasks.Task taskEntity)
        {
            return System.Threading.Tasks.Task.FromResult(
                _taskFactory.CreateTaskInstance(new Summary("summary test"), new Description("description test")));
        }

        public Task<List<Domain.Tasks.Task>> FindAll()
        {
            var tasks = System.Threading.Tasks.Task.FromResult(new List<Domain.Tasks.Task> {
                _taskFactory.CreateTaskInstance(new Summary("summary test"), new Description("description test"))});

            return tasks;
        }

        public Task<Domain.Tasks.Task> FindById(Guid id)
        {
            return System.Threading.Tasks.Task.FromResult(
                _taskFactory.CreateTaskInstance(new Summary("summary test"), new Description("description test")));
        }

        public System.Threading.Tasks.Task Remove(Guid id)
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
