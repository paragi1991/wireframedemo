using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wastearn.ViewModel
{
    public class RegistrationViewModel
    {
        public int SelectedSociety { get; set; }
        public int SelectedResidence { get; set; }
    }

    public class RegisterViewModel
    {
        public int SelectedSociety { get; set; }
        public int SelectedResidence { get; set; }
        public Int64 ContactNumber { get; set; }
    }


    public class OTPVerificationViewModel
    {     
        public Int64 ContactNumber { get; set; }

        public string OTP { get; set; }
    }

    public class MPINViewModel
    {
        public Int64 ContactNumber { get; set; }

        public decimal? MPIN { get; set; }
    }
}