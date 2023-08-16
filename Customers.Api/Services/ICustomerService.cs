using Customers.Api.Domain;

namespace Customers.Api.Services;

public interface ICustomerService
{
    Task<Result<SuccessResult, ErrorResult>> CreateAsync(Customer customer, CancellationToken ct);

    Task<Result<Customer, ErrorResult>> GetAsync(Guid id, CancellationToken ct);

    Task<Result<IList<Customer>, ErrorResult>> GetAllAsync(CancellationToken ct);

    Task<Result<SuccessResult, ErrorResult>> UpdateAsync(Customer customer, CancellationToken ct);

    Task<Result<SuccessResult, ErrorResult>> DeleteAsync(Guid id, CancellationToken ct);
}
