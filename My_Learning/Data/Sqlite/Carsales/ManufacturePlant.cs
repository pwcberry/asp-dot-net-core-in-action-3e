using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class ManufacturePlant
{
    public int ManufacturePlantId { get; set; }

    public string PlantName { get; set; } = null!;

    public string? PlantType { get; set; }

    public string? PlantLocation { get; set; }

    public int? CompanyOwned { get; set; }

    public virtual ICollection<CarParts> CarParts { get; set; } = new List<CarParts>();

    public virtual ICollection<CarVins> CarVins { get; set; } = new List<CarVins>();
}
