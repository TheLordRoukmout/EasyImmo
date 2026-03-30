using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class TypeEvent
{
    public int IdTypeEvent { get; set; }

    public string Label { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
