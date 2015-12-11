#Compromise pattern

In some projects, we don't model a complex business domain. We just need some few classes to represent some simple
concepts. In those cases, we can do a compromise on the Oriented Object Programing : merge domain model and persistent 
model into a single class. In order to map directly data from database into the domain model, we expose its internal 
state through public properties.

For purist, this compromise is not a long term solution. It depends on the size of the application. Don't systematize
this kind of approach, use it sparingly. You need to know its advantages/disadvantages before to use it.

##Advantages
* Simplification of the persistent layer.
* Single model for modeling the domain and the persistent aspects.

##Disadvantages
* Internal states are mutable and can be modified from outside of the domain model, without evaluating business rules
in object methods.
* Our domain model is coupled to the database structure and corrupted.
* Object Relational Mapping tools are corrupting the domain model with metadata or syntax requirement, that are needed 
to persist it.

##Conclusion
Be careful.