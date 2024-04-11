using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class Address
{
    public int Id { get; set; }

    public string HouseNo { get; set; } = null!;

    public string Apartment { get; set; } = null!;

    public string? Landmark { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int Zipcode { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
}
