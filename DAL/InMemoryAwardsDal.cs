using System;
using System.Collections.Generic;
using Models;

namespace DAL
{
    public class InMemoryAwardsDal : IAwardsDao
    {
        private readonly List<Award> _awards;
        private readonly Random _random;

        public InMemoryAwardsDal()
        {
            _awards = new List<Award>();
            _random = new Random();
        }

        public void Save(Award award)
        {
            award.Id = _random.Next();
            _awards.Add(award);
        }

        public void Delete(Award award)
        {
            _awards.Remove(award);
        }

        public void Delete(long id)
        {
            _awards.Remove(_awards.Find(award => award.Id == id));
        }

        public List<Award> FindAll()
        {
            return _awards;
        }

        public Award FindById(long id)
        {
            return _awards.Find(award => award.Id == id);
        }
    }
}