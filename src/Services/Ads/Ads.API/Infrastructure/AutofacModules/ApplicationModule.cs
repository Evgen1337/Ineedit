using Ads.API.Application.Queries;
using Ads.Domain.AggregatesModel.AdAggregate;
using Ads.Infrastructure.Repositories;
using Autofac;

namespace Ads.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        private readonly string _queriesConnectionString;

        public ApplicationModule(string queriesConnectionString)
        {
            _queriesConnectionString = queriesConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AdQueries(_queriesConnectionString))
                .As<IAdQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AdsRepository>()
                .As<IAdRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
