using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum RoleEnum
    {
        Employee = 1,
        ProjectManager = 2,
        HRManager = 3,
        Administrator = 4
    }

    public class Role : EnumTable<RoleEnum>
    {
        public Role(RoleEnum @enum) : base(@enum)
        {
            // call base constructor
        }

        public Role() { }

        public static implicit operator Role(RoleEnum @enum) => new Role(@enum);

        public static implicit operator RoleEnum(Role role) => (RoleEnum)role.Id;
    }
}
