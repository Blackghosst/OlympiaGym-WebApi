using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Core;
using OlympiaGymApi.Core.Models;
using OlympiaGymApi.Enumerators;

namespace OlympiaGymApi.Persistance
{
    public class MemberRepository : IMemberRepository
    {
        private readonly OlympiaGymApiDbContext context;

        public MemberRepository(OlympiaGymApiDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Member>> GetMembers()
        {
            //return await context.Members
            //    .Include(m => m.Memberships.Where(ms=>ms.State==MembershipState.Current))
            //        .ThenInclude(ms => ms.MembershipType)
            //    .ToListAsync();
            //var query = context.Members.Include(m => m.District).AsQueryable();
            var query = context.Members.AsQueryable();
            query.SelectMany(m => m.Memberships)
                .Where(ms => ms.Status == MembershipState.Current)
                .Include(ms => ms.MembershipType)
                .OrderByDescending(ms => ms.Status)
                .Load();
            return await query.ToListAsync();

        }

        public async Task<Member> GetMember(int id)
        {
            return await context.Members
                .Include(m => m.District)
                .Include(m => m.Memberships)
                    .ThenInclude(ms => ms.MembershipType)
                .Include(m => m.Memberships)
                    .ThenInclude(ms => ms.Payment)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Member>> GetInactiveAndBlacklistedMembers()
        {
            return await context.Members
                .Where(m => m.IsActive == false || m.IsBlackListed == true)
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<Member> GetInactiveOrBlacklistedMember(int id)
        {
            return await context.Members
                //.Include(m => m.District)
                //.Include(m => m.Memberships)
                //    .ThenInclude(ms => ms.MembershipType)
                .Where(m => m.IsActive == false || m.IsBlackListed == true)
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public void Add(Member member)
        {
            context.Members.Add(member);
        }
        public void Update(Member member)
        {
            context.Members.Update(member);
        }

        public void Remove(Member member)
        {
            context.Members.Remove(member);
        }

        public async Task<IList<Member>> GetCin(string cin)
        {
            var query = context.Members
                .Where(m => m.Cin == cin)
                .IgnoreQueryFilters();

            query.Select(m => m.Cin)
                .Load();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetFirstNames()
        {
            var query = context.Members
                .IgnoreQueryFilters()
                .OrderBy(m => m.FirstName)
                .Select(m => m.FirstName)
                .Distinct();

            //.GroupBy(m => m.FirstOrDefault())


            return await query.ToListAsync();
        }

        public bool MemberExists(int id)
        {
            return context.Members.Any(e => e.Id == id);
        }
    }
}
