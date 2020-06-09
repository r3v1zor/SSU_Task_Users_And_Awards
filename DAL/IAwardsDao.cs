using System.Collections.Generic;
using Models;

namespace DAL
{
    public interface IAwardsDao
    {
        void Save(Award award);

        void Delete(Award award);

        void Delete(long id);

        List<Award> FindAll();

        Award FindById(long id);
    }
}