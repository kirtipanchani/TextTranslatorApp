using System.Web.Mvc;
using TextTranslatorApp.Service;
using TextTranslatorApp.ServiceContract;
using Unity;
using Unity.Mvc5;

namespace TextTranslatorApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<Itranslator, Translator>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}