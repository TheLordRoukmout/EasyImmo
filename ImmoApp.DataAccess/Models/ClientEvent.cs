using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class ClientEvent
{
    public int IdClient { get; set; }

    public int IdEvent { get; set; }

    public string? RoleInEvent { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Event IdEventNavigation { get; set; } = null!;
}
