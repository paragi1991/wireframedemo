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

    public class RequestViewModel
    {
        public long residenceId { get; set; }
      
    }

    public class RequestProcessViewModel
    {
        public long residenceId { get; set; }
        public string status { get; set; }

        public string ContactNumber { get; set; }
    }

    public class ReviewRequestModel
    {
        public long residenceId { get; set; }
        public string feedback { get; set; }
        public decimal weight { get; set; }
        public decimal rating { get; set; }

    }


    public enum RequestStatus
    {
        Requested = 0,
        Accepted =1 ,
        Rejected = 2
    }
}
