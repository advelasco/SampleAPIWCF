namespace Sample.Mediator.Interface
{
    using Samples.Service.WCF.Interface;

    public interface IServiceFactory
    {
        IUserService GetUserService();
        ISubscriptionService GetSubscriptionService();
    }
}