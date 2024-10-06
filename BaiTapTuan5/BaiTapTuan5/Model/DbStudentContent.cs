using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BaiTapTuan5.Model
{
    public partial class DbStudentContent : DbContext
    {
        public DbStudentContent()
            : base("name=DbStudentContent")
        {
        }

        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public object Faculty { get; internal set; }
        public object Student { get; internal set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
