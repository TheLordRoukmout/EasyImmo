using System;
using System.Collections.Generic;

namespace ImmoApp.DataAccess.Models;

public partial class UserProg
{
    public int IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Role { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
}
