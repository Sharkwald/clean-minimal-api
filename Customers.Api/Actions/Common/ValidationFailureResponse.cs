namespace Customers.Api.Actions.Common;

public class ValidationFailureResponse
{
    public List<string> Errors { get; init; } = new();
}
