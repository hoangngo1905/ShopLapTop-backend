using System;
using System.Collections.Generic;

namespace ShopLapTop.Model.Entities;

public partial class Order
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Phonenumber { get; set; }

    public string? Email { get; set; }

    public string? Note { get; set; }

    public string? Address { get; set; }

    public DateTime? Orderdate { get; set; }

    public int? Status { get; set; }

    public decimal? Total { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
