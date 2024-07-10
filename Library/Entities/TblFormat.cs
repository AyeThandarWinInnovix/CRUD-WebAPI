using System;
using System.Collections.Generic;

namespace WebApi.Testing;

public partial class TblFormat
{
    public int Id { get; set; }

    public string? FormatName { get; set; }

    public int? Length { get; set; }

    public int? CurrentValue { get; set; }

    public DateTime? FormattingAt { get; set; }

    public DateTime? CreatedAt { get; set; }
}
