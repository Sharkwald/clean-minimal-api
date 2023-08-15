using Customers.Api.Services;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.GetAllCustomers;

[HttpGet("customers"), AllowAnonymous]
public class GetAllCustomersEndpoint : EndpointWithoutRequest<Results<Ok<GetAllCustomersResponse>, BadRequest>>
{
    private readonly ICustomerService _customerService;

    public GetAllCustomersEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task<Results<Ok<GetAllCustomersResponse>, BadRequest>> ExecuteAsync(CancellationToken ct)
    {
        try
        {
            var customers = await _customerService.GetAllAsync();
            var customersResponse = customers.ToCustomersResponse();
            return TypedResults.Ok(customersResponse);
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            return TypedResults.BadRequest();
        }
    }
}
