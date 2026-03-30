using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class Agent
{
    public int IdAgent { get; set; }

    public int IdUser { get; set; }

    public DateOnly? HireDate { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<AgencyService> AgencyServices { get; set; } = new List<AgencyService>();

    public virtual UserProg IdUserNavigation { get; set; } = null!;
}
