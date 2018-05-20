namespace Sample.Repository
{
    using Sample.Model;
    using Sample.Repository.Interface;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class UserRepository : IUserRepository, IDisposable
    {
        private SampleContext context;

        public UserRepository(SampleContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserModel> GetAll()
        {
            return context.Users.Where(u => u.Enabled).ToList();
        }

        public UserModel GetById(long id)
        {
            return context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public void Insert(UserModel user)
        {
            context.Users.Add(user);
        }

        public void Delete(long id)
        {
            UserModel user = context.Users.FirstOrDefault(u => u.UserId == id);

            if(user != null)
            {
                user.Enabled = false;

                Update(user);
            }
        }

        public void Update(UserModel user)
        {
            context.Entry(user).State = EntityState.Modified;
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
