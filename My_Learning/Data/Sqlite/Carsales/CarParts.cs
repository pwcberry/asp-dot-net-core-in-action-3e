using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class CarParts
{
    public int PartId { get; set; }

    public string PartName { get; set; } = null!;

    public int ManufacturePlantId { get; set; }

    public DateOnly ManufactureStartDate { get; set; }

    public DateOnly? ManufactureEndDate { get; set; }

    public int? PartRecall { get; set; }

    public virtual ICollection<CarOptions> CarOptionsChassis { get; set; } = new List<CarOptions>();

    public virtual ICollection<CarOptions> CarOptionsEngine { get; set; } = new List<CarOptions>();

    public virtual ICollection<CarOptions> CarOptionsPremiumSound { get; set; } = new List<CarOptions>();

    public virtual ICollection<CarOptions> CarOptionsTransmission { get; set; } = new List<CarOptions>();

    public virtual ManufacturePlant ManufacturePlant { get; set; } = null!;
}
