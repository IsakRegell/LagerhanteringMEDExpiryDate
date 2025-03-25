using System;
using System.Collections.Generic;

namespace LagerhanteringMEDutgångsdatum.Models;

public partial class User
{
    public int UsersId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
