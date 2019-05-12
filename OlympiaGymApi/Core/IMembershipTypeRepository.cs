using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Core.Models;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.Core
{
    public interface IMembershipTypeRepository
    {
        Task<IEnumerable<MembershipType>> GetMembershipType(byte gId);
    }
}
