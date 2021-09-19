using Microsoft.AspNetCore.Mvc;
using sb_light_admin.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using sb_light_admin.BL.Interfaces;
using AutoMapper;

namespace sb_light_admin.view.Controllers
{
    public class MailsController : Controller
    {

        #region
        #endregion


        #region Fields

        // note that we take instance from (I-Employee) not (Employee) directly to make this connection (loosely coupled).
        private readonly IMailsRep MailsRep;

        // let's take instance from mapper, because here in controller you should map between Mirrors and Entities :

        private readonly IMapper newIMapper;

        #endregion


        #region Ctor

        public MailsController(IMailsRep injected_newIMailsRep, IMapper injected_newIMapper)
        {
            this.MailsRep = injected_newIMailsRep;
            this.newIMapper = injected_newIMapper;
        }

        #endregion



        [HttpGet]
        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(MailsVM model)
        {

            //try
            //{
            //    //use disposable with initialize new smtp client instance :
            //    using (var newSmtpClient = new SmtpClient("smtp.gmail.com", 587))
            //    {
            //        newSmtpClient.EnableSsl = true;
            //        newSmtpClient.Credentials = new NetworkCredential("as8338873@gmail.com", "A@123321A@");
            //        newSmtpClient.Send("as8338873@gmail.com", "shiccorama@gmail.com", Title, Message);

            //    }

            //    TempData["confirmedMessage"] = "Mail Sent";
            //    return View();

            //}

            //catch (Exception ex)
            //{
            //    TempData["confirmedMessage"] = "Mail Failed";
            //    return View();

            //}

            //_________________________________________________________________________________


            using (var newSmtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                newSmtpClient.EnableSsl = true;
                newSmtpClient.Credentials = new NetworkCredential("pythonat.sheriff@gmail.com", "put here your password");
                newSmtpClient.Send("pythonat.sheriff@gmail.com", "sheriff71180@gmail.com", model.Title, model.Message);
            }

            TempData["confirmedMessage"] = "Mail Sent";
            return View();

        }
    }
}
