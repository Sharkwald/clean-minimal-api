namespace Customers.Api.Actions.GetCustomer;

using Common;
using FastEndpoints;

public class GetCustomerSummary : Summary<GetCustomerEndpoint>
{
    public GetCustomerSummary()
    {
        Summary = "Returns a single customer by id";
        Description = "Returns a single customer by id";
        Response<CustomerResponse>(200, "Successfully found and returned the customer");
        Response(404, "The customer does not exist in the system");
    }
}
