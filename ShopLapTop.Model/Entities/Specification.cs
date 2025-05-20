using System;
using System.Collections.Generic;

namespace ShopLapTop.Model.Entities;

public partial class Specification
{
    public int Id { get; set; }

    public string? Cpu { get; set; }

    public string? Ram { get; set; }

    public string? GraphicsCard { get; set; }

    public string? OperatingSystem { get; set; }

    public string? Dimensions { get; set; }

    public string? Weight { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
