using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartup(typeof(FujitsuChizai.Startup))]

namespace FujitsuChizai
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Models.Entities.ModelContext>());
        }
    }
}
