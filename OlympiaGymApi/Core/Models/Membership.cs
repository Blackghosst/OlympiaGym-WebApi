using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Persistance;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.Core.Models
{
    [Table("Memberships")]
    public class Membership: BaseEntity
    {
        public int Id { get; set; }
        public int MemberId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public MembershipState Status { get; private set; }
        public int MembershipTypeId { get; set; }
        public MembershipType MembershipType { get; set; }
        public Payment Payment { get; set; }

    }
}
