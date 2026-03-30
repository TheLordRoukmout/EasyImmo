using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class Event
{
    public int IdEvent { get; set; }

    public int IdTypeEvent { get; set; }

    public int IdEstate { get; set; }

    public DateTime DateEvent { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<ClientEvent> ClientEvents { get; set; } = new List<ClientEvent>();

    public virtual RealEstate IdEstateNavigation { get; set; } = null!;

    public virtual TypeEvent IdTypeEventNavigation { get; set; } = null!;
}
