using Dotnet.Onion.Template.Domain.Tasks.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

/*
 * Factories are concerned with creating new entities and value objects. 
 * They also validate the invariants for the newly created objects.
 * 
 * This is the TaskFactory, which creates new instances of Tasks,
 * it inherits from Task Domain model as you can see
 */

namespace Dotnet.Onion.Template.Infrastructure.Factories
{
    public class TaskFactory : Domain.Tasks.Task
    {
        public TaskFactory()
        {

        }

        public TaskFactory(Summary summary, Description description)
        {
            TaskId = new TaskId(Guid.NewGuid());
            Summary = summary;
            Description = description;
        }
    }
}
