The following patterns are known to describe business solutions.

- [Value Object](#value-object)
- [Entity](#entity)
- [Aggregate Root](#aggregate-root)
- [Repository](#repository)
- [Use Case](#use-case)
- [Bounded Context](#bounded-context)
- [Entity Factory](#entity-factory)
- [Domain Service](#domain-service)
- [Application Service](#application-service)

### Value Object

Encapsulate the tiny domain business rules. Structures that are unique by their properties and the whole object is immutable, once it is created its state can not change.

```c#
public readonly struct Name
{
    private readonly string _text;

    public Name(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new NameShouldNotBeEmptyException("The 'Name' field is required");

        _text = text;
    }

    public override string ToString()
    {
        return _text;
    }
}
```

Rules of thumb:

1. The developer should make the Value Object serializable and deserializable.
1. A Value Object can not reference an Entity or another mutable object.

### Entity

Highly abstract and mutable objects unique identified by its IDs.

Rules of Thumb:

1. Entities are mutable.
1. Entities are highly abstract.
1. Entities do not need to be serializable.
1. The entity state should be encapsulated to external access.

### Aggregate Root

Similar to Entities with the addition that Aggregate Root are responsible to keep the graph of objects consistent.

1. Owns entities object graph.
1. Ensure the child entities state are always consistent.
1. Define the transaction scope.

```c#
    public class Task : IAggregateRoot
    {
        public TaskId TaskId { get; set; }

        public Summary Summary { get; set; }

        public Description Description { get; set; }
    }
```

Rules of thumb:

1. Protect business invariants inside Aggregate boundaries.
1. Design small Aggregates.
1. Reference other Aggregates by identity only.
1. Update other Aggregates using eventual consistency. (Event Sourcing)

### Repository

Provides persistence capabilities to Aggregate Roots.

```c#
public class TaskRepository : ITaskRepository
    {
        private readonly ITaskFactory _taskFactory;

        public TaskRepository(ITaskFactory taskFactory)
        {
            _taskFactory = taskFactory;
        }

        public Task<Domain.Tasks.Task> Add(Domain.Tasks.Task taskEntity)
        {
            //Your database access implementation
        }

        public Task<List<Domain.Tasks.Task>> FindAll()
        {
            //Your database access implementation

            return tasks;
        }

        public Task<Domain.Tasks.Task> FindById(Guid id)
        {
            //Your database access implementation
        }

        public System.Threading.Tasks.Task Remove(Guid id)
        {
            //Your database access implementation
        }
    }
```

Rules of thumb:

1. The repository is designed around the aggregate root.
1. A repository for every entity is a code smell.

### Application Service

It is the application entry point for an user interaction. It accepts an input message, executes the algorithm then it should give the output message to the Output port.

```c#
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskFactory _taskFactory;
        private readonly TaskViewModelMapper _taskViewModelMapper;
        private readonly ITracer _tracer;
        private readonly IMediator _mediator;

        public TaskService(ITaskRepository taskRepository, TaskViewModelMapper taskViewModelMapper, ITracer tracer, ITaskFactory taskFactory, IMediator mediator)
        {
            _taskRepository = taskRepository;
            _taskViewModelMapper = taskViewModelMapper;
            _tracer = tracer;
            _taskFactory = taskFactory;
            _mediator = mediator;
        }

        public async Task<TaskViewModel> Create(TaskViewModel taskViewModel)
        {
            using(var scope = _tracer.BuildSpan("Create_TaskService").StartActive(true))
            {
                var newTaskCommand = _taskViewModelMapper.ConvertToNewTaskCommand(taskViewModel);
                
                var taskResult = await _mediator.SendAsync<Domain.Tasks.Task>(newTaskCommand);

                return _taskViewModelMapper.ConstructFromEntity(taskResult);
            }
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            using (var scope = _tracer.BuildSpan("Delete_TaskService").StartActive(true))
            {
                var deleteTaskCommand = _taskViewModelMapper.ConvertToDeleteTaskCommand(id);
                await _mediator.PublishAsync(deleteTaskCommand);
            }
        }

        public async Task<IEnumerable<TaskViewModel>> GetAll()
        {
            using (var scope = _tracer.BuildSpan("GetAll_TaskService").StartActive(true))
            {
                var tasksEntities = await _taskRepository.FindAll();

                return _taskViewModelMapper.ConstructFromListOfEntities(tasksEntities);
            }
        }

        public async Task<TaskViewModel> GetById(Guid id)
        {
            using (var scope = _tracer.BuildSpan("GetById_TaskService").StartActive(true))
            {
                var taskEntity = await _taskRepository.FindById(id);

                return _taskViewModelMapper.ConstructFromEntity(taskEntity);
            }
        }
    }
```

Rules of thumb:

* The use case implementation are close to a human readable language. (Get all, get by ID, delete, create, etc)
* Invokes transaction operations (eg. Unit Of Work).

### Bounded Context

It is a logical boundary, similar to a module in a system. In this project the single Domain project is the single bounded context we designed.

### Entity Factory

Creates new instances of Entities and Aggregate Roots. Should be implemented by the Infrastructure layer.

```c#
    public class EntityFactory : ITaskFactory
    {
        public Domain.Tasks.Task CreateTaskInstance(Summary summary, Description description)
        {
            return new TaskFactory(summary, description);
        }
    }
```

