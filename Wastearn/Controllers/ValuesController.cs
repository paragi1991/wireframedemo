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

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        [Route("registration/firststep")]
        [HttpPost]
        // POST api/values
        public HttpResponseMessage RegistrationFirstStep([FromBody]RegistrationViewModel registrationViewModel)
        {

            Registration registration = new Registration
            {
                ResidenceId = registrationViewModel.SelectedResidence,
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
            var dataExists = _db.Registrations.Where(s => s.ResidenceId == registerViewModel.SelectedResidence).FirstOrDefault();

            if (dataExists != null && dataExists.ContactNumber != null)
            {
                Registration registration = new Registration
                {
                    ResidenceId = registerViewModel.SelectedResidence,
                    ContactNumber = registerViewModel.ContactNumber
                };
                _db.Registrations.Add(registration);
                _db.SaveChanges();

                // otp send logic needs to be write here 
            }
            return Request.CreateResponse(HttpStatusCode.Created);

        }
        //[Route("otpverify/thirdstep")]
        //[HttpPost]
        //// POST api/values
        //public HttpResponseMessage Otpverification([FromBody]OTPVerificationViewModel otpVerificationViewModel)
        //{
        //    var dataExists = _db.Registrations.Where(s => s.ContactNumber == otpVerificationViewModel.ContactNumber).FirstOrDefault();

        //    if (dataExists != null && dataExists.OT != null)
        //    {
        //        Registration registration = new Registration
        //        {
        //            ResidenceId = registerViewModel.SelectedResidence,
        //            ContactNumber = registerViewModel.ContactNumber
        //        };
        //        _db.Registrations.Add(registration);
        //        _db.SaveChanges();
        //    }
        //    return Request.CreateResponse(HttpStatusCode.Created);

        //}

        [Route("setpassword/forthstep")]
        [HttpPost]
        // POST api/values
        public HttpResponseMessage SetPassword([FromBody]MPINViewModel mpinviewModel)
        {
            var dataExists = _db.Registrations.Where(s => s.ContactNumber == mpinviewModel.ContactNumber).FirstOrDefault();

            if (dataExists != null)
            {
                dataExists.MPIN = mpinviewModel.MPIN;
                _db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Created);

        }
    }
}
