using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class RealEstate
{
    public int IdEstate { get; set; }

    public string Reference { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Address { get; set; } = null!;

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public decimal? Surface { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreationDate { get; set; }

    public int IdTypeEstate { get; set; }

    public int? IdOwner { get; set; }

    public virtual ICollection<EstateStatusHistory> EstateStatusHistories { get; set; } = new List<EstateStatusHistory>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Client? IdOwnerNavigation { get; set; }

    public virtual TypeEstate IdTypeEstateNavigation { get; set; } = null!;
}
