using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class Brands
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public virtual ICollection<Models> Models { get; set; } = new List<Models>();

    public virtual ICollection<Dealers> Dealer { get; set; } = new List<Dealers>();
}
