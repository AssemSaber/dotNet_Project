using System;
using System.Collections.Generic;

namespace otherServices.Data_Project.Models;

public partial class Message
{
    public long MessageId { get; set; }

    public long LandlordId { get; set; } // assume sender
     
    public long TenantId { get; set; }  // assume reciever

    public string Message1 { get; set; } = null!;

    public DateTime DateMessge { get; set; }

    public virtual User Landlord { get; set; } = null!;

    public virtual User Tenant { get; set; } = null!;
}
