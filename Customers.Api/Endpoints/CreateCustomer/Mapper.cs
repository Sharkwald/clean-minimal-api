using Customers.Api.Domain;
using Customers.Api.Domain.Common;

namespace Customers.Api.Endpoints.CreateCustomer;

public static class Mapper
{
    public static Customer ToCustomer(this CreateCustomerRequest request)
    {
        return new Customer
        {
            Id = CustomerId.From(Guid.NewGuid()),
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            FullName = FullName.From(request.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
        };
    }
}
