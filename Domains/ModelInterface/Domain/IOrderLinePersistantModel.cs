using ClassLibrary1;

namespace Domains.ModelInterface.Domain
{
    public interface IOrderLinePersistantModel
    {
        Product Product { get; set; }
        int Quantity { get; set; }
    }
}