using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlympiaGymApi.RestAPI.Dtos
{
    public class PaymentDto
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
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
