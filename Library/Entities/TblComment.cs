using System;
using System.Collections.Generic;

namespace WebApi;

public partial class TblComment
{
    public int CommentId { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual TblPost Post { get; set; } = null!;

    public virtual TblUser User { get; set; } = null!;
}
