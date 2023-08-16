using Customers.Api.Endpoints.Common;
using Customers.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.GetCustomer;

[HttpGet("customers/{id:guid}"), AllowAnonymous]
public class GetCustomerEndpoint : Endpoint<GetCustomerRequest, Results<Ok<CustomerResponse>, NotFound, StatusCodeHttpResult>>
{
    private readonly ICustomerService _customerService;

    public GetCustomerEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task<Results<Ok<CustomerResponse>, NotFound, StatusCodeHttpResult>> ExecuteAsync(GetCustomerRequest req,
        CancellationToken ct)
    {
        var customerResult = await _customerService.GetAsync(req.Id, ct);

        return customerResult.Match<Results<Ok<CustomerResponse>, NotFound, StatusCodeHttpResult>>(
            (customer) => TypedResults.Ok(customer.ToCustomerResponse()),
            (error) => error switch
            {
                ErrorResult.NotFound => TypedResults.NotFound(),
                ErrorResult.Unauthorized => TypedResults.NotFound(), // avoid different response as existence test
                _ => TypedResults.StatusCode(500)
            }
        );
    }
}
