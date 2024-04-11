using System;
using System.Collections.Generic;

namespace WalkinPortalAPI.Models;

public partial class JobApplication
{
    public int Id { get; set; }

    public byte[] UpdatedResume { get; set; } = null!;

    public int SelectedTimeSlotId { get; set; }

    public int SelectedPreferenceId { get; set; }

    public int UsersId { get; set; }

    public int JobPostId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual JobPost JobPost { get; set; } = null!;

    public virtual PreferredJobRole SelectedPreference { get; set; } = null!;

    public virtual TimeSlot SelectedTimeSlot { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
