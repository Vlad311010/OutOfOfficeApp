using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using OutOfOfficeWebApp.Interfaces;
using OutOfOfficeWebApp.Models;
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
                return String.Empty;
            }

            Claim? roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (roleClaim == null)
            {
                return String.Empty;
            }
            return roleClaim.Value;
        }

        public static string GetIdentifier(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return String.Empty;

            var identificatorClaim = principal.Claims.FirstOrDefault(c => c.Type == "Identificator");

            return identificatorClaim == null ? String.Empty : identificatorClaim.Value;
        }

        public static bool IsInManagerRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return false;

            
            Claim? roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            if (roleClaim == null)
                return false;

            return roleClaim.Value == "Administrator" || roleClaim.Value == "HRManager" || roleClaim.Value == "ProjectManager";
        }


        public static async Task<Employee?> GetActiveEmployee(this ClaimsPrincipal principal, IEmployeesRepository employeeRepo)
        {
            var identificatorClaim = principal.Claims.FirstOrDefault(c => c.Type == "Identificator");
            if (identificatorClaim == null || String.IsNullOrEmpty(identificatorClaim.Value))
                return null;

            Employee loggedinEmployee = await employeeRepo.GetById(Int32.Parse(identificatorClaim.Value));
            return loggedinEmployee;
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

        public static IFormFile ToFormFile(this byte[] byteArray)
        {
            var stream = new MemoryStream(byteArray);
            return new FormFile(stream, 0, byteArray.Length, null!, "formFile")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image"
            };
        }
    }
}
