using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sb_light_admin.BL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace sb_light_admin.view.Controllers
{
    public class AccountsController : Controller
    {


        #region Fields


        private readonly IMapper iMapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        #endregion




        #region Ctor

        public AccountsController(IMapper _IMapper, UserManager<IdentityUser> _UserManager, SignInManager<IdentityUser> _SignInManager)
        {
            iMapper = _IMapper;
            userManager = _UserManager;
            signInManager = _SignInManager;
        }


        #endregion


        #region sign in
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ActionName("SignIn")]
        public async Task<IActionResult> SignInPost(SignInVM model)
        {

        
            try
            {
                if (ModelState.IsValid)
                {


                    var UserData = new IdentityUser()
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }


                        else
                        {
                            ModelState.AddModelError("", "Invalid Email or Password");
                        }


                }
                else
                {
                    return View();
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return View();
        }

        #endregion


        #region log off
        [HttpPost]
        public IActionResult LogOff()
        {

            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return View();
        }


        #endregion


        #region register
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterPost(RegisterVM model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    //var UserData = iMapper.Map<IdentityUser>(model);

                    var UserData = new IdentityUser()
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var result = await userManager.CreateAsync(UserData);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }

                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
           
            return View();
        }
        #endregion


        #region forget password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ForgetPassword")]
        public IActionResult ForgetPasswordPost(ForgetPasswordVM model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return View();
        }

        #endregion


        #region confirm forget password
        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        #endregion


        #region reset password
        [HttpGet]
        public IActionResult ResetPassword()
        {


            return View();
        }

        [HttpPost]
        [ActionName("ResetPassword")]
        public IActionResult ResetPasswordPost(ResetPasswordVM model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return View();
        }

        #endregion


        #region confirm reset password
        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        #endregion


        #region index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {

            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return View();
        }


        #endregion




    }
}
