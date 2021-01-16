using Dotnet.Onion.Template.Domain.Tasks;
using Dotnet.Onion.Template.Domain.Tasks.Commands;
using Dotnet.Onion.Template.Domain.Tasks.Events;
using FluentMediator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Onion.Template.Application.Handlers
{
    public class TaskCommandHandler
    {
        private readonly ITaskFactory _taskFactory;
        private readonly ITaskRepository _taskRepository;
        private readonly IMediator _mediator;

        public TaskCommandHandler(ITaskRepository taskRepository, ITaskFactory taskFactory, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _taskFactory = taskFactory;
            _mediator = mediator;
        }

        public async Task<Domain.Tasks.Task> HandleNewTask(CreateNewTaskCommand createNewTaskCommand)
        {
            var task = _taskFactory.CreateTaskInstance(
                summary: new Domain.Tasks.ValueObjects.Summary(createNewTaskCommand.Summary),
                description: new Domain.Tasks.ValueObjects.Description(createNewTaskCommand.Description)
            );

            var taskCreated = await _taskRepository.Add(task);

            // You may raise an event in case you need to propagate this change to other microservices
            await _mediator.PublishAsync(new TaskCreatedEvent(taskCreated.TaskId.ToGuid(),
                taskCreated.Description.ToString(), taskCreated.Summary.ToString()));

            return taskCreated;
        }

        public async System.Threading.Tasks.Task HandleDeleteTask(DeleteTaskCommand deleteTaskCommand)
        {
            await _taskRepository.Remove(deleteTaskCommand.Id);

            // You may raise an event in case you need to propagate this change to other microservices
            await _mediator.PublishAsync(new TaskDeletedEvent(deleteTaskCommand.Id));
        }
    }
}
