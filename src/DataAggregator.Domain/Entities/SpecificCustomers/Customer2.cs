using DataAggregator.Domain.Entities.Base;

namespace DataAggregator.Domain.Entities.SpecificCustomers;

public partial class Customer2 : CustomerBase<int>
{
    public override int Id { get; set; }

    public string? GivenName { get; set; }

    public string? FamilyName { get; set; }

    public string? JobPosition { get; set; }

    public override string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public override string FirstName => GivenName ?? "";

    public override string LastName => FamilyName ?? "";
}
