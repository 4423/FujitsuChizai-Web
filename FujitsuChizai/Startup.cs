﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FujitsuChizai.Startup))]

namespace FujitsuChizai
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
