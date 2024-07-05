﻿using OutOfOfficeWebApp.Utils;

namespace OutOfOfficeWebApp.Models.Enums
{
    public enum AbsenceReasonEnum
    {
        None,
        SickLeave,
        PersonalLeave,
        Vacation,
        UnpaidLeave,
        WorkFromHome
    }

    public class AbsenceReason : EnumTable<AbsenceReasonEnum>
    {
        public AbsenceReason(AbsenceReasonEnum @enum) : base(@enum)
        {
            // call base constructor
        }

        public AbsenceReason() { }

        public static implicit operator AbsenceReason(AbsenceReasonEnum @enum) => new AbsenceReason(@enum);

        public static implicit operator AbsenceReasonEnum(AbsenceReason reason) => (AbsenceReasonEnum)reason.Id;
    }
}
