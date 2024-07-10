using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using WebApi.Data;
using WebApi.Domain.Interface;
using WebApi.Dtos.PolicyDtos;
using WebApi.Dtos.PostDtos;
using WebApi.Testing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.Domain.Service
{
    public class PolicyService : IPolicyService
    {
        private readonly MainDbContext _context;
        private readonly IDataAccess _dataAccess;

        public PolicyService(MainDbContext context, IDataAccess dataAccess)
        {
            _context = context;
            _dataAccess = dataAccess;
        }

        public async Task<bool> CreatePolicy(PolicyCreateDto policyDto)
        {
            try
            {
                // Generate a Policy ID
                string policyId = await GeneratePolicyIdAsync();
                if (!string.IsNullOrEmpty(policyId))
                {
                    // Map PolicyCreateDto to the Policy entity
                    var policyEntity = new TblPolicy
                    {
                        PolicyId = policyId,
                        PolicyHolderName = policyDto.PolicyHolderName,
                        PolicyStartDate = policyDto.PolicyStartDate,
                        PolicyEndDate = policyDto.PolicyEndDate,
                        IsActive = policyDto.IsActive
                    };

                    _context.Add(policyEntity);
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<string> GeneratePolicyIdAsync()
        {
            try
            {
                string spName = "sp_generate_policy_id";
                var result = await _dataAccess.GetData<string, object>(spName, new { });

                if (result != null && result.Any())
                {
                    return result.First();
                }

                return null!; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null!;
            }
        }

        public async Task<IEnumerable<TblPolicy>> GetPolicies()
        {
            var policies = await _context.TblPolicies
                             .Where(p => p.IsActive)
                             .ToListAsync();
                        
            return policies;
        }
    }
}
