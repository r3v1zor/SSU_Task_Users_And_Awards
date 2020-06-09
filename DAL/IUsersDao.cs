using System.Collections.Generic;
using Models;

namespace DAL
{
    public interface IUsersDao
    {
        void Save(User user);

        void Delete(User user);

        void Delete(long id);

        List<User> FindAll();

        User FindById(long id);

        List<Award> GetAllAwards(long id);

        void AddAward(long userId, Award award);

        void DeleteAwardById(long userId, long awardId);
    }
}