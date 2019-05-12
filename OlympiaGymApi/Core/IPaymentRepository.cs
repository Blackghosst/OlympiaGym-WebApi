using OlympiaGymApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OlympiaGymApi.Persistance
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<IEnumerable<Payment>> GetPayments(int id);
        Task<Payment> GetPayment(int id);
        void Add(Payment Payment);
        void Update(Payment Payment);
        bool PaymentExists(int id);
    }
}