using Customers.Api.Endpoints.Common;
using FastEndpoints;

namespace Customers.Api.Endpoints.UpdateCustomer;

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
