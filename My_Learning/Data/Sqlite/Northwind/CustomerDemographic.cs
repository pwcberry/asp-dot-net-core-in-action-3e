using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Northwind;

public partial class CustomerDemographic
{
    public string Id { get; set; } = null!;

    public string? CustomerDesc { get; set; }
}
