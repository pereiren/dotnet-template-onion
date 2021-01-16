using Dotnet.Onion.Template.Domain.Tasks.Events;
using System.Threading.Tasks;

namespace Dotnet.Onion.Template.Application.Handlers
{
    public class TaskEventHandler
    {
        public async Task HandleTaskCreatedEvent(TaskCreatedEvent taskCreatedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }

        public async Task HandleTaskDeletedEvent(TaskDeletedEvent taskDeletedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }
    }
}
