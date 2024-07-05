using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum RequestStatusEnum
    {
        New = 0
    }

    public class RequestStatus : EnumTable<RequestStatusEnum>
    {
        public RequestStatus(RequestStatusEnum @enum) : base(@enum)
        {
            // call base constructor
        }

        public RequestStatus() { }

        public static implicit operator RequestStatus(RequestStatusEnum @enum) => new RequestStatus(@enum);

        public static implicit operator RequestStatusEnum(RequestStatus status) => (RequestStatusEnum)status.Id;
    }
}
