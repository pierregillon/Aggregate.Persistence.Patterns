# Aggregate persistence patterns

## Introduction
Building a set of pure domain objects to answer a business need is hard. When you finally succeed to create a perfectly
encapsulated aggregate using [**Domain Driven Design**](https://en.wikipedia.org/wiki/Domain-driven_design), 
you could think it's done. But, what about persistence ? Persistence in *Object-Oriented Programing* can be hard when models 
are well encapsulated.

This repository contains examples of patterns you can use to fit this need of **encapsulation and persistence**.

## The patterns
The patterns are :
* **[Compromise](/Patterns/Aggregate.Persistence.Compromise/)** 
: oriented object compromise by exposing domain model internal states publicly to persist them.
* **[Binary](/Patterns/Aggregate.Persistence.Binary)** 
: persist internal states of an domain model using binary serialization
* **[State-Interface](/Patterns/Aggregate.Persistence.StateInterface/)**
: create a clear separation between domain model and persistence model. Copy states from one to other
thanks to interface.
* **[State-Snapshot](/Patterns/Aggregate.Persistence.StateSnapshot/)**
: create a clear separation between domain model and persistence model. Persistent model is returned 
by the domain model as a *Snapshot* of its internal states.
* **[Inner-Class](/Patterns/Aggregate.Persistence.InnerClass/)**
: create a clear separation between domain model and persistence model. Create a converter class within 
the domain model to access to its internal states and build persistent model from it.
* **[Event-Sourcing](/Patterns/Aggregate.Persistence.EventSourcing/)**
: the domain model state is decomposed in domain events that are persisted in the database. They are 
replayed in the domain model to restore its original state.

## A simple domain
To illustrate aggregate persistence patterns, we will use a simple domain : an order management system with product, prices and quantities.
The order contract is :

```csharp
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
```

All the patterns are implementing the same business rules :
* The product add in an order requires a positive non null quantity.
* When adding an existing product, the new quantity is added to the previous one.
* When removing an existing product, the new quantity is removed to the previous one.
* The add, remove and submit operations can only be done if the order is not already submitted.
* The total amount is updated in real time, to every operations.

## Tests
A set of unit tests and integration tests are implemented for all the different patterns using XUnit. To run the integration
tests, you must publish the database project before (Patterns.Tables).

## Original blog
You can find the original blog about those domain model persistence patterns [**here**](http://pierregillon.com/persister-un-pur-domain-model-pas-si-simple/).

Enjoy !
