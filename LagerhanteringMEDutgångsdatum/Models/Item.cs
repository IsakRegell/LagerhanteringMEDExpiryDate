using System;
using System.Collections.Generic;

namespace LagerhanteringMEDutgångsdatum.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime ExpiryDate { get; set; }

    public int CategoryId { get; set; }

    public int UserId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
