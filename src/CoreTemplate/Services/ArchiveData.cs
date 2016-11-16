using CoreTemplate.Contracts;
using CoreTemplate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Services
{  

    public class ArchiveData : IArchiveData
    {
        private readonly ArchiveContext _context;

        public ArchiveData(ArchiveContext context)
        {
            _context = context;
        }

        public void Add(Archive newArchive)
        {
            _context.Add(newArchive);            
        }

        public void Update(Archive updatedArchive)
        {
            _context.Entry(updatedArchive).State = EntityState.Modified;
        }

        public void Delete(Archive archive)
        {
            _context.Remove(archive);
        }

        public int Commit()
        {
          return _context.SaveChanges();
        }

        public Archive Get(int id)
        {
           return _context.Archives.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Archive> GetAll()
        {
            return _context.Archives;
        }
     
    }
}
