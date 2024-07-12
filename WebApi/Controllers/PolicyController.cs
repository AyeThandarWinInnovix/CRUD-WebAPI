using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Interface;
using WebApi.Domain.Service;
using WebApi.Dtos.PolicyDtos;
using WebApi.Dtos.PostDtos;
using WebApi.ResponseModels;
using WebApi.Testing;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<BaseResponseModel<bool>> CreatePolicy([FromForm] PolicyCreateDto policyDto)
        {
            if (!ModelState.IsValid)
                return new BaseResponseModel<bool>(StatusCodes.Status400BadRequest, "Model is not valid");

            try
            {
                policyDto.IsActive = true;
                var result = await _policyService.CreatePolicy(policyDto);
                if (result)
                    return new BaseResponseModel<bool>(StatusCodes.Status200OK, "Success", true);
                else
                    return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, "An error occurred while creating the policy", false);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<bool>(StatusCodes.Status500InternalServerError, ex.Message, false);
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<BaseResponseModel<TblPolicy>> GetPolicy()
        {
            var policies = await _policyService.GetPolicies();
            if (!policies.Any())
                return new BaseResponseModel<TblPolicy>(StatusCodes.Status404NotFound, "No post found.");

            return new BaseResponseModel<TblPolicy>(StatusCodes.Status200OK, "Success", policies.ToList());
        }
    }
}
