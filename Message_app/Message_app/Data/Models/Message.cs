using System;
using System.Collections.Generic;

namespace Message_app.Data.Models;

public partial class Message
{
    public long MessageId { get; set; }

    public long LandlordId { get; set; }

    public long TenantId { get; set; }

    public string message { get; set; } = null!;

    public DateTime DateMessge { get; set; }
}
