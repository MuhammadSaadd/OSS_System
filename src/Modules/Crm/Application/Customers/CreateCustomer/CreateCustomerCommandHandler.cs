using Crm.Application.Abstractions;
using Crm.Domain;
using MediatR;
using SharedKernel;

namespace Crm.Application.Customers.CreateCustomer;

public sealed class CreateCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<CreateCustomerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var createResult = Customer.Create(request.Name, request.Email, request.Address);

        if (createResult.IsFailure)
            return Result.Failure<Guid>(createResult.Error!);

        await repository.AddAsync(createResult.Value, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return Result.Success(createResult.Value.Id);
    }
}
