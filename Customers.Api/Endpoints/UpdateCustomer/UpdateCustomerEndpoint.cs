using Customers.Api.Endpoints.Common;
using Customers.Api.Services;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.UpdateCustomer;

[HttpPut("customers/{id:guid}"), AllowAnonymous]
public class UpdateCustomerEndpoint : 
    Endpoint<UpdateCustomerRequest, Results<Ok<CustomerResponse>, NotFound, StatusCodeHttpResult>>
{
    private readonly ICustomerService _customerService;

    public UpdateCustomerEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task<Results<Ok<CustomerResponse>, NotFound, StatusCodeHttpResult>> ExecuteAsync(
        UpdateCustomerRequest req, CancellationToken ct)
    {
        var existingCustomerResult = await _customerService.GetAsync(req.Id, ct);
        if (!existingCustomerResult.IsSuccess) return TypedResults.NotFound();
        
        var updatedCustomer = req.ToCustomer();
        var updateResult = await _customerService.UpdateAsync(updatedCustomer, ct);
        
        return updateResult.Match<Results<Ok<CustomerResponse>, NotFound, StatusCodeHttpResult>>(
            _ => TypedResults.Ok(updatedCustomer.ToCustomerResponse()),
            error => error switch
            {
                ErrorResult.NotFound => TypedResults.NotFound(),
                _ => TypedResults.StatusCode(500)
            }
        );
    }
}
