using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalkinPortalAPI.Models;

public partial class Education
{
    public int Id { get; set; }

    public int AggregatePercentage { get; set; }

    public int PassingYear { get; set; }

    public string? Qualification { get; set; }

    public string? EducationStream { get; set; }

    public string? CollegeName { get; set; }

    public string? CollegeLocation { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int UserId { get; set; }
}
