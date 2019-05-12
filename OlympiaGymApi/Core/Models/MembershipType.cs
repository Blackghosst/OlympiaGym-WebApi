using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Persistance;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.Core.Models
{
    [Table("MembershipTypes")]
    public class MembershipType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        public byte Gender { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public decimal Price { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public bool Status { get; set; }

        public ICollection<Membership> Memberships { get; set; }

        public MembershipType()
        {
            Memberships = new Collection<Membership>();
        }
    }
}
