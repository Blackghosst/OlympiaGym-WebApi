using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.RestAPI.Dtos
{
    public class SaveMemberDto
    {
        public int Id { get; set; }
        public string Cin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public byte Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Photo { get; set; }
        public bool IsDeleted { get; set; }
        public string Occupation { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlackListed { get; set; }
        public int DistrictId { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime LastUpdated { get; set; }
        //public string UpdatedBy { get; set; }
    }

}