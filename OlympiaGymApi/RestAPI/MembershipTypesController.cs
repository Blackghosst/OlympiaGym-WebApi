using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OlympiaGymApi.Core;
using OlympiaGymApi.Core.Models;
using OlympiaGymApi.RestAPI.Dtos;

namespace OlympiaGymApi.RestAPI
{
    [Produces("application/json")]
    [Route("api/MembershipTypes")]
    public class MembershipTypesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMembershipTypeRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;

        public MembershipTypesController(IMapper mapper, IMembershipTypeRepository repository, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = loggerFactory.CreateLogger(nameof(MembershipTypesController));
        }

        //GET: api/MembershipTypes
        [HttpGet("{gId}")]
        public async Task<IEnumerable<MembershipTypeDto>> GetMembershipTypes([FromRoute] byte gId)
        {
            try
            {
                var msTypes = await repository.GetMembershipType(gId);
                return mapper.Map<IEnumerable<MembershipType>, IEnumerable<MembershipTypeDto>>(msTypes);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                throw exp;
            }
        }

    }
}