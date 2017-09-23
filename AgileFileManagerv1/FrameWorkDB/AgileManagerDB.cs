using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace FrameWorkDB.V1
{
    public class AgileManagerDB : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AgileManager;Integrated Security=True")
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(a => a.dealer)
                .WithMany(b => b.clients)
                .HasForeignKey(a => a.DealerID)
                .HasConstraintName("FK_Clients_DealerID_Dealers");

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.department)
                .WithMany(b => b.employees)
                .HasForeignKey(a => a.DepartmentID)
                .HasConstraintName("FK_Employees_DepartmentID_Departments");

            modelBuilder.Entity<File>()
                .HasOne(a => a.client)
                .WithMany(b => b.files)
                .HasForeignKey(a => a.ClientID)
                .HasConstraintName("FK_Files_ClientID_Clients");

            modelBuilder.Entity<File>()
                .HasOne(a => a.employee)
                .WithMany(b => b.files)
                .HasForeignKey(a => a.EmployeeID)
                .HasConstraintName("FK_Files_EmployeeID_Employees");

            modelBuilder.Entity<File>()
                .HasOne(a => a.license)
                .WithMany(b => b.files)
                .HasForeignKey(a => a.LicenseID)
                .HasConstraintName("FK_Files_LicenseID_Licenses");

            modelBuilder.Entity<File>()
                .HasOne(a => a.issue)
                .WithMany(b => b.files)
                .HasForeignKey(a => a.IssueID)
                .HasConstraintName("FK_Files_IssueID_Issues");

            modelBuilder.Entity<File>()
                .HasOne(a => a.state)
                .WithMany(b => b.files)
                .HasForeignKey(a => a.StateID)
                .HasConstraintName("FK_Files_StateID_States");

            modelBuilder.Entity<File>()
                .HasOne(a => a.priority)
                .WithMany(b => b.files)
                .HasForeignKey(a => a.PriorityID)
                .HasConstraintName("FK_Files_PriorityID_Priorities");

            modelBuilder.Entity<Intervention>()
                .HasOne(a => a.file)
                .WithMany(b => b.interventions)
                .HasForeignKey(a => a.FileID)
                .HasConstraintName("FK_Interventions_FileID_Files");

            modelBuilder.Entity<Intervention>()
                .HasOne(a => a.employee)
                .WithMany(b => b.interventions)
                .HasForeignKey(a => a.EmployeeID)
                .HasConstraintName("FK_Interventions_EmployeeID_Employees");

            modelBuilder.Entity<License>()
                .HasOne(a => a.client)
                .WithMany(b => b.licenses)
                .HasForeignKey(a => a.ClientID)
                .HasConstraintName("FK_Licenses_ClientID_Clients");

            modelBuilder.Entity<License>()
                .HasOne(a => a.application)
                .WithMany(b => b.licenses)
                .HasForeignKey(a => a.ApplicationID)
                .HasConstraintName("FK_Licenses_ApplicationID_Applications");

            modelBuilder.Entity<Report>()
                .HasOne(a => a.employee)
                .WithMany(b => b.reports)
                .HasForeignKey(a => a.EmployeeID)
                .HasConstraintName("FK_Reports_EmployeeID_Employees");

            modelBuilder.Entity<Report>()
                .HasOne(a => a.file)
                .WithMany(b => b.reports)
                .HasForeignKey(a => a.FileID)
                .HasConstraintName("FK_Reports_FileID_Files");
        }
    }
}
