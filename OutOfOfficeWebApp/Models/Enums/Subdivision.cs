using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum SubdivisionEnum
    {
        CustomerExperience = 1,
        GlobalOperations = 2,
        InnovativeSolutions = 3
    }

    public class Subdivision : EnumTable<SubdivisionEnum>
    {
        public Subdivision(SubdivisionEnum @enum) : base(@enum)
        {
            // call base constructor
        }

        public Subdivision() { }

        public static implicit operator Subdivision(SubdivisionEnum @enum) => new Subdivision(@enum);

        public static implicit operator SubdivisionEnum(Subdivision role) => (SubdivisionEnum)role.Id;
    }
}
