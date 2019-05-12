using System.Collections.Generic;
using System.Threading.Tasks;
using OlympiaGymApi.Core.Models;

namespace OlympiaGymApi.Core
{
    public interface IMembershipRepository
    {
        Task<IEnumerable<Membership>> GetMemberships(int id);
        Task<Membership> GetMembership(int id);
        void Add(/*int id, */Membership membership);
        void Update(Membership membership);
        void Remove(Membership membership);
        bool MembershipExists(int id);

    }
}