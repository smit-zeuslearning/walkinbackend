using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WalkinPortalAPI.Models;

public partial class ContactNumber
{
    public int Id { get; set; }

    public int CountryCode { get; set; }

    public long PhoneNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int UserId { get; set; }
}
