using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class EstateStatusHistory
{
    public int IdHistory { get; set; }

    public int IdEstate { get; set; }

    public int IdStatusOffer { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public virtual RealEstate IdEstateNavigation { get; set; } = null!;

    public virtual TypeStatusOffer IdStatusOfferNavigation { get; set; } = null!;
}
