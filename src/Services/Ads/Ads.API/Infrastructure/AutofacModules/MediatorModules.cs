using Ads.API.Application.Commands;
using Autofac;
using MediatR;
using System.Reflection;

namespace Ads.API.Infrastructure.AutofacModules
{
    public class MediatorModules : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CreateAdCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return t => componentContext.TryResolve(t, out var o)
                    ? o
                    : null;
            });
        }
    }
}
