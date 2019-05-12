using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OlympiaGymApi.Core;
using OlympiaGymApi.Core.Models;
using OlympiaGymApi.Persistance;
using OlympiaGymApi.RestAPI.Dtos;

namespace OlympiaGymApi.RestAPI
{
    [Produces("application/json")]
    [Route("api/Payments")]
    public class PaymentsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPaymentRepository repository;
        private readonly IMembershipRepository membershipRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;

        public PaymentsController(IMapper mapper, IPaymentRepository repository, IMembershipRepository membershipRepository, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.membershipRepository = membershipRepository;
            this.unitOfWork = unitOfWork;
            this.logger = loggerFactory.CreateLogger(nameof(MembersController));
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<IEnumerable<PaymentDto>> GetAllPayments()
        {
            try
            {
                var payments = await repository.GetAllPayments();
                return mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentDto>>(payments);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                throw exp;
            }
        }

        // GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<PaymentDto>> GetPayments(int id)
        {
            try
            {
                var payments = await repository.GetPayments(id);
                return mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentDto>>(payments);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                throw exp;
            }
        }
        [Route("payment/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPayment([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = await repository.GetPayment(id);

            if (payment == null)
            {
                return NotFound();
            }
            var paymentDto = mapper.Map<Payment, PaymentDto>(payment);
            return Ok(paymentDto);
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!membershipRepository.MembershipExists(paymentDto.MembershipId))
            {
                return NotFound();
            }
            else
            {
                var payment = mapper.Map<Payment>(paymentDto);
                repository.Add(payment);
                await unitOfWork.Complete();

                //return CreatedAtAction("GetMembership", new { id = membership.Id });
                return StatusCode(201);
            }

        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment([FromRoute]int id, [FromBody] PaymentDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentDto.Id)
            {
                return BadRequest();
            }

            var payment = await repository.GetPayment(id);

            if (!repository.PaymentExists(id))
            {
                return NotFound();
            }
            else
            {
                mapper.Map(paymentDto, payment);

            }

            try
            {
                repository.Update(payment);
                await unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!repository.PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            payment = await repository.GetPayment(id);
            var result = mapper.Map<Payment, PaymentDto>(payment);
            return Ok(result);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
