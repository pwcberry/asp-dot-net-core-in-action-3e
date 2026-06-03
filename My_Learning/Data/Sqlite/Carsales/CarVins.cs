using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class CarVins
{
    public int Vin { get; set; }

    public int ModelId { get; set; }

    public int OptionSetId { get; set; }

    public DateOnly ManufacturedDate { get; set; }

    public int ManufacturedPlantId { get; set; }

    public virtual ICollection<CustomerOwnership> CustomerOwnership { get; set; } = new List<CustomerOwnership>();

    public virtual ManufacturePlant ManufacturedPlant { get; set; } = null!;

    public virtual Models Model { get; set; } = null!;

    public virtual CarOptions OptionSet { get; set; } = null!;
}
