using Customers.Api.Endpoints.Common;
using Customers.Api.Services;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.CreateCustomer;

public class CreateCustomerEndpoint : Endpoint<CreateCustomerRequest, Results<CreatedAtRoute<CustomerResponse>, BadRequest>>
{
    private readonly ICustomerService _customerService;

    public CreateCustomerEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override void Configure()
    {
        Post("customers");
        Description(x => x.WithName("GetCustomer"));
        AllowAnonymous();
    } 

    public override async Task<Results<CreatedAtRoute<CustomerResponse>, BadRequest>> ExecuteAsync(
        CreateCustomerRequest req, CancellationToken ct)
    {
        try
        {
            var customer = req.ToCustomer();

            await _customerService.CreateAsync(customer);

            var customerResponse = customer.ToCustomerResponse();

            return TypedResults.CreatedAtRoute(customerResponse, "GetCustomer", new { ID = customer.Id.Value });
        }
        catch (Exception ex) when (ex is not ValidationException) 
        {
            return TypedResults.BadRequest();
        }
    }
}
