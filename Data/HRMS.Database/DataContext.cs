using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using HRMS.Database.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace HRMS.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDimissionInfo> EmployeeDimissions { get; set; }
        public DbSet<EmployeeEntryInfo> EmployeeEntries { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet <PositionTransferCrossDepart> TransferCrossDeparts { get; set; }
        public DbSet<PositionTransferInnerDepart> TransferInnerDeparts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<File> Files { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Attendance>().ToTable("Attendance");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<EmployeeDimissionInfo>().ToTable("EmployeeDimission");
            modelBuilder.Entity<EmployeeEntryInfo>().ToTable("EmployeeEntry");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<PositionTransferCrossDepart>().ToTable("TransferCrossDepart");
            modelBuilder.Entity<PositionTransferInnerDepart>().ToTable("TransferInnerDepart");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<RolePermission>().ToTable("RolePermission");
            modelBuilder.Entity<File>().ToTable("File");
        }
    }



    public class DbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(@"Server=.;Database=HRMS;User=sa;Pwd=china;Trusted_Connection=True;");

            return new DataContext(optionsBuilder.Options);
        }
    }

}
