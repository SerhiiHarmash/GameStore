using System.Web.Mvc;
using GameStore.CR;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

namespace GameStore.WEB
{
    public static class NinjectConfigurator
    {
        internal static void Configuration()
        {
            NinjectModule ninjectModule = new NinjectRegistrations();
            var kernel = new StandardKernel(ninjectModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}