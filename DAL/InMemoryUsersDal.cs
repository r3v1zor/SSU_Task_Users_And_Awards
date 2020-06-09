using System;
using System.Collections.Generic;
using Models;

namespace DAL
{
    public class InMemoryUsersDal : IUsersDao
    {
        private readonly List<User> _users;
        private readonly Random _random;

        public InMemoryUsersDal()
        {
            _users = new List<User>();
            _random = new Random();
        }

        public void Save(User user)
        {
            user.Id = _random.Next();
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public void Delete(long id)
        {
            _users.Remove(_users.Find(user => user.Id == id));
        }

        public List<User> FindAll()
        {
            return _users;
        }

        public User FindById(long id)
        {
            return _users.Find(user => user.Id == id);
        }

        public List<Award> GetAllAwards(long id)
        {
            return _users.Find(user => user.Id == id).Awards;
        }
        
        public void AddAward(long userId, Award award)
        {
            _users.Find(user => user.Id == userId).Awards.Add(award);
        }

        public void DeleteAwardById(long userId, long awardId)
        {
            var awards = _users.Find(user => user.Id == userId).Awards;

            awards.Remove(awards.Find(award => award.Id == awardId));
        }
    }
}