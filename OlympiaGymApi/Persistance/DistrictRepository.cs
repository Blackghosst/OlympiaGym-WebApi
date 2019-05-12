using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Core;
using OlympiaGymApi.Core.Models;

namespace OlympiaGymApi.Persistance
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly OlympiaGymApiDbContext context;

        public DistrictRepository(OlympiaGymApiDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<District>> GetDistricts()
        {
            return await context.Districts
                .OrderBy(d=>d.Name)
                .ToListAsync();

        }
    }
}