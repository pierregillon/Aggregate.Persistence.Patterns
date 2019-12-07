# The *Event Sourcing* pattern

As it is described in the litterature, the event sourcing is a different way to represent data.

>Event Sourcing ensures that all changes to application state are stored as a sequence of events. --**Martin Fowler**

Instead of persisting the state of our Domain Model or Aggregate, the main idea here is **to capture every 
change of the state of an application in an event object**. For example here, we'll find a *OrderPlaced*, *OrderSubmitted*, *ProductAdded*, ...


## Advantages
* Domain model is **pure**
* **Events natively emerged when designing domain models** and are used to communicate with other bounding contexts.
* Business rules are **retroactive**
* Events **natively historized** what happen in the system

## Disadvantages
* **Hard to explore the events** that happened in the system as they often are stored in a denormalized way (JSON)
* Events are stored in a **different database (write optimized) that the state we want to display**. So we need to
* synchronize events and state projections, which add complexity.

## Conclusion
This pattern surely have a cost, but it creates a clear separation between domain model and persistent model(s).
Business rules and domain models are not corrupted by infrastructure constraints (Metadata, Attributes, ...)