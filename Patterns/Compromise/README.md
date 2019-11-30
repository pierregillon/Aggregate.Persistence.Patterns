# The *Compromise* pattern

In some projects, we don't model a complex business domain. We just need some few classes to implement CRUD application.
In those cases, we can do a compromise on the *Oriented Object Programing* : **merge the domain model and the persistent 
model into a single class**. In order to map directly data from database into the domain model, we expose its internal 
state through public properties.

For purist, this compromise is not a long term solution. It depends on the size of the application. Don't systematize
this kind of approach, use it sparingly. You need to know its advantages/disadvantages before to use it.

## Advantages
* Single model for the domain and the persistence.
* No mapping required
* Thin abstraction layers

## Disadvantages
* Internal states are *mutable* and can **be modified from outside of the domain model**, without evaluating business rules
in object methods.
* Our domain model is **coupled to the database structure**
* *Object Relational Mapping* (ORM) tools **are corrupting the domain model with metadata or syntax requirement**

## Conclusion
Use it for **CRUD application**, but not when business rules are complex to model.