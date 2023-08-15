using Customers.Api.Services;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Customers.Api.Endpoints.DeleteCustomer;

[HttpDelete("customers/{id:guid}"), AllowAnonymous]
public class DeleteCustomerEndpoint : Endpoint<DeleteCustomerRequest, Results<NoContent, BadRequest, NotFound>>
{
    private readonly ICustomerService _customerService;

    public DeleteCustomerEndpoint(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public override async Task<Results<NoContent, BadRequest, NotFound>> ExecuteAsync(DeleteCustomerRequest req, CancellationToken ct)
    {
        try
        {
            var deleted = await _customerService.DeleteAsync(req.Id);
            if (!deleted)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            return TypedResults.BadRequest();
        }
    }
}
