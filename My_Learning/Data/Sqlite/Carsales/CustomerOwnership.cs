using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class CustomerOwnership
{
    public int CustomerId { get; set; }

    public int Vin { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public int PurchasePrice { get; set; }

    public DateOnly? WaranteeExpireDate { get; set; }

    public int DealerId { get; set; }

    public virtual Customers Customer { get; set; } = null!;

    public virtual Dealers Dealer { get; set; } = null!;

    public virtual CarVins VinNavigation { get; set; } = null!;
}
