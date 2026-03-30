using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class TypeStatusOffer
{
    public int IdStatusOffer { get; set; }

    public string Label { get; set; } = null!;

    public virtual ICollection<EstateStatusHistory> EstateStatusHistories { get; set; } = new List<EstateStatusHistory>();
}
