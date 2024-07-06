using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq.Expressions;
using System.Security.Claims;

namespace OutOfOfficeWebApp.Utils
{
    public static class Extensions
    {
        public static string GetRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                return "";
            }

            Claim? roleClaim = principal.Claims.SingleOrDefault(c => c.Type.EndsWith("role", true, null));

            if (roleClaim == null)
            {
                return String.Empty;
            }
            return roleClaim.Value;
        }        
    }
}
