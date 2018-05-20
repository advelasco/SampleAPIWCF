namespace Sample.Mediator
{
    using Sample.Mediator.Interface;
    using Samples.Service.WCF.Interface;
    using System.ServiceModel;

    public class ServiceFactory : IServiceFactory
    {
        public IUserService GetUserService()
        {
            ChannelFactory<IUserService> myChannelFactory = new ChannelFactory<IUserService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:7741/Sample/Services/UserService"));

            return myChannelFactory.CreateChannel();
        }

        public ISubscriptionService GetSubscriptionService()
        {
            ChannelFactory<ISubscriptionService> myChannelFactory = new ChannelFactory<ISubscriptionService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:7741/Sample/Services/SubscriptionService"));

            return myChannelFactory.CreateChannel();
        }
    }
}