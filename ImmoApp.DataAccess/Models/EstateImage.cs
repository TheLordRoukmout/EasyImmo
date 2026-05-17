using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class EstateImage
{
    public int IdImage { get; set; }

    public int? IdEstate { get; set; }

    public string? ImagePath { get; set; }

    public bool? IsMain { get; set; }

    public virtual RealEstate? IdEstateNavigation { get; set; }
}
