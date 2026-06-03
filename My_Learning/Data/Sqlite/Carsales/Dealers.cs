using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class Dealers
{
    public int DealerId { get; set; }

    public string DealerName { get; set; } = null!;

    public string? DealerAddress { get; set; }

    public virtual ICollection<CustomerOwnership> CustomerOwnership { get; set; } = new List<CustomerOwnership>();

    public virtual ICollection<Brands> Brand { get; set; } = new List<Brands>();
}
