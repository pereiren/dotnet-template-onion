using System;
using System.Collections.Generic;
using System.Text;

/*
 * Encapsulate the tiny domain business rules. Structures that are unique 
 * by their properties and the whole object is immutable, once it is created
 * its state can not change.
 * https://martinfowler.com/bliki/ValueObject.html
 */

namespace Dotnet.Onion.Template.Domain.Tasks.ValueObjects
{
    public struct TaskId
    {
        private readonly Guid _taskId;

        public TaskId(Guid taskId)
        {
            if (taskId.Equals(Guid.Empty))
                throw new ArgumentNullException($"Task Id does not have any value");

            _taskId = taskId;
        }

        public Guid ToGuid()
        {
            return _taskId;
        }
    }
}
