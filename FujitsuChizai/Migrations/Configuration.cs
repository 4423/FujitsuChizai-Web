namespace FujitsuChizai.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using FujitsuChizai.Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<FujitsuChizai.Models.Entities.ModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FujitsuChizai.Models.Entities.ModelContext context)
        {   
        }
    }
}
