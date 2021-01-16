using Dotnet.Onion.Template.Domain.Tasks.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

/* 
 * An AGGREGATE is a cluster of associated objects that we treat 
 * as a unit for the purpose of data changes.Each AGGREGATE has a 
 * root and a boundary.The boundary defines what is inside the 
 * AGGREGATE.The root is a single, specific ENTITY contained 
 * in the AGGREGATE. 
 * 
 * It can be read simply and anyone can understand the meaning of
 * a Task and the scope of its definition just seeing this class.
 * Must be alligned with the language used by the requirements
 * (ubiqutuos language)
 */

namespace Dotnet.Onion.Template.Domain.Tasks
{
    public class Task : IAggregateRoot
    {
        public TaskId TaskId { get; set; }

        public Summary Summary { get; set; }

        public Description Description { get; set; }
    }
}
