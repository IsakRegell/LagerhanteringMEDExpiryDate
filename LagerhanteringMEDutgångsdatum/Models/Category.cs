using System;
using System.Collections.Generic;

namespace LagerhanteringMEDutgångsdatum.Models;

public partial class Category
{
    public int CategorieId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
