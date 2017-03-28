using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using LinkedInEx.CommonClasses;
using LinkedInEx.Indexers;
using LinkedInEx.LinkedInCache;
using LinkedInEx.ProfileIdProvider;
using LinkedInEx.ScoreService;
using LinkedInEx.Search;
using LinkedInEx.Service;
using LinkedInEx.Validators;

namespace LinkedInEx
{
    public static class ContainerConfig
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<BulkedLinkedInServiceDecorator>().As<ILinkedInService>().SingleInstance().WithParameter(new NamedParameter("decorated", "LinkedInService"));
            builder.RegisterType<LinkedInService>().Named<ILinkedInService>("LinkedInService");
            builder.RegisterType<DummyProfileValidator>().As<IProfileValidator>().SingleInstance();
            builder.RegisterType<GuidIdProvider>().As<IProfileIdProvider>().SingleInstance();
            builder.RegisterType<DataIndexersCache>().As<ILinkedInCache<InnerLinkedInProfile>>().SingleInstance();
            builder.RegisterType<OnlySkillSearchCache>().As<ILinkedInCache<SearchCount>>().SingleInstance();
            builder.RegisterType<BySearchCacheScoreService>().As<IScoreService>().SingleInstance();
            builder.RegisterType<DummyProfileValidator>().As<IProfileValidator>().SingleInstance();
            builder.RegisterType<ValidationProfileSearchProxy>().As<IProfileSearcher>().WithParameter("profileSearcher", "SearchCountDecorator").SingleInstance();
            builder.RegisterType<SearchCountDecorator>().Named<IProfileSearcher>("SearchCountDecorator").WithParameter("decorated", "ProfileSearcher").SingleInstance();
            builder.RegisterType<ProfileSearcher>().Named<IProfileSearcher>("ProfileSearcher");
            builder.RegisterType<PersistentKeyValueProfilesIndexer>().As<ILinkedInProfilesIndexer>().SingleInstance();
            return builder.Build();
        }
    }
}