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
    public class MembershipRepository : IMembershipRepository
    {
        private readonly OlympiaGymApiDbContext context;

        public MembershipRepository(OlympiaGymApiDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Membership>> GetMemberships(int id)
        {
            return await context.Memberships
                .Where(ms => ms.MemberId == id)
                .Include(ms=>ms.MembershipType)
                .Include(ms => ms.Payment)
                .OrderByDescending(ms=>ms.EndDate)
                .ToListAsync();
        }

        public async Task<Membership> GetMembership(int id)
        {
            return await context.Memberships
                .Include(ms => ms.MembershipType)
                .Include(ms=>ms.Payment)
                .SingleOrDefaultAsync(ms => ms.Id == id);
        }
        public void Add(Membership membership)
        {
            //var member = context.Members.SingleOrDefault(m => m.Id == id);
            //member.Memberships.Add(membership);
            context.Memberships.Add(membership);
        }
        public void Update(Membership membership)
        {
            context.Memberships.Update(membership);
        }

        public void Remove(Membership membership)
        {
            context.Memberships.Remove(membership);
        }

        public bool MembershipExists(int id)
        {
            return context.Memberships.Any(e => e.Id == id);
        }


    }
}
