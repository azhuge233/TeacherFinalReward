﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeacherReward.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TeacherRewardEntities : DbContext
    {
        public TeacherRewardEntities()
            : base("name=TeacherRewardEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Department> Department { get; set; }
        public DbSet<TeacherCrosswiseTaskInfo> TeacherCrosswiseTaskInfo { get; set; }
        public DbSet<TeacherInfo> TeacherInfo { get; set; }
        public DbSet<TeacherPrizeInfo> TeacherPrizeInfo { get; set; }
        public DbSet<TeacherProjectInfo> TeacherProjectInfo { get; set; }
        public DbSet<TeacherPublishThesisInfo> TeacherPublishThesisInfo { get; set; }
        public DbSet<TeacherScore> TeacherScore { get; set; }
        public DbSet<TeacherTaskInfo> TeacherTaskInfo { get; set; }
        public DbSet<TeacherTeachThesisInfo> TeacherTeachThesisInfo { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
