using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    }
}