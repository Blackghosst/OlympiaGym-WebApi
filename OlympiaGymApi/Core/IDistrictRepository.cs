using System.Collections.Generic;
using System.Threading.Tasks;
using OlympiaGymApi.Core.Models;

namespace OlympiaGymApi.Core
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<District>> GetDistricts();
        //Task<Member> GetMember(int id);
        //void Add(Member member);
        //void Remove(Member member);
        //bool MemberExists(int id);
    }
}