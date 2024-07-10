using System;
using System.Collections.Generic;

namespace WebApi.Testing;

public partial class TblPolicy
{
    public int Id { get; set; }

    public string? PolicyId { get; set; }

    public string? PolicyHolderName { get; set; }

    public DateTime? PolicyStartDate { get; set; }

    public DateTime? PolicyEndDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
