using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Onion.Template.Domain.Tasks.Events
{
    public class TaskDeletedEvent : TaskEvent
    {
        public TaskDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}
