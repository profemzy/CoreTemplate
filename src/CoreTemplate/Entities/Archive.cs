using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.Entities
{
    public class Archive
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string Name { get; set; }

        [Display(Name = "File Date")]
        [DataType(DataType.Date)]
        public DateTime CaseDate { get; set; }

        [Display(Name = "File Path")]
        public string FilePath { get; set; }
    }
}
