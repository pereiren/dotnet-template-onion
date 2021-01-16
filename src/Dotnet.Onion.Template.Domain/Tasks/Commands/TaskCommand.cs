using System;

namespace Dotnet.Onion.Template.Domain.Tasks.Commands
{
    public class TaskCommand
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }
    }
}
