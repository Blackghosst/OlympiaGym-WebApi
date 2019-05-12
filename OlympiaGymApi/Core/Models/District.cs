using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OlympiaGymApi.Core.Models
{
    [Table("Districts")]
    public class District
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        //public ICollection<Member> Members { get; set; }

        //public District()
        //{
        //    Members = new Collection<Member>();
        //}
    }
}
