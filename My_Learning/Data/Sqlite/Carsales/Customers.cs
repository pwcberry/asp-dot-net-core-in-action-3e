using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class Customers
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Gender { get; set; }

    public int? HouseholdIncome { get; set; }

    public DateOnly Birthdate { get; set; }

    public long PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<CustomerOwnership> CustomerOwnership { get; set; } = new List<CustomerOwnership>();
}
