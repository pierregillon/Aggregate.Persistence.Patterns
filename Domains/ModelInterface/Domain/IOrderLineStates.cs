using Domain.Base;

namespace Domains.ModelInterface.Domain
{
    public interface IOrderLineStates
    {
        Product Product { get; set; }
        int Quantity { get; set; }
    }
}