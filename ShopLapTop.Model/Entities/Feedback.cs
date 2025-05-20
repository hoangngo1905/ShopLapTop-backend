using System;
using System.Collections.Generic;

namespace ShopLapTop.Model.Entities;

public partial class Feedback
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Status { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
