using System.Collections.Generic;
using System.Threading.Tasks;
using OlympiaGymApi.Core.Models;

namespace OlympiaGymApi.Core
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetMember(int id);
        Task<IEnumerable<Member>> GetInactiveAndBlacklistedMembers();
        Task<Member> GetInactiveOrBlacklistedMember(int id);
        void Add(Member member);
        void Update(Member member);
        void Remove(Member member);
        Task<IList<Member>> GetCin(string cin);
        Task<IReadOnlyList<string>> GetFirstNames();
        bool MemberExists(int id);
    }
}