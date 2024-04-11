using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class User: IdentityUser<int>
{
    public override int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public override string? Email { get; set; }

    public string? Resume { get; set; }

    public string? DisplayPicture { get; set; }

    public string? PortfolioUrl { get; set; }

    public bool? GetJobUpdate { get; set; }

    public string? Password { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ContactNumber> ContactNumbers { get; set; } = new List<ContactNumber>();

    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    public virtual ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

    public virtual ICollection<PreferredJobRole> PreferredJobRoles { get; set; } = new List<PreferredJobRole>();

    public virtual ICollection<ProfessionalQualification> ProfessionalQualifications { get; set; } = new List<ProfessionalQualification>();
}
