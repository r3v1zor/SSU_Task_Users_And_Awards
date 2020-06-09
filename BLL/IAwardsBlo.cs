using System.Collections.Generic;
using Models;

namespace BLL
{
    public interface IAwardsBlo
    {
        void Save(string title);


        void Delete(long awardId);


        Award FindById(long awId);


        List<Award> FindAll();
    }
}