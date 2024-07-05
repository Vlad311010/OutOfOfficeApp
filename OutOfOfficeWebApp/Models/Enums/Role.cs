using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum RoleEnum
    {
        Employee = 0,
        ProjectManager = 1,
        HRManager = 2,
        Administrator = 3
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
