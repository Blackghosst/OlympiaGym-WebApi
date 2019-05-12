using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlympiaGymApi.Core;
using OlympiaGymApi.RestAPI.Dtos;
using AutoMapper;
using OlympiaGymApi.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace OlympiaGymApi.RestAPI
{
    [Produces("application/json")]
    [Route("api/Members")]
    public class MembersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMemberRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;

        public MembersController(IMapper mapper, IMemberRepository repository, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = loggerFactory.CreateLogger(nameof(MembersController));
        }

        /// <response code="200">Returns the List of Members</response>
        /// <response code="400">If the item is null</response>           
        //GET: api/Members
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberDto>), 200)]
        [ProducesResponseType(typeof(MemberDto), 400)]
        public async Task<IEnumerable<MemberDto>> GetMembers()
        {
            try
            {
                var members = await repository.GetMembers();
                return mapper.Map<IEnumerable<Member>, IEnumerable<MemberDto>>(members);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                throw exp;
            }
        }

        /// <response code="200">Returns the List of Members</response>
        /// <response code="400">If the item is null</response>           
        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = await repository.GetMember(id);

            if (member == null)
            {
                return NotFound();
            }
            var memberDto = mapper.Map<Member, MemberDto>(member);
            return Ok(memberDto);
        }

        [Route("inact&blacklistMembers")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberDto>), 200)]
        [ProducesResponseType(typeof(MemberDto), 400)]
        public async Task<IEnumerable<MemberDto>> GetInactiveAndBlacklistedMembers()
        {
            try
            {
                var members = await repository.GetInactiveAndBlacklistedMembers();
                return mapper.Map<IEnumerable<Member>, IEnumerable<MemberDto>>(members);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                throw exp;
            }
        }

        //[Route("inact&blacklistMembers/member")]
        //[HttpGet("{ibId}")]
        //[ProducesResponseType(typeof(IEnumerable<MemberDto>), 200)]
        //[ProducesResponseType(typeof(MemberDto), 400)]
        //public async Task<IActionResult> GetInactiveOrBlacklistedMember([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var member = await repository.GetInactiveOrBlacklistedMember(id);

        //    if (member == null)
        //    {
        //        return NotFound();
        //    }
        //    var memberDto = mapper.Map<Member, MemberDto>(member);
        //    return Ok(memberDto);
        //}

        //GET: api/Members/IsCinRegistred
        [Route("IsCinRegistred")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberDto>), 200)]
        [ProducesResponseType(typeof(MemberDto), 400)]
        public async Task<bool> IsCinRegistred(string cin)
        {
            try
            {
                IList<Member> membersCin = await repository.GetCin(cin);
                return membersCin.Count == 0;
                //var members = await repository.GetCin();
                //return mapper.Map<IEnumerable<Member>, IEnumerable<MemberDto>>(members);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                throw exp;
            }
        }

        //GET: api/Members/IsCinRegistred
        [Route("GetFirstNames")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberDto>), 200)]
        [ProducesResponseType(typeof(MemberDto), 400)]
        public async Task<IReadOnlyList<string>> GetFirstNames()
        {
            //List<string> FirstNamesList = new List<string>();

            try
            {
                var members = await repository.GetFirstNames();
                //foreach (Member m in members)
                //{
                //    FirstNamesList.Add(m.FirstName);
                //}
                return members;
                //return FirstNamesList;
                //var members = await repository.GetCin();
                //return mapper.Map<IEnumerable<Member>, IEnumerable<MemberDto>>(members);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                throw exp;
            }
        }

        // PUT: api/Members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember([FromRoute] int id, [FromBody] SaveMemberDto saveMemberDto)
        {
            //throw new Exception();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saveMemberDto.Id)
            {
                return BadRequest();
            }

            var member = await repository.GetMember(id);

            if (!repository.MemberExists(id))
            {
                return NotFound();
            }
            else
            {
                mapper.Map(saveMemberDto, member);

            }

            try
            {
                repository.Update(member);
                await unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!repository.MemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            member = await repository.GetMember(id);
            var result = mapper.Map<Member, MemberDto>(member);
            return Ok(result);
        }

        /// <response code="200">Returns the List of Members</response>
        /// <response code="400">If the item is null</response>           
        // POST: api/Members
        [HttpPost]
        public async Task<IActionResult> PostMember([FromBody] SaveMemberDto saveMemberDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = mapper.Map<SaveMemberDto, Member>(saveMemberDto);
            //member.CreatedDate = DateTime.Now;

            repository.Add(member);
            await unitOfWork.Complete();

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        /// <response code="200">Returns the List of Members</response>
        /// <response code="400">If the item is null</response>           
        // Patch: api/Members/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMember([FromRoute] int id, [FromBody] JsonPatchDocument<SaveMemberDto> jsonPatchMember)
        {
            if (jsonPatchMember == null)
            {
                return BadRequest();
            }
            var member = new Member();

            if (!repository.MemberExists(id))
            {
                member = await repository.GetInactiveOrBlacklistedMember(id);
            }
            else if (!repository.MemberExists(id))
            {
                return NotFound();
            }
            else
            {
                member = await repository.GetMember(id);
            }
            var memberDto = mapper.Map<SaveMemberDto>(member);
            //if (!repository.MemberExists(id))
            //{
            //    var member = await repository.GetInactiveOrBlacklistedMember(id);
            //    if (member==null)
            //    {
            //        
            //        jsonPatchMember.ApplyTo(memberDto);
            //        mapper.Map(memberDto, member);
            //    }
            //}
            //else
            //{

                jsonPatchMember.ApplyTo(memberDto);
                mapper.Map(memberDto, member);
            //}
            try
            {
                await unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!repository.MemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var result = mapper.Map<Member, MemberDto>(member);
            return Ok(result);
        }

        /// <response code="200">Returns the List of Members</response>
        /// <response code="400">If the item is null</response>           
        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = await repository.GetMember(id);
            if (member == null)
            {
                return NotFound();
            }

            repository.Remove(member);
            await unitOfWork.Complete(); ;

            return Ok(member);
        }

    }
}
