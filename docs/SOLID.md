# SOLID Principles

Code examples of the SOLID principles.

- [Single Responsibility Principle](#single-responsibility-principle)
- [Open for Extension Closed for Modification](#open-for-extension-closed-for-modification)
- [Liskov Substitution Principle](#liskov-substitution-principle)
- [Interface Segregation Principle](#interface-segregation-principle)
- [Dependency Inversion](#dependency-inversion)


#### Single Responsibility Principle

> A class should take care of a Single Responsibility.

- Code example [Srp.cs](Solid/SingleResponsibility/Srp.cs)

##### Bad Way

This Add method does too much, it shouldnt know how to write to the log and add a customer.

```c#
class Customer
{

    void Add(Database db)
    {
        try
        {
            db.Add();
        }
        catch (Exception ex)
        {
            File.WriteAllText(@"C:\Error.txt", ex.ToString());
        }
    }
}
```

##### Good Way

Good Way, not violating the single responsibility principle.
Now we abstract the logger, so its writing the error.

```c#
class Customer
{
    private FileLogger logger = new FileLogger();
    void Add(Database db)
    {
        try {
            db.Add();
        }
        catch (Exception ex)
        {
            logger.Handle(ex.ToString());
        }
    }
}
class FileLogger
{
    void Handle(string error)
    {
        File.WriteAllText(@"C:\Error.txt", error);
    }
}
```

#### Open for Extension Closed for Modification

> Prefer extension over modification.

- Code example [Ocp.cs](Solid/OpenClosed/Ocp.cs)

##### Bad Way

Violating the Open Closed Principle
This is bad, because at the moment, there are 2 types
of customer, if we want to add another customer type
we have to add a `if else` below. So Modifying the existing code

```c#
class Customer
{
    int Type;

    void Add(Database db)
    {
        if (Type == 0)
        {
            db.Add();
        }
        else
        {
            db.AddExistingCustomer();
        }
    }
}
```

##### Good Way

This is better, because we structure the code so its
easier to extend and hard to modify


```c#
class CustomerBetter
{
    void Add(Database db)
    {
        db.Add();
    }
}

class ExistingCustomer : CustomerBetter
{
    override void Add(Database db)
    {
        db.AddExistingCustomer();
    }
}

class AnotherCustomer : CustomerBetter
{
    override void Add(Database db)
    {
        db.AnotherExtension();
    }
}
```

#### Liskov Substitution Principle

> Parent class should be able to refer child objects seamlessly during runtime polymorphism.

- Code example [Lsp.cs](Solid/Liskov/Lsp.cs)

##### Bad Way

BAD: Violating Liskov substitution principle
The parent should easily the replace the child object and not break any functionality, only lose some.
e.g. here, we don't want this to add an enquiry so we have to throw
a new exception, that is violating the principle

```c#
class Enquiry : Customer
{
    override int Discount(int sales)
    {
        return sales * 5;
    }

    override void Add(Database db)
    {
        throw new Exception("Not allowed");
    }
}

class BetterGoldCustomer : Customer
{
    override int Discount(int sales)
    {
        return sales - 100;
    }
}

class BetterSilverCustomer : Customer
{
    override int Discount(int sales)
    {
        return sales - 50;
    }
}

// e.g. to show how this is bad:
class ViolatingLiskovs
{
    void ParseCustomers()
    {
        var database = new Database();
        var customers = new List<Customer>
        {
            new GoldCustomer(),
            new SilverCustomer(),
            new Enquiry() // This is valid, but...
        };

        foreach (Customer c in customers)
        {
            // Enquiry.Add() will throw an exception here!
            c.Add(database);
        }
    }
}
```

##### Good Way

```c#
interface IDiscount {
    int Discount(int sales);
}

interface IDatabase {
    void Add(Database db);
}

internal class Customer : IDiscount, IDatabase
{
    int Discount(int sales) { return sales; }
    void Add(Database db) { db.Add(); }
}

// GOOD: Now, we don't violate Liskov Substitution principle
class AdheringToLiskovs
{
    public void ParseCustomers()
    {
        var database = new Database();
        var customers = new List<Customer>
        {
            new GoldCustomer(),
            new SilverCustomer(),
            new Enquiry() // This will give a compiler error, rather than runtime error
        };

        foreach (Customer c in customers)
        {
            // Enquiry.Add() will throw an exception here!
            // But, we won't get to here as compiler will complain
            c.Add(database);
        }
    }
}
```

#### Interface Segregation Principle

> Client should not be forced to use an interface, if it doesn't need it.

- Code example [Isp.cs](Solid/InterfaceSegregation/Isp.cs)

##### Bad Way

If we want to add more functionality, don't add to existing
interfaces, segregate them out.

```c#
interface ICustomer // existing
{
    void Add();
}

interface ICustomerImproved
{
    void Add();
    void Read(); // Existing Functionality, BAD
}
```

##### Good Way

Just create another interface, that a class can ALSO extend from

```c#
interface ICustomerV1 : ICustomer
{
    void Read();
}

class CustomerWithRead : ICustomer, ICustomerV1
{
    void Add()
    {
        var customer = new Customer();
        customer.Add(new Database());
    }

    void Read()
    {
        // GOOD: New functionality here!
    }
}

// e.g.
void ManipulateCustomers()
{
    var database = new Database();
    var customer = new Customer();
    customer.Add(database); // Old functionality, works fine
    var readCustomer = new CustomerWithRead();
    readCustomer.Read(); // Good! New functionalty is separate from existing customers
}
```

#### Dependency Inversion

> High level modules should not depend on low-level modules, but should depend on abstraction.

- Code example [Dip.cs](Solid/DependencyInversion/Dip.cs)

##### Bad Way

Bad: We are relying on the customer to say that we
are using a File Logger, rather than another type of
logger, e.g. EmailLogger.

```c#
class FileLogger
{
    void Handle(string error)
    {
        File.WriteAllText(@"C:\Error.txt", error);
    }
}

internal class Customer
{
    FileLogger logger = new FileLogger(); // Bad

    public void Add(Database db)
    {
        try
        {
            db.Add();
        }
        catch (Exception error)
        {
            logger.Handle(error.ToString());
        }
    }
}
```

##### Good Way

Good: We pass in a Logger interface to the customer
so it doesnt know what type of logger it is

```c#
class BetterCustomer
{
    ILogger logger;
    BetterCustomer(ILogger logger)
    {
        this.logger = logger;
    }

    void Add(Database db)
    {
        try
        {
            db.Add();
        }
        catch (Exception error)
        {
            logger.Handle(error.ToString());
        }
    }
}
class EmailLogger : ILogger
{
    void Handle(string error)
    {
        File.WriteAllText(@"C:\Error.txt", error);
    }
}

interface ILogger
{
    void Handle(string error);
}

// e.g. when it is used:
void UseDependencyInjectionForLogger()
{
    var customer = new BetterCustomer(new EmailLogger());
    customer.Add(new Database());
}
```