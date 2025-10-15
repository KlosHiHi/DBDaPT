using System;
using System.Collections.Generic;

namespace LabWork11.Model;

public partial class DeletedVisitor
{
    public int VisitorId { get; set; }

    public string Phone { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Email { get; set; }

    public DateTime DeletedDate { get; set; }

    public string Login { get; set; } = null!;
}
