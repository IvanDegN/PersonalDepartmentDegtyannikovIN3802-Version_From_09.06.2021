﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonalDepartmentDegtyannikovIN3802
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PersonalDepartmentDB : DbContext
    {
        public PersonalDepartmentDB()
            : base("name=PersonalDepartmentDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Sexes> Sexes { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}