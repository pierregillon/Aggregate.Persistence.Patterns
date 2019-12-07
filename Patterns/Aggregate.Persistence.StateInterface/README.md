# The *State-interface* pattern

When your domain is complex, you want to be able to model it without any technical constraints.
To do that, you can separate your **domain models / aggregates**, containing the business logic, 
from your **persistent models**, representing the data with persistence constraints
(data type, format, [**ORM**](https://en.wikipedia.org/wiki/Object-relational_mapping) metadata, ...)

At one point, **you will be forced to map those 2 concepts** from and to. 

The ***state-interface pattern*** let you copy these information through an *interface*,
containing the shared data and implemented in both domain model and persistent. 

The copy process can be implemented through .Net reflection, Automapper, ...

## Advantages
* Domain model keep its full encapsulation and is not corrupted with persistence concerns.
* Free to design you model completely differently from persistence.

## Disadvantages
* An interface should normally define a ***behaviour***. Using it to hold shared data between 
domain model and persistent model is not very clean.
* The explicit implementation of the interface, in the domain model, add plumbing code near 
the business logics.