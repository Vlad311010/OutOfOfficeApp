using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum ActiveStatusEnum
    {
        Incative = 1,
        Active = 2
    }

    public class ActiveStatus : EnumTable<ActiveStatusEnum>
    {
        public ActiveStatus(ActiveStatusEnum @enum) : base(@enum)
        {
            // call base constructor
        }

        public ActiveStatus() { }

        
    }
}
