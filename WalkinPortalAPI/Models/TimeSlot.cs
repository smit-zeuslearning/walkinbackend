using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class TimeSlot
{
    public int Id { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public int JobPostId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModefiedDate { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual JobPost JobPost { get; set; } = null!;
}
