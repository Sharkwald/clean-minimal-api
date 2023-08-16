using Customers.Api.Endpoints.Common;
using Customers.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.CreateCustomer;

public class
    CreateCustomerEndpoint : Endpoint<CreateCustomerRequest, Results<CreatedAtRoute<CustomerResponse>, StatusCodeHttpResult>>
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

    public override async Task<Results<CreatedAtRoute<CustomerResponse>, StatusCodeHttpResult>> ExecuteAsync(
        CreateCustomerRequest req, CancellationToken ct)
    {
        var customer = req.ToCustomer();

        var creationResult = await _customerService.CreateAsync(customer, ct);

        return creationResult.Match<Results<CreatedAtRoute<CustomerResponse>, StatusCodeHttpResult>>(
            _ =>
            {
                var customerResponse = customer.ToCustomerResponse();
                return TypedResults.CreatedAtRoute(customerResponse, "GetCustomer", new { ID = customer.Id.Value });
            },
            _ => TypedResults.StatusCode(500)
        );
    }
}
