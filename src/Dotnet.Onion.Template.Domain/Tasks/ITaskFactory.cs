using Dotnet.Onion.Template.Domain.Tasks.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

/*
 * Factories are concerned with creating new entities and value objects. 
 * They also validate the invariants for the newly created objects.
 * 
 * This is the interface definition to create Tasks (to be implemented in
 * Infrastructure layer)
 */

namespace Dotnet.Onion.Template.Domain.Tasks
{
    public interface ITaskFactory
    {
        Task CreateTaskInstance(Summary summary, Description description);
    }
}
