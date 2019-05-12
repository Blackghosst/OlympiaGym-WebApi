using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.RestAPI.Dtos
{
    public class MemberDto
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
        [StringLength(50)]
        public string ParentName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        //[DataType(DataType.Date)]
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
        [StringLength(255)]
        public string Occupation { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlackListed { get; set; }
        public int DistrictId { get; set; }
        public KeyValuePairDto District { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }


        public ICollection<MembershipDto> Memberships { get; set; }

        public MemberDto()
        {
            Memberships = new Collection<MembershipDto>();
            //IsActive = true;
            //IsBlackListed = false;
            //IsDeleted = false;
        }
    }
}
