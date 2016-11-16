using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTemplate.Entities;

namespace CoreTemplate.ViewModels
{
    public class ArchiveViewModel
    {
        public IEnumerable<Archive> Archives { get; set; }
    }
}
