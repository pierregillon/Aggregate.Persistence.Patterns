#State interface:

When your domain is complex, you want to be able to model it without any technical constraints. To do that, you can 
separate your domain models, containing the business logic, from your persistent models, containing data with persistence 
constraints (data type, format, orm metadata, ...).

At a moment you need to convert a domain model to a persistence model, and vis versa. Regarding the encapsulation level 
of your domain models, you can't easily copy external data to its internal states. 

The **state-interface pattern** let you copy these information through an **interface** that is implemented in both domain model 
and persistent model and contains shared data. 

The copy can be implemented with .net reflection.

##Advantages
* Domain model keep its full encapsulation and is not corrupted.
* Separation of concerns between domain model and persistent model.
* Object Relational Mapping metadata are implemented in the persistent model.

##Disadvantages
* An interface should define a behaviour. Use it to hold shared data between domain model and persistent model is not very clean.
* The explicit implementation of the interface, in the domain model, add plumbing code near the business rules.

##Conclusion
**State-interface** is a good pattern to separate domain model and persistent model and to keep a pure domain modeling.