using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class EstateDocument
{
    public int IdDocument { get; set; }

    public int? IdEstate { get; set; }

    public string? DocumentName { get; set; }

    public string? DocumentPath { get; set; }

    public DateTime? UploadDate { get; set; }

    public virtual RealEstate? IdEstateNavigation { get; set; }
}
