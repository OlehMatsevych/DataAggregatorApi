using DataAggregator.Domain.Entities.Base;

namespace DataAggregator.Domain.Entities.SpecificCustomers;

public partial class Customer145 : CustomerBase<string>
{
    public string UserId { get; set; } = null!;

    public string? Name { get; set; }

    public override string Email { get; set; } = null!;

    public string? Password { get; set; }

    public override string Id { get => UserId; set => UserId = value; }

    public override string FirstName => Name?.Split(' ')[0] ?? "";

    public override string LastName => Name?.Split(' ').Last() ?? "";
}
