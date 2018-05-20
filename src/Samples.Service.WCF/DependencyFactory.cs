namespace Samples.Service.WCF
{
    using Microsoft.EntityFrameworkCore;
    using Sample.Repository;
    using Sample.Repository.Interface;
    using Sample.Services;
    using Sample.Services.Interface;
    using Unity;
    using Unity.Lifetime;

    public class DependencyFactory
    {
        private static IUnityContainer _container;

        /// <summary>
        /// Public reference to the unity container which will 
        /// allow the ability to register instrances or take 
        /// other actions on the container.
        /// </summary>
        public static IUnityContainer Container
        {
            get
            {
                return _container;
            }
            private set
            {
                _container = value;
            }
        }

        /// <summary>
        /// Static constructor for DependencyFactory which will 
        /// initialize the unity container.
        /// </summary>
        static DependencyFactory()
        {
            var container = new UnityContainer();

            container.RegisterType<SampleContext>(new PerResolveLifetimeManager());
            container.RegisterType<IUnityOfWork, UnitOfWork>(new PerResolveLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserServices, UserServices>();

            container.RegisterType<ISubscriptionRepository, SubscriptionRepository>();
            container.RegisterType<ISubscriptionServices, SubscriptionServices>();

            _container = container;
        }

        /// <summary>
        /// Resolves the type parameter T to an instance of the appropriate type.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        public static T Resolve<T>()
        {
            T ret = default(T);

            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }

            return ret;
        }
    }
}
