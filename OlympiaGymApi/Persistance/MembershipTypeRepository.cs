using Microsoft.EntityFrameworkCore;
using OlympiaGymApi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Core.Models;
using OlympiaGymApi.Enumerators;
using OlympiaGymApi.Persistance;

namespace OlympiaGymApi.Persistance
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly OlympiaGymApiDbContext context;

        public MembershipTypeRepository(OlympiaGymApiDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<MembershipType>> GetMembershipType(byte gId)
        {
            return await context.MembershipTypes
                .Where(mst => mst.Gender == gId && mst.Status == true)
                .ToListAsync();
        }
    }
}
