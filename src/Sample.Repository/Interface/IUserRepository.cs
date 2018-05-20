namespace Sample.Repository.Interface
{
    using Sample.Model;
    using System.Collections.Generic;

    public interface IUserRepository
    {
        IEnumerable<UserModel> GetAll();
        UserModel GetById(long id);
        void Insert(UserModel user);
        void Delete(long id);
        void Update(UserModel user);
    }
}
