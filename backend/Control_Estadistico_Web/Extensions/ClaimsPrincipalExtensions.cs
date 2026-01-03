using System;
using System.Security.Claims;

namespace Control_Estadistico_Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetCompanyId(this ClaimsPrincipal user)
        {
            var companyIdClaim = user.FindFirst("company_id");

            if (companyIdClaim == null)
                throw new UnauthorizedAccessException("company_id claim not found");

            return Guid.Parse(companyIdClaim.Value);
        }
    }
}
