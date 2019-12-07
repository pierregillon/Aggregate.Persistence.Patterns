namespace Patterns.StateInterface.Infrastructure.Mapping
{
    public interface IMapper<TDomainModel, TPersistentModel>
        where TDomainModel : class
        where TPersistentModel : class
    {
        TDomainModel ToDomainModel(TPersistentModel persistentModel);
        TPersistentModel ToPersistentModel(TDomainModel domainModel);
    }
}