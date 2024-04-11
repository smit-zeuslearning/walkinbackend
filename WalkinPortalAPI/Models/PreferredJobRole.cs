using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class PreferredJobRole
{
    public int Id { get; set; }

    public bool? InstructionalDesigner { get; set; }

    public bool? SoftwareEnginner { get; set; }

    public bool? SoftwareQualityEngineer { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    //public virtual User User { get; set; } = null!;
}
