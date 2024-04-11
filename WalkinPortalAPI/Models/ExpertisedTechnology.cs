using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class ExpertisedTechnology
{
    public int Id { get; set; }

    public bool? Javascript { get; set; }

    public bool? Angularjs { get; set; }

    public bool? Reactjs { get; set; }

    public bool? Nodejs { get; set; }

    public string? Other { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int ProfessionalQualificationId { get; set; }

    //public virtual ProfessionalQualification ProfessionalQualification { get; set; } = null!;
}
