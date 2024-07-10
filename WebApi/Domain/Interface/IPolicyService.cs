using WebApi.Dtos.PolicyDtos;
using WebApi.Testing;

namespace WebApi.Domain.Interface
{
    public interface IPolicyService
    {
        Task<string> GeneratePolicyIdAsync();
        Task<bool> CreatePolicy(PolicyCreateDto policyDto);
        Task<IEnumerable<TblPolicy>> GetPolicies();
    }
}
