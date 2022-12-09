using System;
using System.Collections.Generic;

namespace Calendar.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Calendar> Calendars { get; } = new List<Calendar>();
}
