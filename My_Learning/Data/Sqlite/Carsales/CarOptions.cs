using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class CarOptions
{
    public int OptionSetId { get; set; }

    public int? ModelId { get; set; }

    public int EngineId { get; set; }

    public int TransmissionId { get; set; }

    public int ChassisId { get; set; }

    public int? PremiumSoundId { get; set; }

    public string Color { get; set; } = null!;

    public int OptionSetPrice { get; set; }

    public virtual ICollection<CarVins> CarVins { get; set; } = new List<CarVins>();

    public virtual CarParts Chassis { get; set; } = null!;

    public virtual CarParts Engine { get; set; } = null!;

    public virtual Models? Model { get; set; }

    public virtual CarParts? PremiumSound { get; set; }

    public virtual CarParts Transmission { get; set; } = null!;
}
