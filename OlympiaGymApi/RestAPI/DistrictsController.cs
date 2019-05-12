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
    [Route("api/Districts")]
    public class DistrictsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IDistrictRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;

        public DistrictsController(IMapper mapper, IDistrictRepository repository, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = loggerFactory.CreateLogger(nameof(DistrictsController));
        }

        //GET: api/Members
        [HttpGet]
        public async Task<IEnumerable<KeyValuePairDto>> GetDistricts()
        {
            try
            {
                var districts = await repository.GetDistricts();
                return mapper.Map<IEnumerable<District>, IEnumerable<KeyValuePairDto>>(districts);
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
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetMember([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var member = await repository.GetMember(id);

        //    if (member == null)
        //    {
        //        return NotFound();
        //    }
        //    var memberDto = mapper.Map<Member, MemberDto>(member);
        //    return Ok(memberDto);
        //}

        ///// <response code="200">Returns the List of Members</response>
        ///// <response code="400">If the item is null</response>           
        //// PUT: api/Members/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMember([FromRoute] int id, [FromBody] MemberDto memberDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != memberDto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var member = await repository.GetMember(id);

        //    if (!repository.MemberExists(id))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        mapper.Map(memberDto, member);
        //        member.LastUpdated = DateTime.Now;
        //    }

        //    try
        //    {
        //        await unitOfWork.Complete();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!repository.MemberExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    var result = mapper.Map<Member, MemberDto>(member);
        //    return Ok(result);
        //}

        ///// <response code="200">Returns the List of Members</response>
        ///// <response code="400">If the item is null</response>           
        //// POST: api/Members
        //[HttpPost]
        //public async Task<IActionResult> PostMember([FromBody] MemberDto memberDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var member = mapper.Map<MemberDto, Member>(memberDto);
        //    member.LastUpdated = DateTime.Now;

        //    repository.Add(member);
        //    await unitOfWork.Complete();

        //    return CreatedAtAction("GetMember", new { id = member.Id }, member);
        //}

        ///// <response code="200">Returns the List of Members</response>
        ///// <response code="400">If the item is null</response>           
        //// Patch: api/Members/5
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> PatchMember([FromRoute] int id, [FromBody] JsonPatchDocument<Member> jsonPatchMember)
        //{
        //    if (jsonPatchMember == null)
        //    {
        //        return BadRequest();
        //    }
        //    var member = await repository.GetMember(id);
        //    if (!repository.MemberExists(id))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        jsonPatchMember.ApplyTo(member);
        //    }
        //    try
        //    {
        //        await unitOfWork.Complete();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!repository.MemberExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    var result = mapper.Map<Member, MemberDto>(member);
        //    return Ok(result);
        //}

        ///// <response code="200">Returns the List of Members</response>
        ///// <response code="400">If the item is null</response>           
        //// DELETE: api/Members/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMember([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var member = await repository.GetMember(id);
        //    if (member == null)
        //    {
        //        return NotFound();
        //    }

        //    repository.Remove(member);
        //    await unitOfWork.Complete(); ;

        //    return Ok(member);
        //}

    }
}
