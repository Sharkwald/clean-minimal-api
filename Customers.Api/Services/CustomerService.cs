using Customers.Api.Contracts;
using Customers.Api.Domain;
using Customers.Api.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace Customers.Api.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<SuccessResult,ErrorResult>> CreateAsync(Customer customer, CancellationToken ct)
    {
        try
        {
            var existingUser = await _customerRepository.GetAsync(customer.Id.Value, ct);
            if (existingUser is not null)
            {
                var message = $"A user with id {customer.Id} already exists";
                throw new ValidationException(message, new[]
                {
                    new ValidationFailure(nameof(Customer), message)
                });
            }

            var customerDto = customer.ToCustomerDto();
            var success = await _customerRepository.CreateAsync(customerDto, ct);
            return success ? SuccessResult.Success : ErrorResult.UnexpectedError;
        }
        catch (Exception ex)
        {
            //TODO: Log error.
            return ErrorResult.UnexpectedError;
        }
    }

    public async Task<Result<Customer,ErrorResult>> GetAsync(Guid id, CancellationToken ct)
    {
        try
        {
            var customerDto = await _customerRepository.GetAsync(id, ct);
            if (customerDto == default(CustomerDto)) return ErrorResult.NotFound;
            return customerDto?.ToCustomer();
        }
        catch (Exception ex)
        {
            //TODO: Log error.
            return ErrorResult.UnexpectedError;
        }
    }

    public async Task<Result<IList<Customer>, ErrorResult>> GetAllAsync(CancellationToken ct)
    {
        try
        {
            var customerDtos = await _customerRepository.GetAllAsync(ct);
            var customers = customerDtos.Select(x => x.ToCustomer()).ToList();
            return customers;
        }
        catch (Exception ex)
        {
            //TODO: Log error.
            return ErrorResult.UnexpectedError;
        }
    }

    public async Task<Result<SuccessResult, ErrorResult>> UpdateAsync(Customer customer, CancellationToken ct)
    {
        try
        {
            var customerDto = customer.ToCustomerDto();
            var success = await _customerRepository.UpdateAsync(customerDto, ct);
            return success ? SuccessResult.Success : ErrorResult.NotFound;
        }
        catch (Exception ex)
        {
            //TODO: Log error.
            return ErrorResult.UnexpectedError;
        }
    }

    public async Task<Result<SuccessResult, ErrorResult>> DeleteAsync(Guid id, CancellationToken ct)
    {
        try
        {
            var success = await _customerRepository.DeleteAsync(id, ct);
            return success ? SuccessResult.Success : ErrorResult.NotFound;
        }
        catch (Exception ex)
        {
            //TODO: Log error.
            return ErrorResult.UnexpectedError;
        }
    }
}
