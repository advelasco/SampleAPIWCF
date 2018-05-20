namespace Sample.Repository
{
    using Sample.Repository.Interface;
    using System;

    public class UnitOfWork : IUnityOfWork, IDisposable
    {
        private SampleContext _context;

        public UnitOfWork(SampleContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
