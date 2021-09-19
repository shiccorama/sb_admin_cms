using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace sb_light_admin.view.AllConfigurationFiles
{
    public class AllConfigFiles
    {

        #region
        #endregion

        #region  Kestral :
        //var host = new WebHostBuilder();
        //host.UseKestrel()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseIISIntegration()
        //        .UseStartup<Startup>()
        //        .Build()
        //        .Run();

        #endregion

        #region add tag helper at _ViewImports file:

        //@addTagHelper*,Microsoft.AspNetCore.Mvc.TagHelpers
        #endregion

        #region add Layout shared view to _ViewStart file:

        //        @{
        //    Layout = "~/Views/Layout/MainLayout.cshtml";
        //}


        #endregion

        #region add Mapper and Create DomainProfile in BL :



        //        1. Add Packages
        // - Auto Mapper
        // - Auto Mapper Dependency Injection
        //for both > .View & > .BL


        //2. Create Domain Profile Class in .BL(Business layer)


        //3. Add The Following Configuration at Start Up ConfigureServices For DI Configuration With AutoMapper:

        //  - services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));


        //4. Inject All Mapped Class + ClassVM In Profile Class Constructor:

        //            CreateMap<Department, DepartmentMirror>();
        //            CreateMap<DepartmentMirror, Department>();


        //5. Inject IMapper In Your((Controller))

        //       private readonly IMapper newIMapper;

        //        public YourController(IMapper injected_IMapper)
        //        {
        //            this.newIMapper = injected_IMapper;
        //        }


        //6. Map Your Objects Like:
        // -  newIMapper.Map<Entity>(Mirror);

        #endregion

        #region Connection String Configuration :

        //services.AddDbContextPool<SharpDbContext>(opts => 
        //    opts.UseSqlServer(Configuration.GetConnectionString("SharpDbConnection")));

        #endregion

        #region  NewtonSoft config :

        //        - Download
        //  Newtonsoft for .net core

        //- add the below in startup
        // services.AddControllersWithViews().AddNewtonsoftJson(opt=> {
        //            opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
        //        });
        #endregion

        #region Important link and DataTable :

        //  https://www.ag-grid.com/example.php   >>>> best suit for angular

        //https://www.telerik.com/support/demos   >>>> price tag

        //https://www.devexpress.com/support/demos/    >>>> price tag

        //https://www.jtable.org/  >>>> for free

        //https://datatables.net/  >>>> for free

        //https://www.stimulsoft.com/en >>> specifically for reporting

        //https://www.infragistics.com/
        #endregion

        #region remove files and photos of the employee from database :


        //        if (System.IO.File.Exists(Directory.GetCurrentDirectory() + FolderName + FileName))
        //{
        //    System.IO.File.Delete(Directory.GetCurrentDirectory() + FolderName + FileName);
        //}


        #endregion

        #region Send mail options :


        //        - Send Mail
        //============
        //1- Using :
        //-system.Net;
        //-system.Net.Mail;  
        //=============
        //2- Login To Account By Using :
        //-Host  ==> For Test "smtp.gmail.com"
        //-Port  ==> For Test "587" "secure and free"  
        //-Credentials(UserName , Password) "Authentication"
        //-EnableSSL (Secure socket Layer)
        //=============
        //3- Send Mail Using Send("From","To","Title","Body")

        //To
        //Title
        //Message

        // Sender (Source) "Host  + Port"              =>     "Host  + Port"    Reciever (Destination)



        #endregion

        #region  How to Upload files :


        //1 )  // Get Directory
        //            string FolderPath = Directory.GetCurrentDirectory() + FolderName;

        // 2)  // Get File Name
        //            string FileName = Guid.NewGuid() + Path.GetFileName(fileUrl.FileName);

        // 3)  // Merge Path with File Name
        //            string FinalPath = Path.Combine(FolderPath, FileName);

        // 4)  // Save File As Streams "Data Overtime"
        //            using (var Stream = new FileStream(FinalPath, FileMode.Create))
        //            {
        //                fileUrl.CopyTo(Stream);
        //            }
        #endregion

        #region Globalization and Localization :

        //        A ) Create Resource File To Save Your Text

        //B ) At Startup Add All Needed Configuration Needs To Localization

        //==> In Configuration Services

        //- 1  __ this is will be added after services.AddControllersWithViews() :
        //.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        //.AddDataAnnotationsLocalization(options =>
        //                {
        //                    options.DataAnnotationLocalizerProvider = (type, factory) =>
        //                        factory.Create(typeof(SharedResource));
        //                }); 

        //==> In Configure((in middleware or pipeline))

        //- 2 

        //var supportedCultures = new[] {
        //      new CultureInfo("ar-EG"),
        //      new CultureInfo("en-US"),
        //};

        //-3
        //            app.UseRequestLocalization(new RequestLocalizationOptions
        //            {
        //                DefaultRequestCulture = new RequestCulture("en-US"),
        //                SupportedCultures = supportedCultures,
        //                SupportedUICultures = supportedCultures,
        //                RequestCultureProviders = new List<IRequestCultureProvider>
        //                {
        //                new QueryStringRequestCultureProvider(),
        //                new CookieRequestCultureProvider()
        //                }
        //});


        //C ) At View_Imports Add :

        //@using Microsoft.Extensions.Localization
        //@using WebApplication7.Resource

        //@inject IStringLocalizer<SharedResource> SharedLocalizer

        //D ) Replace Your Text By 
        //@SharedLocalizer["Key"]


        //E ) In Your Layout

         //<a href = "#" >< i class="fa fa-gear fa-fw"></i> @SharedLocalizer["Globalization"]</a>
         //                   <a class="dropdown-item d-flex justify-content-between" href="@Url.Action("SetLanguage", "Home", new {culture = "en-US", returnUrl = Context.Request.Path + Context.Request.QueryString})">
         //                                 <span> English</span>
         //                   </a >
         //                   <a class= "dropdown-item d-flex justify-content-between" href = "@Url.Action("SetLanguage", "Home", new {culture = "ar-EG", returnUrl = Context.Request.Path + Context.Request.QueryString})">
         //                                 <span > Arabic</span>
         //                   </a >


        //       F) At Your Controller

        //        [HttpGet]
        //public IActionResult SetLanguage(string culture, string returnUrl)
        //{
        //    Response.Cookies.Append(
        //        CookieRequestCultureProvider.DefaultCookieName,
        //        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        //        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        //    );

        //    return LocalRedirect(returnUrl);
        //}


        //=============================================
        //@{
        //var Culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        //}

        //@if (Culture == "ar-EG")


        //IStringLocalizer<SharedResource> localizer
        #endregion

        #region user identity configuration :


        //        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        //            {
        //                // Default Password settings.
        //                options.Password.RequireDigit = true;
        //                options.Password.RequireLowercase = true;
        //                options.Password.RequireNonAlphanumeric = true;
        //                options.Password.RequireUppercase = true;
        //                options.Password.RequiredLength = 6;
        //                options.Password.RequiredUniqueChars = 0;
        //            }).AddEntityFrameworkStores<SharpDbContext>()
        //            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

        //app.UseAuthentication();
        #endregion

        #region  Registration :

        //    var user = new IdentityUser()
        //    {
        //        UserName = model.Email,
        //        Email = model.Email
        //    };

        //    var result = await userManager.CreateAsync(user, model.Password);

        //                if (result.Succeeded)
        //                {
        //                   return RedirectToAction("Login");
        //}
        //                else
        //                {
        //                    foreach (var item in result.Errors)
        //                    {
        //                        ModelState.AddModelError("", item.Description);
        //                    }
        //                }
        #endregion

        #region  Login :

        //    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        //                if (result.Succeeded)
        //                {
        //                    return RedirectToAction("Index","Home");
        //}
        //                else
        //                {
        //                   ModelState.AddModelError("", "Invalid UserName Or Password");

        //                }
        #endregion

        #region  Sign Out :


        //        Sign Out Button
        //==================
        //      <form asp-controller="Account" asp-action="LogOff">
        //           <input type="submit" class="btn btn-primary" value="Sign Out" />
        //     </form>


        //          await signInManager.SignOutAsync();
        //            return RedirectToAction("Login");



        //==================================================

        //Welcome User 
        //==============

        // @using Microsoft.AspNetCore.Identity;

        //@inject SignInManager<IdentityUser> signInManager

        //                    @if (signInManager.IsSignedIn(User))
        //                    {
        //                        <div class="email">@User.Identity.Name</div>
        //                    }

        #endregion

        #region  Forget Password :

        //    var user = await userManager.FindByEmailAsync(model.Email);

        //            if (user != null)
        //            {
        //                var token = await userManager.GeneratePasswordResetTokenAsync(user);

        //    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

        //    MailSender.Mail("Password Reset", passwordResetLink);

        //                //logger.Log(LogLevel.Warning, passwordResetLink);

        //                return RedirectToAction("ConfirmForgetPassword");
        //}

        //            return RedirectToAction("ConfirmForgetPassword");

        #endregion

        #region Reset Password :

        //        var user = await userManager.FindByEmailAsync(model.Email);

        //                if (user != null)
        //                {
        //                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

        //                    if (result.Succeeded)
        //                    {
        //                        return RedirectToAction("ConfirmResetPassword");
        //    }

        //                    foreach (var error in result.Errors)
        //                    {
        //                        ModelState.AddModelError("", error.Description);
        //                    }

        //return View(model);
        //                }

        //                return RedirectToAction("ConfirmResetPassword");

        #endregion

        #region  login with Google and FaceBook :

        //        From 104 To 109
        //===============

        //https://www.youtube.com/watch?v=ZgPK51X5BGw&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=105


        #endregion

        #region Microsoft Identity :

        //        - Security(Authentication - Authorization)
        //===================================
        //1 - Identification (Registration) - Collect For Data
        //     User  -  Membership


        //2 - Authentication ( Who Are You , Where You Come - "Local - Active Directory - External Server "Domain Server" - Federated Server")
        //    Login


        //3 - Authorization (  What Can I Do )
        //    Roles (Group Of Users) 



        //============================================

        //1) Custom Security

        // 1 - Roles (ID  -  RoleName)
        // 2 - Users (ID - UseName - Password , ..... )
        // 3 - UserInRole (ID - RoleID  -  UserID)


        //  ---------------- Static Data -----------------------

        // - Menu (ID -  MenuName)
        // - SubMenu (ID -  SubMenuName - SubMenuUrl )
        // - SubInMenu (ID  - MenuID - SubID - UerInRoleID)



        //=============================================

        // 2) Microsoft Identity (ASP.NET Core)


        // - Identification 
        //     ( Create USer - Update - Delete - Read Info. - Account Confirmation ) - UserManager (IdentityUser)


        // - Authentication 
        //    ( SignIn - SignOut - IsSigned - Password Recovery - Two Factor Authentication - OTP Authentication - Third Party Ex. Google - FB ) - SignInManager (IdentityUser)


        // - Authorization 
        //    ( Create Role  -  Update -  Delete - Read Info. ) - RoleManager(IdentityRoles)



        //=============================================

        // - IdentityContext

        // - UserManager<IdentityUser> ==> Belong To User
        // - SignInManager<IdentityUser>   ==> - SignIn - SignOut - IsSigned
        // - RoleManager<IdentityRole> ==> Belong To Role

        //Reference :
        //    https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-5.0



        #endregion

        #region  API config :

        //        1) Enable Controllers

        //   services.AddControllers();

        //2) Map Routing

        //  app.UseEndpoints(a => a.MapDefaultControllerRoute());

        #endregion

        #region  Enable Swagger :

        //        1) At ConfigureServices
        //        services.AddSwaggerGen();


        //2) At Configure
        //        app.UseSwagger();
        //         app.UseSwaggerUI(c =>
        //            {
        //                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //            });



        #endregion

        #region   Enable the CORS policy :

        //        1) At Configure Service

        //            services.AddCors();


        //2) At Configure


        //            app.UseCors(options => options
        //            .AllowAnyOrigin()
        //            .AllowAnyMethod()
        //            .AllowAnyHeader());
        #endregion

        #region Create Server Side API :

        //        - What Is API?
        //- Create Server Side API
        //- Test API "Post Man"
        //- Security

        //- Documentation
        //- Document API Using Swagger UI

        //- Enable Cors Policy

        //CORS => Cross Orgine Resource Sharing


        //===========================

        //Cod : "" ,
        //Status : "",
        //Msg : "" ,
        //Data : [
        //{
        //  Id : "",
        //  Name : ""
        //} ,
        //{
        //  Id : "",
        //  Name : ""
        //} ,

        //]
        #endregion

        #region  Database for EF core :

                //        Existing Database With EF Core
                //https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx
                //Http://www.learnentityframeworkcore.com/


        #endregion


        #region
        #endregion


        #region
        #endregion


        #region
        #endregion





























    }
}
