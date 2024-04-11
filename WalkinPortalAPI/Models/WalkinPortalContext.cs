using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WalkinPortalAPI.Models;

public partial class WalkinPortalContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public WalkinPortalContext()
    {
    }

    public WalkinPortalContext(DbContextOptions<WalkinPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<ContactNumber> ContactNumbers { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<ExpertisedTechnology> ExpertisedTechnologies { get; set; }

    public virtual DbSet<FamalierTechnology> FamalierTechnologies { get; set; }

    public virtual DbSet<JobApplication> JobApplications { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    public virtual DbSet<JobRoleDescription> JobRoleDescriptions { get; set; }

    public virtual DbSet<PreferredJobRole> PreferredJobRoles { get; set; }

    public virtual DbSet<ProfessionalQualification> ProfessionalQualifications { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=password;database=walkin_portal", ServerVersion.AutoDetect("server=localhost;port=3306;user=root;password=password;database=walkin_portal"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apartment)
                .HasMaxLength(255)
                .HasColumnName("apartment");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.HouseNo)
                .HasMaxLength(255)
                .HasColumnName("house_no");
            entity.Property(e => e.Landmark)
                .HasMaxLength(255)
                .HasDefaultValueSql("'N/A'")
                .HasColumnName("landmark");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("state");
            entity.Property(e => e.Zipcode).HasColumnName("zipcode");
        });

        modelBuilder.Entity<ContactNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contact_number");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryCode).HasColumnName("country_code");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            //entity.HasOne(d => d.User).WithMany(p => p.ContactNumbers)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("contact_number_ibfk_1");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("education");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AggregatePercentage).HasColumnName("aggregate_percentage");
            entity.Property(e => e.CollegeLocation)
                .HasMaxLength(255)
                .HasColumnName("college_location");
            entity.Property(e => e.CollegeName)
                .HasMaxLength(255)
                .HasColumnName("college_name");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.EducationStream)
                .HasMaxLength(255)
                .HasColumnName("education_stream");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.PassingYear).HasColumnName("passing_year");
            entity.Property(e => e.Qualification)
                .HasMaxLength(255)
                .HasColumnName("qualification");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            //entity.HasOne(d => d.User).WithMany(p => p.Educations)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("education_ibfk_1");
        });

        modelBuilder.Entity<ExpertisedTechnology>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("expertised_technologies");

            entity.HasIndex(e => e.ProfessionalQualificationId, "professional_qualification_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Angularjs)
                .HasDefaultValueSql("'0'")
                .HasColumnName("angularjs");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Javascript)
                .HasDefaultValueSql("'0'")
                .HasColumnName("javascript");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Nodejs)
                .HasDefaultValueSql("'0'")
                .HasColumnName("nodejs");
            entity.Property(e => e.Other)
                .HasMaxLength(50)
                .HasDefaultValueSql("'N/A'")
                .HasColumnName("other");
            entity.Property(e => e.ProfessionalQualificationId).HasColumnName("professional_qualification_id");
            entity.Property(e => e.Reactjs)
                .HasDefaultValueSql("'0'")
                .HasColumnName("reactjs");

            //entity.HasOne(d => d.ProfessionalQualification).WithMany(p => p.ExpertisedTechnologies)
            //    .HasForeignKey(d => d.ProfessionalQualificationId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("expertised_technologies_ibfk_1");
        });

        modelBuilder.Entity<FamalierTechnology>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("famalier_technologies");

            entity.HasIndex(e => e.ProfessionalQualificationId, "professional_qualification_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Angularjs)
                .HasDefaultValueSql("'0'")
                .HasColumnName("angularjs");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Javascript)
                .HasDefaultValueSql("'0'")
                .HasColumnName("javascript");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Nodejs)
                .HasDefaultValueSql("'0'")
                .HasColumnName("nodejs");
            entity.Property(e => e.Other)
                .HasMaxLength(50)
                .HasDefaultValueSql("'N/A'")
                .HasColumnName("other");
            entity.Property(e => e.ProfessionalQualificationId).HasColumnName("professional_qualification_id");
            entity.Property(e => e.Reactjs)
                .HasDefaultValueSql("'0'")
                .HasColumnName("reactjs");

            //entity.HasOne(d => d.ProfessionalQualification).WithMany(p => p.FamalierTechnologies)
            //    .HasForeignKey(d => d.ProfessionalQualificationId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("famalier_technologies_ibfk_1");
        });

        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job_application");

            entity.HasIndex(e => e.JobPostId, "job_post_id");

            entity.HasIndex(e => e.SelectedPreferenceId, "selected_preference_id");

            entity.HasIndex(e => e.SelectedTimeSlotId, "selected_time_slot_id");

            entity.HasIndex(e => e.UsersId, "users_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.JobPostId).HasColumnName("job_post_id");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.SelectedPreferenceId).HasColumnName("selected_preference_id");
            entity.Property(e => e.SelectedTimeSlotId).HasColumnName("selected_time_slot_id");
            entity.Property(e => e.UpdatedResume)
                .HasColumnType("blob")
                .HasColumnName("updated_resume");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.JobPost).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.JobPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_application_ibfk_3");

            entity.HasOne(d => d.SelectedPreference).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.SelectedPreferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_application_ibfk_1");

            entity.HasOne(d => d.SelectedTimeSlot).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.SelectedTimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_application_ibfk_4");

            entity.HasOne(d => d.Users).WithMany(p => p.JobApplications)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_application_ibfk_2");
        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job_post");

            entity.HasIndex(e => e.AddressId, "address_id");

            entity.HasIndex(e => e.InstructionalDesignDescriptionId, "instructional_design_description_id");

            entity.HasIndex(e => e.SoftwareEngineerDescriptionId, "software_engineer_description_id");

            entity.HasIndex(e => e.SoftwareQualityEngineerDesriptionId, "software_quality_engineer_desription_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.InstructionalDesignDescriptionId).HasColumnName("instructional_design_description_id");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.SoftwareEngineerDescriptionId).HasColumnName("software_engineer_description_id");
            entity.Property(e => e.SoftwareQualityEngineerDesriptionId).HasColumnName("software_quality_engineer_desription_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Address).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_post_ibfk_1");

            entity.HasOne(d => d.InstructionalDesignDescription).WithMany(p => p.JobPostInstructionalDesignDescriptions)
                .HasForeignKey(d => d.InstructionalDesignDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_post_ibfk_2");

            entity.HasOne(d => d.SoftwareEngineerDescription).WithMany(p => p.JobPostSoftwareEngineerDescriptions)
                .HasForeignKey(d => d.SoftwareEngineerDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_post_ibfk_3");

            entity.HasOne(d => d.SoftwareQualityEngineerDesription).WithMany(p => p.JobPostSoftwareQualityEngineerDesriptions)
                .HasForeignKey(d => d.SoftwareQualityEngineerDesriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_post_ibfk_4");
        });

        modelBuilder.Entity<JobRoleDescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job_role_description");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.GrossPackage).HasColumnName("gross_package");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Requirements)
                .HasColumnType("text")
                .HasColumnName("requirements");
            entity.Property(e => e.RoleDescription)
                .HasColumnType("text")
                .HasColumnName("role_description");
        });

        modelBuilder.Entity<PreferredJobRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("preferred_job_role");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.InstructionalDesigner)
                .HasDefaultValueSql("'0'")
                .HasColumnName("instructional_designer");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.SoftwareEnginner)
                .HasDefaultValueSql("'0'")
                .HasColumnName("software_enginner");
            entity.Property(e => e.SoftwareQualityEngineer)
                .HasDefaultValueSql("'0'")
                .HasColumnName("software_quality_engineer");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            //entity.HasOne(d => d.User).WithMany(p => p.PreferredJobRoles)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("preferred_job_role_ibfk_1");
        });

        modelBuilder.Entity<ProfessionalQualification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("professional_qualification");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppledRoldLast12months)
                .HasMaxLength(50)
                .HasDefaultValueSql("'N/A'")
                .HasColumnName("appled_rold_last_12months");
            entity.Property(e => e.ApplicationType)
                .HasMaxLength(50)
                .HasColumnName("application_type");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.LastWorkingDate)
                .HasColumnType("date")
                .HasColumnName("last_working_date");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.OnOnticePeriod).HasColumnName("on_ontice_period");
            entity.Property(e => e.TerminationNoticeMonths).HasColumnName("termination_notice_months");
            entity.Property(e => e.TotalExperience).HasColumnName("total_experience");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ZeusTestLast12months).HasColumnName("zeus_test_last_12months");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("time_slots");

            entity.HasIndex(e => e.JobPostId, "job_post_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.EndTime)
                .HasMaxLength(30)
                .HasColumnName("end_time");
            entity.Property(e => e.JobPostId).HasColumnName("job_post_id");
            entity.Property(e => e.ModefiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modefied_date");
            entity.Property(e => e.StartTime)
                .HasMaxLength(30)
                .HasColumnName("start_time");

            entity.HasOne(d => d.JobPost).WithMany(p => p.TimeSlots)
                .HasForeignKey(d => d.JobPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("time_slots_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.DisplayPicture).HasColumnName("display_picture");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.GetJobUpdate)
                .HasDefaultValueSql("'0'")
                .HasColumnName("get_job_update");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.PortfolioUrl)
                .HasMaxLength(255)
                .HasColumnName("portfolio_url");
            entity.Property(e => e.Resume).HasColumnName("resume");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
