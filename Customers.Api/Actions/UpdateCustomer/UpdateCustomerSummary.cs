namespace Customers.Api.Actions.UpdateCustomer;

using Common;
using FastEndpoints;

public class UpdateCustomerSummary : Summary<UpdateCustomerEndpoint>
{
    public UpdateCustomerSummary()
    {
        Summary = "Updates an existing customer in the system";
        Description = "Updates an existing customer in the system";
        Response<CustomerResponse>(201, "Customer was successfully updated");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
    }
}
