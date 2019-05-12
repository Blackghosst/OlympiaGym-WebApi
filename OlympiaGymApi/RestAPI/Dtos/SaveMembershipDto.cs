using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympiaGymApi.RestAPI.Dtos
{
    public class SaveMembershipDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int MembershipTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
