﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BitFrame;
using BitFrame.Models;
using FormationGame.Models;
using FormationGame.Store;

namespace FormationGame
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			Database.SetInitializer(new DropCreateDatabaseAlways<FormationStore>());

			BitCore.Builder = new FormationBitBuilder();

	        (new FormationBitBuilder()).GetStore().Database.Initialize(true);

	        var model = new TestModel
	        {
		        Name = "Hest"
	        };

	        var hestRepo = new Repository<TestModel>();

			hestRepo.Insert(model);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }

	public class FormationBitBuilder : DefaultBitCoreBuilder
	{
		public override BitStore GetStore()
		{
			return new FormationStore(ConfigurationManager.ConnectionStrings["LocalConnection"].ToString());
		}
	}
}