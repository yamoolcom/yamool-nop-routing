using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Web.Framework.Mvc.Routing;

namespace Yamool.Nop.Plugin.Routing.Infrastructure
{
    public sealed class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 100;

        public void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            services.AddScoped<IRouteProvider, NopRouteProvider>();
        }
    }
}