using System;
using System.Collections.Generic;
using Models;

namespace BLL
{
    public interface IUsersBlo
    {
        void Save(string name, DateTime dateOfBirth);
        void Delete(long id);

        void AddAward(long userId, Award award);
        void DeleteAward(long userId, long awardId);
        List<User> FindAll();

        User FindById(long userId);
    }
}