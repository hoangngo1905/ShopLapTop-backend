using System;
using System.Collections.Generic;

namespace ShopLapTop.Model.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public string? Address { get; set; }

    public int? Status { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
