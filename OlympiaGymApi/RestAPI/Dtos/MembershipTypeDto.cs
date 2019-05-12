using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.RestAPI.Dtos
{
    public class MembershipTypeDto
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
    }
}
