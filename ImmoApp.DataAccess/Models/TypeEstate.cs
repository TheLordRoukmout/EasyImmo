using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class TypeEstate
{
    public int IdTypeEstate { get; set; }

    public string Label { get; set; } = null!;

    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}
