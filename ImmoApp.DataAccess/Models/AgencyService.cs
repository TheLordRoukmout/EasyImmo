using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class AgencyService
{
    public int IdService { get; set; }

    public int IdTypeService { get; set; }

    public int IdClient { get; set; }

    public int? IdAgent { get; set; }

    public string? Description { get; set; }

    public DateTime? DateService { get; set; }

    public decimal? Price { get; set; }

    public virtual Agent? IdAgentNavigation { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual TypeService IdTypeServiceNavigation { get; set; } = null!;
}
