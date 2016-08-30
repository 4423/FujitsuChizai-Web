using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models.Entities
{
    public class ModelContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<PlaceMark> PlaceMarks { get; set; }
        public DbSet<Edge> Edges { get; set; }
        public DbSet<Map> Maps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Edges 内で2つのForeignKeyを指定するとエラーが発生するためカスケードを削除する
            // http://stackoverflow.com/questions/14489676/entity-framework-how-to-solve-foreign-key-constraint-may-cause-cycles-or-multi
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}