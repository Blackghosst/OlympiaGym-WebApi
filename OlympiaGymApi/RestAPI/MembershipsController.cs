using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlympiaGymApi.Core.Models;
using OlympiaGymApi.Persistance;
using OlympiaGymApi.RestAPI.Dtos;
using AutoMapper;
using OlympiaGymApi.Core;

namespace OlympiaGymApi.RestAPI
{
    [Produces("application/json")]
    [Route("api/Members/{id:int}/memberships")]
    //[Route("api/memberships")]
    public class MembershipsController : Controller
    {
        private readonly OlympiaGymApiDbContext _context;
        private readonly IMapper mapper;
        private readonly IMembershipRepository msRepository;
        private readonly IMemberRepository mRepository;
        private readonly IUnitOfWork unitOfWork;

        public MembershipsController(OlympiaGymApiDbContext context, IMapper mapper, IMembershipRepository msRepository, IMemberRepository mRepository, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.mapper = mapper;
            this.mRepository = mRepository;
            this.msRepository = msRepository;
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Memberships
        [HttpGet]
        public async Task<IEnumerable<MembershipDto>> GetMemberships(int id)
        {
            var memberships = await msRepository.GetMemberships(id);
            return mapper.Map<IEnumerable<Membership>, IEnumerable<MembershipDto>>(memberships);
            //return  Ok(member.Memberships.ToList());
        }

        // GET: api/Memberships/5
        [HttpGet("{msId:int}", Name = "GetMembership")]
        public async Task<IActionResult> GetMembership([FromRoute] int msId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var membership = await msRepository.GetMembership(msId);

            if (membership == null)
            {
                return NotFound();
            }
            var membershipDto = mapper.Map<MembershipDto>(membership);
            return Ok(membershipDto);
        }

        // PUT: api/Memberships/5
        [HttpPut("{msId:int}")]
        public async Task<IActionResult> PutMembership([FromRoute] int msId, [FromBody] SaveMembershipDto membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (msId ==0)
            {
                return BadRequest();
            }

            //_context.Entry(membership).State = EntityState.Modified;
            var membership = await msRepository.GetMembership(msId);

            if (!msRepository.MembershipExists(msId))
            {
                return NotFound();
            }
            else
            {
                mapper.Map(membershipDto, membership);
            }

            try
            {
                msRepository.Update(membership);
                await unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!msRepository.MembershipExists(membershipDto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Memberships
        [HttpPost]
        public async Task<IActionResult> PostMembership([FromRoute]int id, [FromBody] SaveMembershipDto membershipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!mRepository.MemberExists(id))
            {
                return NotFound();
            }
            else
            {
                var membership = mapper.Map<Membership>(membershipDto);
                msRepository.Add(/*id,*/ membership);
                await unitOfWork.Complete();

                //return CreatedAtAction("GetMembership", new { id = membership.Id });
                return Ok(membership);
            }
        }

        // DELETE: api/Memberships/5
        [HttpDelete("{msId:int}")]
        public async Task<IActionResult> DeleteMembership([FromRoute] int msId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var membership = await msRepository.GetMembership(msId); ;
            if (membership == null)
            {
                return NotFound();
            }

            msRepository.Remove(membership);
            await unitOfWork.Complete();

            return Ok(membership);
        }

    }
}