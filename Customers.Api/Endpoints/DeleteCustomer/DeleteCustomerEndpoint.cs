using Customers.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.DeleteCustomer;

[HttpDelete("customers/{id:guid}"), AllowAnonymous]
public class DeleteCustomerEndpoint : Endpoint<DeleteCustomerRequest, Results<NoContent, NotFound, StatusCodeHttpResult>>
{
    private readonly ICustomerService _customerService;

    public DeleteCustomerEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task<Results<NoContent, NotFound, StatusCodeHttpResult>> ExecuteAsync(DeleteCustomerRequest req,
        CancellationToken ct)
    {
        var deletionResult = await _customerService.DeleteAsync(req.Id, ct);
        
        return deletionResult.Match<Results<NoContent, NotFound, StatusCodeHttpResult>>(
            _ => TypedResults.NoContent(),
            error => error switch 
            {
                ErrorResult.NotFound => TypedResults.NotFound(),
                ErrorResult.Unauthorized => TypedResults.NotFound(), // avoid different response as existence test
                _ => TypedResults.StatusCode(500)
            }
        );
    }
}
