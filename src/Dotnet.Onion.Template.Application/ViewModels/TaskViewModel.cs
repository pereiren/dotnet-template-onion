using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

/*
 * A view model represents the data that you want to display on 
 * your view/page, whether it be used for static text or for input
 * values (like textboxes and dropdown lists). It is something 
 * different than your domain model. It is a model for the view. 
 */
namespace Dotnet.Onion.Template.Application.ViewModels
{
    public class TaskViewModel
    {
        public TaskViewModel()
        {

        }
        public TaskViewModel(Guid taskId, string summary, string description)
        {
            Id = taskId.ToString();
            Summary = summary;
            Description = description;
        }
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(150)]
        public string Description { get; set; }

        [MaxLength(1500)]
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; } = "";
    }


}
