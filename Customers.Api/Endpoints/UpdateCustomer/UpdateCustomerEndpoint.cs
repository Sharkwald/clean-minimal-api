using Customers.Api.Endpoints.Common;
using Customers.Api.Services;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.UpdateCustomer;

[HttpPut("customers/{id:guid}"), AllowAnonymous]
public class UpdateCustomerEndpoint : Endpoint<UpdateCustomerRequest, Results<Ok<CustomerResponse>, NotFound, BadRequest>>
{
    private readonly ICustomerService _customerService;

    public UpdateCustomerEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task<Results<Ok<CustomerResponse>, NotFound, BadRequest>> ExecuteAsync(UpdateCustomerRequest req, CancellationToken ct)
    {
        try
        {
            var existingCustomer = await _customerService.GetAsync(req.Id);

            if (existingCustomer is null)
            {
                return TypedResults.NotFound();
            }

            var customer = req.ToCustomer();
            await _customerService.UpdateAsync(customer);

            var customerResponse = customer.ToCustomerResponse();
            return TypedResults.Ok(customerResponse);
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            return TypedResults.BadRequest();
        }
    }
}
