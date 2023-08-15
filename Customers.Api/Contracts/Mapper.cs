using Customers.Api.Domain;
using Customers.Api.Domain.Common;

namespace Customers.Api.Contracts;

public static class Mapper
{
    public static CustomerDto ToCustomerDto(this Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id.Value.ToString(),
            Email = customer.Email.Value,
            Username = customer.Username.Value,
            FullName = customer.FullName.Value,
            DateOfBirth = customer.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)
        };
    }

    public static Customer ToCustomer(this CustomerDto customerDto)
    {
        return new Customer
        {
            Id = CustomerId.From(Guid.Parse(customerDto.Id)),
            Email = EmailAddress.From(customerDto.Email),
            Username = Username.From(customerDto.Username),
            FullName = FullName.From(customerDto.FullName),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(customerDto.DateOfBirth))
        };
    }
}
