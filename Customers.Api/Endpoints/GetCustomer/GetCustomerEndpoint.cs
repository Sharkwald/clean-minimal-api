using Customers.Api.Endpoints.Common;
using Customers.Api.Services;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.GetCustomer;

[HttpGet("customers/{id:guid}"), AllowAnonymous]
public class GetCustomerEndpoint : Endpoint<GetCustomerRequest, Results<Ok<CustomerResponse>, NotFound, BadRequest>>
{
    private readonly ICustomerService _customerService;

    public GetCustomerEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task<Results<Ok<CustomerResponse>, NotFound, BadRequest>> HandleAsync(GetCustomerRequest req, CancellationToken ct)
    {
        try
        {
            var customer = await _customerService.GetAsync(req.Id);

            if (customer is null)
            {
                return TypedResults.NotFound();
            }

            var customerResponse = customer.ToCustomerResponse();
            return TypedResults.Ok(customerResponse);
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            return TypedResults.BadRequest();
        }
    }
}
