using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Carsales;

public partial class Models
{
    public int ModelId { get; set; }

    public string ModelName { get; set; } = null!;

    public int ModelBasePrice { get; set; }

    public int BrandId { get; set; }

    public virtual Brands Brand { get; set; } = null!;

    public virtual ICollection<CarOptions> CarOptions { get; set; } = new List<CarOptions>();

    public virtual ICollection<CarVins> CarVins { get; set; } = new List<CarVins>();
}
