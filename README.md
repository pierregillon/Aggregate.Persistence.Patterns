#Domain model persistence patterns

##Introduction
Building a set of pure domain objects to answer a business need is hard. When you finally succeed to create a perfect
encapsulated domain model using TDD, BDD, DDD and whatever things containing D, you could think about persistence.
Persistence in Oriented Object Programing can be hard when models are well encapsulated.

This repository contains examples of patterns you can use to fit this need of encapsulation and persistence.

##The context
The context is about an creating a very simple order management system with product, prices and quantities.
The order contract is :

    public interface IOrder
    {
        Guid Id { get; }
        double TotalCost { get; }
        DateTime? SubmitDate { get; }

        void AddProduct(Product product, int quantity);
        void RemoveProduct(Product product);
        int GetQuantity(Product product);
        void Submit();
    }

All the patterns are implementing the same business rules :
* The product add in an order requires a positive non null quantity.
* When adding an existing product, the new quantity is added to the previous one.
* When removing an existing product, the new quantity is removed to the previous one.
* The add, remove and submit operations can only be done if the order is not already submitted.
* The total amount is updated in real time, to every operations.

##Tests
A set of unit tests and integration tests are implemented for all the different patterns using XUnit. To run the integration
tests, you must publish the database project before (Patterns.Tables).

##The patterns
The patterns are :
* **[Compromise](https://github.com/pierregillon/DomainModelPersistencePatterns/tree/master/Patterns/Compromise)** 
        : oriented object compromise by exposing domain model internal states publicly to persist them.
* **Binary** : persist internal states of a domain model using binary serialization
* **State-Interface** : create a clear separation between domain model and persistence model. Copy states from one to other
thanks to interface.
* **State-Snapshot** : create a clear separation between domain model and persistence model. Persistent model is returned 
by the domain model as a Snapshot of its internal states.
* **Inner-Class** : create a clear separation between domain model and persistence model. Create a converter class within 
the domain model to access to its internal states and build persistent model from it.
* **Event-Sourcing** : the domain model state is decomposed in domain events that are persisted in the database. They are 
replayed in the domain model to restore its original state.

##Original blog
You can find the original blog about those domain model persistence patterns here : http://pierregillon.com/ .