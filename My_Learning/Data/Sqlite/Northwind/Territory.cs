using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Northwind;

public partial class Territory
{
    public string Id { get; set; } = null!;

    public string? TerritoryDescription { get; set; }

    public int RegionId { get; set; }
}
