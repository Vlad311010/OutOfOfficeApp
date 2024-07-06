using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum ProjectTypeEnum
    {
        Inner = 1,
        Commercial = 2
    }

    public class ProjectType : EnumTable<ProjectTypeEnum>
    {
        public ProjectType(ProjectTypeEnum @enum) : base(@enum) 
        {
            // call base constructor
        }

        public ProjectType() {  }

        public static implicit operator ProjectType(ProjectTypeEnum @enum) => new ProjectType(@enum);

        public static implicit operator ProjectTypeEnum(ProjectType type) => (ProjectTypeEnum)type.Id;
    }
}
