using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wastearn.Models;
using Wastearn.ViewModel;

namespace Wastearn.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly WastearnEntities _db = new WastearnEntities();

        [Route("registration/firststep")]
        [HttpPost]
        // POST api/values
        public HttpResponseMessage RegistrationFirstStep([FromBody]RegistrationViewModel registrationViewModel)
        {

            Registration registration = new Registration
            {
                ResidenceId = registrationViewModel.SelectedResidence,
                SocietyId = registrationViewModel.SelectedSociety,
                CreatedOn = DateTime.Now
            };
            _db.Registrations.Add(registration);
            _db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created);

        }

        [Route("register/secondstep")]
        [HttpPost]
        // POST api/values
        public HttpResponseMessage Register([FromBody]RegisterViewModel registerViewModel)
        {
            var dataExists = _db.Registrations.Where(s => s.ResidenceId == registerViewModel.SelectedResidence && s.SocietyId == registerViewModel.SelectedSociety).FirstOrDefault();

            if (dataExists != null)
            {
                if (dataExists.ContactNumber == null)
                {
                    dataExists.ContactNumber = registerViewModel.ContactNumber;
                    OtpManagement(registerViewModel);
                    _db.SaveChanges();

                }
                else if (dataExists.ContactNumber != registerViewModel.ContactNumber)
                {
                    Registration registration = new Registration
                    {
                        ResidenceId = registerViewModel.SelectedResidence,
                        SocietyId = registerViewModel.SelectedSociety,
                        ContactNumber = registerViewModel.ContactNumber,
                        CreatedOn = DateTime.Now

                    };
                    _db.Registrations.Add(registration);
                    OtpManagement(registerViewModel);
                    _db.SaveChanges();
                }
                else
                {
                    HttpResponseMessage associatedResponse = Request.CreateResponse(HttpStatusCode.OK, "Alreday Associated this number");
                    return associatedResponse;
                }

            }
            else
            {
                Registration registration = new Registration
                {
                    ResidenceId = registerViewModel.SelectedResidence,
                    SocietyId = registerViewModel.SelectedSociety,
                    ContactNumber = registerViewModel.ContactNumber,
                    CreatedOn = DateTime.Now

                };
                _db.Registrations.Add(registration);
                OtpManagement(registerViewModel);
                _db.SaveChanges();
            }
            // otp send logic needs to be write here 
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, registerViewModel);
            return response;

        }

        private void OtpManagement(RegisterViewModel registerViewModel)
        {
            string sRandomOTP = GenerateRandomOTP();

            OtpHistory otpHistory = new OtpHistory
            {
                ContactNumber = registerViewModel.ContactNumber,
                Otp = sRandomOTP,
                OtpExpired = DateTime.Now.AddMinutes(7),

            };
            _db.OtpHistories.Add(otpHistory);
        }

        [Route("otpverify/thirdstep")]
        [HttpPost]
        // POST api/values
        public HttpResponseMessage Otpverification([FromBody]OTPVerificationViewModel otpVerificationViewModel)
        {
            var dataExists = _db.OtpHistories.Where(s => s.ContactNumber == otpVerificationViewModel.ContactNumber).FirstOrDefault();

            if (dataExists != null)
            {
                if (dataExists.OtpExpired >= DateTime.Now)
                {
                    if (dataExists.Otp == otpVerificationViewModel.OTP)
                    {
                        dataExists.IsVerified = true;
                        dataExists.VerifiedTime = DateTime.Now;
                    }
                    var masterData = _db.Registrations.Where(s => s.ContactNumber == otpVerificationViewModel.ContactNumber).FirstOrDefault();
                    if (masterData != null)
                    {
                        masterData.IsOtpVerified = true;
                    }

                }
                else
                {
                    _db.OtpHistories.Remove(dataExists);
                }
                _db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [Route("setpassword/forthstep")]
        [HttpPost]
        // POST api/values
        public HttpResponseMessage SetPassword([FromBody]MPINViewModel mpinviewModel)
        {
            var dataExists = _db.Registrations.Where(s => s.ContactNumber == mpinviewModel.ContactNumber && s.IsOtpVerified == true).FirstOrDefault();

            if (dataExists != null)
            {
                dataExists.MPIN = mpinviewModel.MPIN;
                _db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [Route("login")]
        [HttpPost]
        // POST api/values
        public HttpResponseMessage Login([FromBody]MPINViewModel mpinviewModel)
        {
            var dataExists = _db.Registrations.Where(s => s.ContactNumber == mpinviewModel.ContactNumber && s.MPIN == mpinviewModel.MPIN).FirstOrDefault();

            if (dataExists != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Login success");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Authentication Fail"); ;
        }
        [Route("pickuprequest")]
        [HttpPost]
        public HttpResponseMessage PickUpRequest([FromBody]RequestViewModel requestViewModel)
        {
            var dataExists = _db.Registrations.Where(s => s.ResidenceId == requestViewModel.residenceId).FirstOrDefault();

            if (dataExists != null)
            {
                var requstedExists = _db.Requests.Where(s => s.residentid == requestViewModel.residenceId && s.status == Convert.ToString(RequestStatus.Requested)).FirstOrDefault();

                if (requstedExists == null)
                {
                    Request request = new Request
                    {
                        residentid = requestViewModel.residenceId,
                        requestedOn = DateTime.Now,
                        status = RequestStatus.Requested.ToString()

                    };
                    _db.Requests.Add(request);
                }
                else
                {
                    requstedExists.requestedOn = DateTime.Now;
                }
                _db.SaveChanges();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Residence information not found in system"); ;

            }

            return Request.CreateResponse(HttpStatusCode.OK, "Request has been put successfully");
        }

        [Route("requestprocess")]
        [HttpPost]
        public HttpResponseMessage RequestProcess([FromBody]RequestProcessViewModel requestProcessViewModel)
        {
            var dataExists = _db.Registrations.Where(s => s.ResidenceId == requestProcessViewModel.residenceId && s.ContactNumber == requestProcessViewModel.ContactNumber).FirstOrDefault();

            if (dataExists != null)
            {

                var requstedExists = _db.Requests.Where(s => s.residentid == requestProcessViewModel.residenceId && s.status == Convert.ToString(RequestStatus.Requested)).FirstOrDefault();
                if (requstedExists != null)
                {
                    if (requstedExists.status == RequestStatus.Accepted.ToString())
                    {
                        requstedExists.status = requestProcessViewModel.status;
                        requstedExists.completedBy = 1;// TODO Needs to be dynamic
                        requstedExists.completedOn = DateTime.Now;
                    }
                    requstedExists.status = requestProcessViewModel.status;
                    _db.SaveChanges();
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no pending request"); ;

                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Residence information not found in system"); ;

            }

            return Request.CreateResponse(HttpStatusCode.OK, "Request has been successfully process");
        }
        [Route("reviewrequest")]
        [HttpPost]
        public HttpResponseMessage ReviewRequest([FromBody]ReviewRequestModel reviewRequestModel)
        {
            var requstedExists = _db.Requests.Where(s => s.residentid == reviewRequestModel.residenceId && s.status == Convert.ToString(RequestStatus.Accepted)).OrderByDescending(i => i.requestedOn).FirstOrDefault();
            if (requstedExists != null && requstedExists.rating == 0)
            {

                requstedExists.feedback = reviewRequestModel.feedback;
                requstedExists.weight = reviewRequestModel.weight;
                requstedExists.rating = reviewRequestModel.rating;
                _db.SaveChanges();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no pending request"); ;

            }
            return Request.CreateResponse(HttpStatusCode.OK, "Review has been submitted successfully"); ;
        }
        [Route("societies")]
        [HttpGet]
        public HttpResponseMessage getSocieties()
        {
            var societyData = _db.Society.ToList();          
            return Request.CreateResponse(HttpStatusCode.OK, societyData); ;
        }
        [Route("residence/{societyid}")]
        [HttpGet]
        public HttpResponseMessage getResidenceBySociety(int societyid)
        {
            var selectedData = _db.Residence.Where(s => s.SocietyId == societyid).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, selectedData); ;
        }
        private string GenerateRandomOTP()

        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            int iOTPLength = 8;

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
    }
}
