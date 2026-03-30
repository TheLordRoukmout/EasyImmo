using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? TypeClient { get; set; }

    public virtual ICollection<AgencyService> AgencyServices { get; set; } = new List<AgencyService>();

    public virtual ICollection<ClientEvent> ClientEvents { get; set; } = new List<ClientEvent>();

    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}
