

I am sure that most of the software developers know the words of clean code, SOLID, clean architecture, hexagonal architectures, onion architectures, etc; but I suspect that most of them don’t know the truth about these amazing topics in software development. If you know them or you want to know more, please feel free to be at my side for this article series about **everything I learnt on Clean Architecture**.

I will try to explain what I learnt in **different articles** , following more or less this structure:

- **Hexagonal Architecture (Alistair Cockburn)**
- **Onion Architecture + DDD (Jeffrey Palermo)**
- **Clean Architecture (Uncle Bob)**
- **What, Why, When**
- **How to follow a Clean Architecture in .NET Core APIs**
- **Bonus track: Clean Architecture + GraphQL in .NET Core (HotChocolate)**

### Hexagonal Architecture

So **let get’s started with Hexagonal Architecture** , also known as Port and Adapters pattern (which I prefer). Even you didn’t know what this pattern means you probably used in your code.


Promoted by Alistair Cockburn in 2005 in [one of his article](http://alistair.cockburn.us/Hexagonal+Architecture)s. Regarding the age of this pattern I would like to quote **Juan Manuel Garrido de Paz** , who mentions this in his awesome[article about Ports and Adapters](https://softwarecampament.wordpress.com/portsadapters/#tc1):

> If you are thinking… “Isn’t the article too old? How is it that it is still worth it nowadays, being software development a discipline in continous evolution where new technologies and frameworks arise every day and kill the one we used yesterday?” Well the answer is in the question. Ports & Adapters is a pattern that promotes **decoupling from technology** and frameworks. So no, it isn’t too old. Good things are timeless. They are like wine, they get better as time passes by.

After this brief introduction, let’s move forward and take a look at the famous hexagonal diagram:


Following this diagram, we can see three important elements for the Ports and Adapters Pattern

- **Application** : Represented as a hexagon (it has this shape because it is easier to draw elements around it), it contains all the business logic that our application needs to execute the business rules defined in the requirements. The Ports and Adapters original article doesn’t mention anything at all and how code must be structured inside the Hexagon/Application ( [http://web.archive.org/web/20180422210157/http://alistair.cockburn.us/Hexagonal+Architecture](http://web.archive.org/web/20180422210157/http://alistair.cockburn.us/Hexagonal+Architecture)). I have seen a lot of articles talking about DDD or application layers but Alistair Cockburn didn’t write on how you should do your code, he only promoted the idea of ports and adapters which I am going to try to explain later on.
- **Drivers** (Primary Actors): External actors that interact with the application to get a response or to execute commands. In other words, the users of our application.
- **Driven Actors** (Secondary Actors): These actors are being triggered by our application. **There are two types of driven actors: repositories** (the application may obtain data from it, for example, a database) **and recipients** (the application only sends information to it, like an SMTP server)

> The difference between a primary and secondary actor lies only in who _initiates_ the conversation

So... where the hell are those ports and adapters?

### Ports

Ports live between the actors and the application core. They are part of the application, living on the edge of the hexagon and they handle the interactions with a given purpose. They are the application boundaries, or in other words, they are the **interfaces** that the application needs to communicate with external actors.

**Driver Ports** are the **API** of the application. This means that they provide the programmatic way of accessing a service to achieve a certain behaviour or output. Imagine in APIs, an interface type which is being called from the controller; that could be a driver port.

In the other hand, **Driven Ports** are the **SPI** (Service Provider Interface) of the application. SPI is the description of classes, interfaces, methods, etc that you extend and implement to achieve a goal. It is the way to extend or alter the behaviour of a software application. Trying to explain it with other words, imagine that one of our APIs sends notifications, the Driven Port is the interface type we created with that method to send the notifications.

![](https://cdn-images-1.medium.com/max/846/0*NAOQ7gFVGZMQu-TR)<figcaption>Diagram showing API and SPI in Ports and Adapters patter ( <a href="https://dev.to/julientopcu/hexagonal-architecture-decoupling-your-technical-code-from-your-business-logic-hexarch-1f3l">https://beyondxscratch.com/2017/08/19/decoupling-your-technical-code-from-your-business-logic-with-the-hexagonal-architecture-hexarch/</a>)</figcaption>

Remember, ports are technology agnostic, they don’t know if for example this notification is being sent correctly or you are spamming your boss. The driven adapter will implement this driven port and will take care of this.

### Adapters

An adapter is a software component that allows a particular technology to interact with a port of the hexagon. Adapters are outside ports level. Some people say they are outside, others say that they are like in an upper parent hexagon outside the ports so I would say they are outside ports. That’s it.

A Driver Adapter uses a Driver Port Interface, converting a specific technology request into a technology-agnostic request to a driver port. In an API, the controller is a driver adapter, receiving requests from the UI and calling methods of the driver port.

A Driven Adapter implements a Driven Port Interface, converting technology-agnostic methods of the port into specific technology methods. For example, remembering our driven port to send notifications, our Driven Adapter would implement it and using email libraries depending on our programming language will send the email.

This pattern gives you the flexibility of mocking everything and even more important, you can switch from one technology to another. For example, if you are using as a Database Driven Adapter the SQL Server libraries and you need to switch to Mongo, it shouldn’t be too hard. Ports and Adapters pattern will force you in some way to follow SOLID principles in your architecture and code, with all the benefits of using them.

![](https://cdn-images-1.medium.com/max/660/0*aiNEwjIAKGx1xiK0)<figcaption>Ports and Adapters in the diagram ( <a href="https://softwarecampament.wordpress.com/portsadapters/#tc1">https://softwarecampament.wordpress.com/portsadapters/#tc1</a>)</figcaption>

### **Summary**

I am going to quote everything from **Juan Manuel Garrido de Paz** article about Ports and Adapters because it is just perfect.

- **The Hexagon — ** the application
- Driver Port — API offered by the application
- Driven Ports — required by the application
- **Actors**  — environment devices that interact with the application
- Drivers — application users (either humans or hardware/software devices)
- Driven Actors — provide services required by the application
- **Adapters —**  to adapt specific technology to the application
- Driver Adapters — use the drivers' ports
- Driven Adapters — implement the driven ports

Also, I want to clarify one thing about the hexagon itself. Some people in other articles mentioned it and some misunderstood what Alistair Cockburn did with the Hexagon shape.

In the original article, Alistair says that the hexagon is just as it is because it is easy to highlight the inside-outside asymmetry and the nature of the ports. The term Hexagonal Architecture comes from this visual effect and it is not related to any kind of shape.

### Benefits

- **Testability**. All code must be isolated if you are following correctly this pattern, so everything can be mocked.
- **Maintainability**. We talked about it before, but if you need to extend an adapter it can be easily done because of all that great isolation we achieve the following Ports and Adapters. Same if we require to switch an adapter implementation to use another technology (aka database, SMTP or queues)
- **Identify Tech Debt**. Tech debt will be there, but following this approach, you will be able to identify code smells and measure the tech debt in terms of effort and time.

### Cons

- **Complexity**. For me, it is the only real issue here. Everyone in a team needs to be aligned to make this kind of architecture work. If that doesn’t happen the code and the architecture will become a mess without the possibility of getting any of the previously mentioned benefits.


### References

- [http://alistair.cockburn.us/Hexagonal+architecture](http://alistair.cockburn.us/Hexagonal+architecture) (Must)
- [http://web.archive.org/web/20180528132445/http://alistair.cockburn.us/Hexagonal+Architecture](http://web.archive.org/web/20180528132445/http://alistair.cockburn.us/Hexagonal+Architecture)
- [http://wiki.c2.com/?HexagonalArchitecture](http://wiki.c2.com/?HexagonalArchitecture)
- [http://wiki.c2.com/?PortsAndAdaptersArchitecture](http://wiki.c2.com/?PortsAndAdaptersArchitecture)
- [https://our-academy.org/posts/arquitectura-hexagonal](https://our-academy.org/posts/arquitectura-hexagonal) (Spanish)
- [https://softwarecampament.wordpress.com/portsadapters](https://softwarecampament.wordpress.com/portsadapters/#tc1) (Must)
- [https://www.youtube.com/watch?v=fGaJHEgonKg](https://www.youtube.com/watch?v=fGaJHEgonKg) (Video from Marcus Biel)
- [https://www.youtube.com/watch?v=th4AgBcrEHA](https://www.youtube.com/watch?v=th4AgBcrEHA) (Video)
- [https://beyondxscratch.com/2017/08/19/decoupling-your-technical-code-from-your-business-logic-with-the-hexagonal-architecture-hexarch/](https://dev.to/julientopcu/hexagonal-architecture-decoupling-your-technical-code-from-your-business-logic-hexarch-1f3l)
- [https://codely.tv/blog/screencasts/arquitectura-hexagonal-ddd/](https://codely.tv/blog/screencasts/arquitectura-hexagonal-ddd/) (Spanish)
- [http://mussabsharif.blogspot.com/2011/08/api-vs-spi.html](http://mussabsharif.blogspot.com/2011/08/api-vs-spi.html)
- [https://blog.ndepend.com/hexagonal-architecture/](https://blog.ndepend.com/hexagonal-architecture/)
- [http://www.dossier-andreas.net/software\_architecture/ports\_and\_adapters.html](http://www.dossier-andreas.net/software_architecture/ports_and_adapters.html)
- [https://hackernoon.com/hexagonal-architecture-with-kotlin-ktor-and-guice-f1b68fbdf2d9](https://hackernoon.com/hexagonal-architecture-with-kotlin-ktor-and-guice-f1b68fbdf2d9)
- [https://stackoverflow.com/questions/2954372/difference-between-spi-and-api](https://stackoverflow.com/questions/2954372/difference-between-spi-and-api)
- [http://thinkmicroservices.com/blog/2018/hexagonal-architecture.html](http://thinkmicroservices.com/blog/2018/hexagonal-architecture.html)