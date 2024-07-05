using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum PositionEnum
    {
        Employee = 0,
        ProjectManager = 1,
        HRManager = 2
    }

    public class Position : EnumTable<PositionEnum>
    {
        public Position(PositionEnum @enum) : base(@enum)
        {
            // call base constructor
        }

        public Position() { }

        public static implicit operator Position(PositionEnum @enum) => new Position(@enum);

        public static implicit operator PositionEnum(Position faculty) => (PositionEnum)faculty.Id;
    }
}
