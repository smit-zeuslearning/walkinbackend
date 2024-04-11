using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class ProfessionalQualification
{
    public int Id { get; set; }

    public string? ApplicationType { get; set; }

    public int TotalExperience { get; set; }

    public bool OnOnticePeriod { get; set; }

    public DateTime LastWorkingDate { get; set; }

    public int? TerminationNoticeMonths { get; set; }

    public bool ZeusTestLast12months { get; set; }

    public string? AppledRoldLast12months { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<ExpertisedTechnology> ExpertisedTechnologies { get; set; } = new List<ExpertisedTechnology>();

    public virtual ICollection<FamalierTechnology> FamalierTechnologies { get; set; } = new List<FamalierTechnology>();

    //public virtual User User { get; set; } = null!;
}
