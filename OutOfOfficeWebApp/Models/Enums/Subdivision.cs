using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum SubdivisionEnum
    {
        CustomerExperience = 0,
        GlobalOperations = 1,
        InnovativeSolutions = 2,
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
