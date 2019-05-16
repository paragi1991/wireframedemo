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
        public string ContactNumber { get; set; }
    }


    public class OTPVerificationViewModel
    {     
        public string ContactNumber { get; set; }

        public string OTP { get; set; }
    }

    public class MPINViewModel
    {
        public string ContactNumber { get; set; }

        public int? MPIN { get; set; }
    }

    public class LoginViewModel
    {
        public string ContactNumber { get; set; }
        public int MPIN { get; set; }
    }

}