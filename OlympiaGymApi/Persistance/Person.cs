using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.Persistance
{
    public abstract class Person: BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "Le numéro de CIN doit être numérique")]
        public string Cin { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:s}")]
        public DateTime BirthDate { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        public byte[] Photo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
