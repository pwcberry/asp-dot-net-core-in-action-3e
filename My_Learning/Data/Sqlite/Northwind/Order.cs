using System;
using System.Collections.Generic;

namespace MyLearning.Data.Sqlite.Northwind;

public partial class Order
{
    public int Id { get; set; }

    public string? CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? RequiredDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    public int? ShipVia { get; set; }

    public decimal Freight { get; set; }

    public string? ShipName { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }
}
