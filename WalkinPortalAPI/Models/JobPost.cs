using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class JobPost
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int AddressId { get; set; }

    public int InstructionalDesignDescriptionId { get; set; }

    public int SoftwareEngineerDescriptionId { get; set; }

    public int SoftwareQualityEngineerDesriptionId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual JobRoleDescription InstructionalDesignDescription { get; set; } = null!;

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual JobRoleDescription SoftwareEngineerDescription { get; set; } = null!;

    public virtual JobRoleDescription SoftwareQualityEngineerDesription { get; set; } = null!;

    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
