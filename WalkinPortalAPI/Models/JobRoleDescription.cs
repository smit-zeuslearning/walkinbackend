using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class JobRoleDescription
{
    public int Id { get; set; }

    public int GrossPackage { get; set; }

    public string RoleDescription { get; set; } = null!;

    public string Requirements { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<JobPost> JobPostInstructionalDesignDescriptions { get; set; } = new List<JobPost>();

    public virtual ICollection<JobPost> JobPostSoftwareEngineerDescriptions { get; set; } = new List<JobPost>();

    public virtual ICollection<JobPost> JobPostSoftwareQualityEngineerDesriptions { get; set; } = new List<JobPost>();
}
