using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum ActiveStatusEnum
    {
        Incative = 0,
        Active = 1
    }

    public class ActiveStatus : EnumTable<ActiveStatusEnum>
    {
        public ActiveStatus(ActiveStatusEnum @enum) : base(@enum)
        {
            // call base constructor
        }

        public ActiveStatus() { }

        public static implicit operator ActiveStatus(ActiveStatusEnum @enum) => new ActiveStatus(@enum);

        public static implicit operator ActiveStatusEnum(ActiveStatus type) => (ActiveStatusEnum)type.Id;
    }
}
