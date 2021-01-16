using Dotnet.Onion.Template.Application.ViewModels;
using Dotnet.Onion.Template.Domain.Tasks;
using Dotnet.Onion.Template.Domain.Tasks.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

/*
 * A view model represents the data that you want to display on 
 * your view/page, whether it be used for static text or for input
 * values (like textboxes and dropdown lists). It is something 
 * different than your domain model. So we need to convert the 
 * domain model to a view model to send it to the client (API response)
 * 
 * This is an example of the mapping, you can use whatever you want in
 * your code, Automapper or any similar library to do this conversion.
 */

namespace Dotnet.Onion.Template.Application.Mappers
{
    public class TaskViewModelMapper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public TaskViewModelMapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<TaskViewModel> ConstructFromListOfEntities(IEnumerable<Task> tasks)
        {
            var tasksViewModel = tasks.Select(i => new TaskViewModel
            {
                Id = i.TaskId.ToGuid().ToString(),
                Description = i.Description.ToString(),
                Summary = i.Summary.ToString()
            }
            );

            return tasksViewModel;
        }

        public TaskViewModel ConstructFromEntity(Task task)
        {
            return new TaskViewModel
            {
                Id = task.TaskId.ToGuid().ToString(),
                Description = task.Description.ToString(),
                Summary = task.Summary.ToString(),
            };
        }

        public CreateNewTaskCommand ConvertToNewTaskCommand(TaskViewModel taskViewModel)
        {
            return new CreateNewTaskCommand(taskViewModel.Summary, taskViewModel.Description);
        }

        public DeleteTaskCommand ConvertToDeleteTaskCommand(Guid id)
        {
            return new DeleteTaskCommand(id);
        }
    }
}
