# dotnet-template-onion

<p align="center">
  <img src="images/logo.jpg" alt="dotnet-template-onion logo" width="400"/>
</p>

A .NET/.NET Core template to use Onion Architecture and DDD (Domain Driven Design) with CQRS and ES with a simple example on how to use all this architecture together from the Controller until the Repository class using Domain objects and different patterns.

### Documentation

You can find information about this template in:

- [Main Architecture](docs/ARCHITECTURE.md)
- [Hexagonal Architecture](docs/HEXAGONAL.md)
- [DDD](docs/DDD.md)
- [CQRS AND ES](docs/CQRS-ES.md)
- [SOLID](docs/SOLID.md)

### Prerequisites

#### .NET 6

Ensure you have the correct dotnet-core SDK installed for your system:

https://dotnet.microsoft.com/download/dotnet/6.0

This is just the version used by the template, if you need to use a newer or older one, you can do it manually after.

### Usage

1. Clone this repository
2. To allow the api to be created you will need to install the template from nuget:

```
dotnet new -i {{Path_where_you_cloned_the_repository}}
```

- Example:

```
dotnet new -i C:\GitLocal\dotnet-template-onion
```

3. To check that the template has been installed successfully:

```
dotnet new -l
```

- There should now be a new template **dotnet_template_onion**

```
Templates                                          Short Name                 Language          Tags
----------------------------------------------------------------------------------------------------------
.NET Core 6.0 Template with CQRS, ES and DDD       dotnet_template_onion      [C#]              Web/API/Microservices
```

4. Create the .Net Core Solution

```
dotnet new dotnet_template_onion -n {{Namespace_of_your_project}} -o <outputFolder>
```

- This will create the folder containing a solution and project folder.
  ![](images/installation.jpg)

And you are ready to go, you can use Visual Studio, Visual Studio Code or any other IDE to proceed with your coding.

### Structure of the template

```
C:.
│   .gitignore
│   Dotnet.Onion.Template.sln
│   README.md
│
├───docs
│       ARCHITECTURE.md
│       CQRS-ES.md
│       DDD.md
│       HEXAGONAL.md
│       SOLID.md
│
├───images
│       dotnet-onion-ddd-cqrs-es.jpg
│
├───src
│   ├───Dotnet.Onion.Template.API
│   │   │   .dockerignore
│   │   │   Dockerfile
│   │   │   Dotnet.Onion.Template.API.csproj
│   │   │   Program.cs
│   │   │   Startup.cs
│   │   │
│   │   ├───Bindings
│   │   ├───Config
│   │   │       appsettings-dev.json
│   │   │       appsettings-int.json
│   │   │       appsettings-prod.json
│   │   │       appsettings-stag.json
│   │   │
│   │   ├───Controllers
│   │   │       TasksController.cs
│   │   │
│   │   ├───Extensions
│   │   │   └───Middleware
│   │   │           ErrorDetails.cs
│   │   │           ExceptionMiddleware.cs
│   │   │
│   │   └───Properties
│   │           launchSettings.json
│   │
│   ├───Dotnet.Onion.Template.Application
│   │   │   Dotnet.Onion.Template.Application.csproj
│   │   │
│   │   ├───Handlers
│   │   │       TaskCommandHandler.cs
│   │   │       TaskEventHandler.cs
│   │   │
│   │   ├───Mappers
│   │   │       TaskViewModelMapper.cs
│   │   │
│   │   ├───Services
│   │   │       ITaskService.cs
│   │   │       TaskService.cs
│   │   │
│   │   └───ViewModels
│   │           TaskViewModel.cs
│   │
│   ├───Dotnet.Onion.Template.Domain
│   │   │   Dotnet.Onion.Template.Domain.csproj
│   │   │   IAggregateRoot.cs
│   │   │   IRepository.cs
│   │   │
│   │   └───Tasks
│   │       │   ITaskFactory.cs
│   │       │   ITaskRepository.cs
│   │       │   Task.cs
│   │       │
│   │       ├───Commands
│   │       │       CreateNewTaskCommand.cs
│   │       │       DeleteTaskCommand.cs
│   │       │       TaskCommand.cs
│   │       │
│   │       ├───Events
│   │       │       TaskCreatedEvent.cs
│   │       │       TaskDeletedEvent.cs
│   │       │       TaskEvent.cs
│   │       │
│   │       └───ValueObjects
│   │               Description.cs
│   │               Summary.cs
│   │               TaskId.cs
│   │
│   └───Dotnet.Onion.Template.Infrastructure
│       │   Dotnet.Onion.Template.Infrastructure.csproj
│       │
│       ├───Factories
│       │       EntityFactory.cs
│       │       TaskFactory.cs
│       │
│       └───Repositories
│               TaskRepository.cs
│
└───tests
    └───Dotnet.Onion.Template.Tests
        │   Dotnet.Onion.Template.Tests.csproj
        │
        └───UnitTests
            ├───Application
            │   └───Services
            │           TaskServiceTests.cs
            │
            └───Helpers
                    HttpContextHelper.cs
                    TaskHelper.cs
                    TaskViewModelHelper.cs



```
