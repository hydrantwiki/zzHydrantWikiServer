using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;

namespace HydrantWiki.Mobile.Api.Bootstrapper
{
    public class CustomBoostrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoCContainer _container, NancyContext _context)
        {
            base.ConfigureRequestContainer(_container, _context);
        }

        protected override void ApplicationStartup(TinyIoCContainer _container, IPipelines _pipelines)
        {
            CookieBasedSessions.Enable(_pipelines);
            Nancy.Security.Csrf.Enable(_pipelines);

        }
        
        protected override void ConfigureConventions(NancyConventions _conventions)
        {
            base.ConfigureConventions(_conventions);
           
            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("images", @"images")
            );

            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddFile("/api.html", "api.html")
            );
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(
                    _builder => _builder.StatusCodeHandlers = new List<Type>());
            }
        }
    }
}