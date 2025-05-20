using System;
using System.Collections.Generic;

namespace ShopLapTop.Model.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public double? Discount { get; set; }

    public string? Description { get; set; }

    public string? Thumbnail { get; set; }

    public int? Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int? ManufacturerId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Specification> Specifications { get; set; } = new List<Specification>();

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
