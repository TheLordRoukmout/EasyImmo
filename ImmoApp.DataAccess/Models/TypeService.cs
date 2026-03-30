using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class TypeService
{
    public int IdTypeService { get; set; }

    public string Label { get; set; } = null!;

    public virtual ICollection<AgencyService> AgencyServices { get; set; } = new List<AgencyService>();
}
