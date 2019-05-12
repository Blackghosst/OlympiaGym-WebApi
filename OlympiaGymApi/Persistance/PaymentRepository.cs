using Microsoft.EntityFrameworkCore;
using OlympiaGymApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympiaGymApi.Persistance
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly OlympiaGymApiDbContext context;

        public PaymentRepository(OlympiaGymApiDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await context.Payments.ToListAsync();
        }

        //public async Task<Payment> GetPayment(int id)
        //{
        //    return await context.Payments
        //        .Include(m => m.District)
        //        .Include(m => m.Paymentships)
        //            .ThenInclude(ms => ms.PaymentshipType)
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //}
        public async Task<IEnumerable<Payment>> GetPayments(int id)
        {
            return await context.Payments
                //.Include(p => p.Membership.MemberId == id)
                .Where(p => p.MembershipId == p.Membership.Id && p.Membership.MemberId == id)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
            //var query = context.Payments.Join(context.Memberships, p=>p.MembershipId, ms=>ms.Id,(pay, mem)=> new {Payment = pay. })

        }

        public async Task<Payment> GetPayment(int id)
        {
            return await context.Payments
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Add(Payment Payment)
        {
            context.Payments.Add(Payment);
        }
        public void Update(Payment Payment)
        {
            context.Payments.Update(Payment);
        }

        public void Remove(Payment Payment)
        {
            context.Payments.Remove(Payment);
        }

        public bool PaymentExists(int id)
        {
            return context.Payments.Any(e => e.Id == id);
        }

    }
}
