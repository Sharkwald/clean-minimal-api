using Customers.Api.Services;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.GetAllCustomers;

[HttpGet("customers"), AllowAnonymous]
public class GetAllCustomersEndpoint : EndpointWithoutRequest<Results<Ok<GetAllCustomersResponse>, StatusCodeHttpResult>>
{
    private readonly ICustomerService _customerService;

    public GetAllCustomersEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task<Results<Ok<GetAllCustomersResponse>, StatusCodeHttpResult>> ExecuteAsync(CancellationToken ct)
    {
        var customersResult = await _customerService.GetAllAsync(ct);
        return customersResult.Match<Results<Ok<GetAllCustomersResponse>, StatusCodeHttpResult>>(
            customers => TypedResults.Ok(customers.ToCustomersResponse()),
            _ => TypedResults.StatusCode(500)
        );
    }
}
