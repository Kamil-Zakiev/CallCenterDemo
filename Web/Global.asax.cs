using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core;
using Domain;
using Domain.Interfaces.Process;
using Domain.Interfaces.Requests;
using Domain.Interfaces.Users;
using Domain.Services.Services;
using Domain.Services.Services.Process;
using NHibernateConfigs;
using Web.Autentications;
using Web.Controllers;
using Web.Infrastructure;
using Web.Infrastructure.ModelBinders;

namespace Web
{
    public class MvcApplication : HttpApplication
    {
        public static IWindsorContainer Container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(UserLoadParams), new UserLoadParamsBinder());

            InitIoC();

            SessionFactoryProvider.InitConfig(ConfigurationManager.AppSettings.Get("ConnectionString"));
        }

        private void InitIoC()
        {
            Container = new WindsorContainer();
            Container.Register(Component.For<IWindsorContainer>().Instance(Container));
            Container.Register(Component.For<IAuthenticationService>()
                .ImplementedBy<CustomAutentication>()
                .LifestylePerWebRequest());

            Container.Register(Component.For<IAuthorizationService>().ImplementedBy<AuthorizationService>().LifestylePerWebRequest());

            RegisterControllers();
            RegisterServices();

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container));
        }

        private void RegisterServices()
        {
            Container.Register(Component.For<IRequestCreationService>().ImplementedBy<RequestCreationService>().LifestylePerWebRequest());
            Container.Register(Component.For<ICurrentOperatorService>().ImplementedBy<CurrentOperatorService>().LifestylePerWebRequest());
            Container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestylePerWebRequest());
            Container.Register(Component.For<IPasswordHashService>().ImplementedBy<PasswordHashService>().LifestylePerWebRequest());
            Container.Register(Component.For(typeof(IDataStore<>)).ImplementedBy(typeof(DataStore<>)).LifestylePerWebRequest());
            
            Container.Register(Component.For<INextStateService>().ImplementedBy<NextStateService>());
        }

        protected void Application_AuthenticateRequest(object source, EventArgs e)
        {
            var app = (HttpApplication)source;
            var context = app.Context;

            var customAutentication = Container.Resolve<IAuthenticationService>();
            customAutentication.HttpContext = context;
            context.User = customAutentication.CurrentUserPrincipal;
        }

        private void RegisterControllers()
        {
            var controllerTypes =
                AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(a => a.GetExportedTypes())
                .Where(type => !type.IsAbstract)
                .Where(type => typeof(Controller).IsAssignableFrom(type));

            foreach (var controllerType in controllerTypes)
            {
                Container.Register(Component.For(controllerType)
                    .ImplementedBy(controllerType)
                    .Named(controllerType.Name)
                    .LifestyleTransient());
            }
        }
    }
}