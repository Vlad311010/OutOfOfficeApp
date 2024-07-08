using Microsoft.AspNetCore.Mvc.Rendering;
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
            return Regex.Replace(str, @"(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])", " ");
        }

        public static IEnumerable<SelectListItem>SetSelectedOption(this IEnumerable<SelectListItem> selectLists, string? value) 
        {
            value ??= string.Empty;
            foreach (SelectListItem listItem in selectLists)
            {
                listItem.Selected = listItem.Value == value;
            }
            return selectLists;
        }
    }
}
