using SharedKernel;

namespace Crm.Domain;

public sealed class Customer : AggregateRoot
{
    private Customer()
    {
    }

    public string Name { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public CustomerStatus Status { get; private set; }

    public static Result<Customer> Create(string name, string email, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Customer>("Customer name is required.");

        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Customer>("Customer email is required.");

        if (string.IsNullOrWhiteSpace(address))
            return Result.Failure<Customer>("Customer address is required.");

        var customer = new Customer
        {
            Name = name.Trim(),
            Email = email.Trim(),
            Address = address.Trim(),
            Status = CustomerStatus.Active
        };

        return Result.Success(customer);
    }

    public Result Deactivate()
    {
        if (Status == CustomerStatus.Inactive)
            return Result.Failure("Customer is already inactive.");

        Status = CustomerStatus.Inactive;
        return Result.Success();
    }
}
