using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Core;

namespace OlympiaGymApi.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OlympiaGymApiDbContext context;

        public UnitOfWork(OlympiaGymApiDbContext context)
        {
            this.context = context;
        }
        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}
