using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Persistance;

namespace OlympiaGymApi.Core.Models
{
    [Table("Members")]
    public class Member:Person
    {
        [StringLength(255)]
        public string Occupation { get; set; }
        [StringLength(50)]
        public string ParentName { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlackListed { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public ICollection<Membership> Memberships { get; set; }

        public Member()
        {
            Memberships = new Collection<Membership>();
        }

    }
}
