using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.RestAPI.Dtos
{
    public class MembershipDto
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
        public DateTime CreatedDate { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public MembershipTypeDto MembershipType { get; set; }
        public PaymentDto Payment { get; set; }

    }
}
