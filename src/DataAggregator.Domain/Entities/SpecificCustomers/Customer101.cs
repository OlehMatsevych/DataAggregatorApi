using DataAggregator.Domain.Entities.Base;

namespace DataAggregator.Domain.Entities.SpecificCustomers;

public partial class Customer101 : CustomerBase<int>
{
    public override int Id { get; set; }

    public override string FirstName { get; } = null!;

    public override string LastName { get; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public override string Email { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string? Salutation { get; set; }

    public string? PasswordHash { get; set; }

    public DateTime? LastLoginDate { get; set; }
}
