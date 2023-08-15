using Customers.Api.Endpoints.Common;

namespace Customers.Api.Endpoints.GetAllCustomers;

public class GetAllCustomersResponse
{
    public IEnumerable<CustomerResponse> Customers { get; init; } = Enumerable.Empty<CustomerResponse>();
}
