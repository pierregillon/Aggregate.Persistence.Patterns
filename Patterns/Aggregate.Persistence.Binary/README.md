# The *binary serialization* pattern

This pattern consists to simply serialize the domain model or aggregate to a file.
The binary serialization convert directly to C# class to a byte array without needing to explicitly define
what field or properties you want to serialize, public or private. **Everything is saved**.

You only need to add the [Serialize] attribute on top of your class.

## Advantages
* You can design a pure domain model
* Every fields private or not is saved and restored

## Disadvantages
* **Very hard** (impossible?) to version you domain model. If you change fields or properties, the deserialization
process may failed.
* **Very hard** to query the state of the application through multiple aggregates as you need to deserialize everything.

## Conclusion
This pattern can be useful in very little project but became hard to maintain on large ones, even if domain models
are perfectly encapsulated.