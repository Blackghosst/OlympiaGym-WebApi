using OlympiaGymApi.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OlympiaGymApi.Core.Models
{
    [Table("Payments")]
    public class Payment: BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public int MembershipId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int Discount { get; set; }
        [Required]
        public string PaymentType { get; set; }
        public decimal Fees { get; set; }
        public string Notes { get; set; }
        public string CheckNumber { get; set; }
        [Required]
        public decimal PaidAmount { get; set; }
        public Membership Membership { get; set; }
    }
}
