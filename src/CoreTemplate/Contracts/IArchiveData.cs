using CoreTemplate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.Contracts
{
    public interface IArchiveData
    {
        IEnumerable<Archive> GetAll();
        Archive Get(int id);
        void Add(Archive newArchive);

        void Update(Archive updatedArchive);

        void Delete(Archive archive);

        int Commit();
      
    }
}
