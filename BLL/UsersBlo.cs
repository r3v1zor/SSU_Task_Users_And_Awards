using System;
using System.Collections.Generic;
using DAL;
using Models;

namespace BLL
{
    public class UsersBlo : IUsersBlo
    {
        private IUsersDao _usersDao;

        public UsersBlo(IUsersDao usersDao)
        {
            _usersDao = usersDao;
        }

        public void Save(string name, DateTime dateOfBirth)
        {
            var age = DateTime.Now.Year - dateOfBirth.Year;
            var user = new User(name, dateOfBirth, age);
            
            _usersDao.Save(user);
        }

        public void Delete(long id)
        {
            _usersDao.Delete(id);
        }

        public List<User> FindAll()
        {
            return _usersDao.FindAll();
        }
        
        public void AddAward(long userId, Award award)
        {
            _usersDao.AddAward(userId, award);
        }

        public void DeleteAward(long userId, long awardId)
        {
            _usersDao.DeleteAwardById(userId, awardId);
        }

        public User FindById(long userId)
        {
            return _usersDao.FindById(userId);
        }
    }
}