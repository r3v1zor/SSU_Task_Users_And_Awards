using System;
using System.Collections.Generic;
using DAL;
using Models;

namespace BLL
{
    public class AwardsBlo : IAwardsBlo
    {
        private IAwardsDao _awardsDao;

        public AwardsBlo(IAwardsDao awardsDao)
        {
            _awardsDao = awardsDao;
        }

        public void Save(string title)
        {
            var award = new Award(title);
            
            _awardsDao.Save(award);
        }

        public void Delete(long awardId)
        {
            _awardsDao.Delete(awardId);
        }

        public Award FindById(long id)
        {
            return _awardsDao.FindById(id);
        }

        public List<Award> FindAll()
        {
            return _awardsDao.FindAll();
        }
    }
}