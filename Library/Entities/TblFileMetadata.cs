using System;
using System.Collections.Generic;

namespace WebApi.Testing;

public partial class TblFileMetadata
{
    public int Id { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public DateTime? UploadDate { get; set; }

    public string? PolicyId { get; set; }

    public virtual TblPolicy? Policy { get; set; }
}
