

namespace WordChainGame.Web.App_Start
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using AutoMapper;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler;
    using System.Reflection;
    using System.Web.Http;
    using WordChainGame.Data.Model;
    using WordChainGame.Services.Repositories;
    using WordChainGame.Services.Services;
    using WordChainGame.Services.Services.Words;
    using WordChainGame.Services.UnitOfWork;
    using WordChainGame.Utils.MapperProfile;

    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<WordChainGameContext>()
                   .InstancePerRequest();

            builder.RegisterGeneric(typeof(GenericRepository<>))
                   .As(typeof(IGenericRepository<>))
                   .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                   .As<IUnitOfWork>()
                   .InstancePerRequest();

            builder.RegisterType<UserService>()
                   .As<IUserService>()
                   .InstancePerRequest();

            builder.RegisterType<TopicService>()
                   .As<ITopicService>()
                   .InstancePerRequest();

            builder.RegisterType<WordService>()
                   .As<IWordService>()
                   .InstancePerRequest();

            builder.RegisterType<SecureDataFormat<AuthenticationTicket>>()
                   .As<ISecureDataFormat<AuthenticationTicket>>()
                   .InstancePerRequest();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WordChainGameProfile());
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }
}