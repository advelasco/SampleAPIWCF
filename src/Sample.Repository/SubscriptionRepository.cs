namespace Sample.Repository
{
    using Sample.Model;
    using Sample.Repository.Interface;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class SubscriptionRepository : ISubscriptionRepository, IDisposable
    {
        private SampleContext context;

        public SubscriptionRepository(SampleContext context)
        {
            this.context = context;
        }

        public IEnumerable<SubscriptionModel> GetAll()
        {
            return context.Subscriptions.Where(u => u.Enabled).ToList();
        }

        public SubscriptionModel GetById(Guid id)
        {
            return context.Subscriptions.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<SubscriptionModel> GetByUserId(Guid id)
        {
            return context.Subscriptions.Where(u => u.UserId == id && u.Enabled).ToList();
        }

        public void Insert(SubscriptionModel subscription)
        {
            context.Subscriptions.Add(subscription);
        }

        public void Delete(Guid id)
        {
            SubscriptionModel subscription = context.Subscriptions.FirstOrDefault(u => u.Id == id);

            if(subscription != null)
            {
                subscription.Enabled = false;

                Update(subscription);
            }
        }

        public void Update(SubscriptionModel subscription)
        {
            context.Entry(subscription).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
