using System;

namespace Dotnet.Onion.Template.Domain.Tasks.Events
{
    public class TaskCreatedEvent : TaskEvent
    {
        public TaskCreatedEvent(Guid id, string description, string summary)
        {
            Id = id;
            Summary = summary;
            Description = description;
        }
    }
}
