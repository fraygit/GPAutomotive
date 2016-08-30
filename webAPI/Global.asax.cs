using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web;
using System.Web.Http;
using GPA.MongoData.Interface;
using GPA.MongoData.Repository;

namespace GPA.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<ICarRepository, CarRepository>(Lifestyle.Scoped);
            container.Register<IParkingSpaceRepository, ParkingSpaceRepository>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IUserTokenRepository, UserTokenRepository>(Lifestyle.Scoped);
            container.Register<IEmailNotificationRepository, EmailNotificationRepository>(Lifestyle.Scoped);
            container.Register<IBookingRepository, BookingRepository>(Lifestyle.Scoped);
            container.Register<IParkingImageRepository, ParkingImageRepository>(Lifestyle.Scoped);
            container.Register<IAdminUserRepository, AdminUserRepository>(Lifestyle.Scoped);
            container.Register<IAdminUserTokenRepository, AdminUserTokenRepository>(Lifestyle.Scoped);
            container.Register<ICCTransactionRepository, CCTransactionRepository>(Lifestyle.Scoped);
            container.Register<IUserCreditCardRepository, UserCreditCardRepository>(Lifestyle.Scoped);
            container.Register<ICreditCardPaymentRepository, CreditCardPaymentRepository>(Lifestyle.Scoped);
            container.Register<IParkingRepository, ParkingRepository>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
