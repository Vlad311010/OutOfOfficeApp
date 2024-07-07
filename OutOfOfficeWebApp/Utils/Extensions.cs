using System.Security.Claims;
using System.Text.RegularExpressions;

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

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(str, "(\\B[A-Z]+?(?=[A-Z][^A-Z])|\\B[A-Z]+?(?=[^A-Z]))", " $1");
        }
    }
}
