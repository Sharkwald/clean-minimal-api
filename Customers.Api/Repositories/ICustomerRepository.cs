using Customers.Api.Contracts;

namespace Customers.Api.Repositories;

public interface ICustomerRepository
{
    Task<bool> CreateAsync(CustomerDto customer, CancellationToken ct);

    Task<CustomerDto?> GetAsync(Guid id, CancellationToken ct);

    Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken ct);

    Task<bool> UpdateAsync(CustomerDto customer, CancellationToken ct);

    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}
