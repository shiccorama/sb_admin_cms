using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using sb_light_admin.BL.Helper;
using sb_light_admin.BL.Interfaces;
using sb_light_admin.BL.Models;
using sb_light_admin.BL.Repositories;
using sb_light_admin.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sb_light_admin.view.Controllers
{
    public class EmployeeController : Controller
    {

        #region
        #endregion


        #region Fields

        // note that we take instance from (I-Employee) not (Employee) directly to make this connection (loosely coupled).
        private readonly IEmployeeMirror newEmployeeMirror;

        // let's take instance from mapper, because here in controller you should map between Mirrors and Entities :

        private readonly IMapper newIMapper;

        //taking instance from Repositories : 

        private readonly IDepartmentMirror newDepartmentMirror1;

        private readonly ICountryMirror newCountryMirror;

        private readonly ICityMirror newCityMirror;

        private readonly IDistrictMirror newDistrictMirror;



        #endregion


        #region Ctor

        public EmployeeController(IEmployeeMirror injected_newEmployeeMirror, IMapper injected_newIMapper, IDepartmentMirror injected_newDepartmentMirror1, ICountryMirror injected_newCountryMirror, ICityMirror injected_newCityMirror, IDistrictMirror injected_newDistrictMirror)
        {
            this.newEmployeeMirror = injected_newEmployeeMirror;
            this.newIMapper = injected_newIMapper;
            this.newDepartmentMirror1 = injected_newDepartmentMirror1;


            this.newCountryMirror = injected_newCountryMirror;
            this.newCityMirror = injected_newCityMirror;
            this.newDistrictMirror = injected_newDistrictMirror;
        }

        #endregion


        #region  Actions

        // let's make (Read) Crud operation :

        #region Read all tables operation
        public IActionResult Index(string searchField = "")

        {


            if (searchField == "" || searchField == null)

            {
                // first, define a variable to hold all (((entities))) of database :
                var allTable = newEmployeeMirror.Read_All_Func();

                // now, let's convert from Entity to mirror to enable controller to read it :
                //you can read it like this : var convertedToMirror is equal to the instance of newIMapper which has built-in function
                //called Map which respectively mapping Tuple or collection of EmployeeMirror with entities allTable :
                var convertedToVM = newIMapper.Map<IEnumerable<EmployeeVM>>(allTable);

                // finally, take the converted value of mirrors and show it in view();

                //return RedirectToAction("Index", "Employee");
                return View(convertedToVM);
            }

            else
            {
                var searchedField = newEmployeeMirror.Search_All_Func(searchField);
                var convertToVM = newIMapper.Map<IEnumerable<EmployeeVM>>(searchedField);
                return View(convertToVM);
            }
        }

        #endregion

        // let's make (Details) Crud operation :

        #region details operation

        [HttpGet]
        public IActionResult Details(int id)
        {

            var rowDetailsData = newEmployeeMirror.Read_By_Id_Func(id);
            var convertedToVM = newIMapper.Map<EmployeeVM>(rowDetailsData);


            //here we need to check mapping :
            ViewBag.photo_details = rowDetailsData.Photo_Name;
            ViewBag.cv_details = rowDetailsData.CV_Name;

            //this is how we make a dynamic variable for a path:
            ViewBag.photo_path = $@"~/Files/Photos/{ViewBag.photo_details}";
            ViewBag.cv_path = $@"~/Files/Docs/{ViewBag.cv_details}";


            var allDepartments = newDepartmentMirror1.Read_All_Func();
            ViewBag.departmentList = new SelectList(allDepartments, "Id", "DepartmentName");

            return View(convertedToVM);
        }

        #endregion

        // let's make (search) button :

        #region  search operation


        [HttpGet]
        public IActionResult Search(string param_search)
        {
            return View();
        }

        [HttpPost]
        [ActionName("Search")]
        public IActionResult SearchName(string param_search)
        {
            if (param_search == "" || param_search == null)

            {
                return RedirectToAction("Index", "Employee");
            }

            else
            {
                var searchedField = newEmployeeMirror.Search_All_Func(param_search);
                var convertToVM = newIMapper.Map<EmployeeVM>(searchedField);
                return View(convertToVM);
            }

        }


        #endregion

        // let's make (create) Crud operation :

        #region create operation

        [HttpGet]

        public IActionResult Create()
        {
            // to show a drop down menu for departments when selecting new employee, you have to get :


            var allDepartments = newDepartmentMirror1.Read_All_Func();
            var allCountries = newCountryMirror.Read_All_Func();
            // I don't need to do all of these menus as long as I choose the parent menu above (i.e Countries) :

            //var allCities = newCityMirror.Read_All_Func();
            //var allDistricts = newDistrictMirror.Read_All_Func();

            // allDepartments variable carries all departments and then use built-in function
            // called (selectlist) by taking initiation (new) and then assign allDepartment var and
            // Id of the department and the department name and FYI , if one of these written by mistake
            // it won't work ... 
            ViewBag.departmentList = new SelectList(allDepartments, "Id", "DepartmentName");

            ViewBag.CountryList = new SelectList(allCountries, "Id", "CountryName");
            // I don't need to do all of these menus as long as I choose the parent menu above (i.e Countries) :

            //ViewBag.CityList = new SelectList(allDepartments, "Id", "CityName");

            //ViewBag.DistrictList = new SelectList(allDepartments, "Id", "DistrictName");

            return View();
        }

        [HttpPost]

        public IActionResult Create(EmployeeVM param_EmployeeVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //#region photo upload

                    //// first, you need to get the path of your folder on the server and then add /wwwroot/ to get into Files folder :
                    //// note : by using System.IO , you can manipulate any files on your server and "Directory" is a namespace of it.
                    //// don't forget to add (/) in the end of the path to avoid concatenating wrong (/Photoscats.jpg):

                    //var photoPath = Directory.GetCurrentDirectory() + "/wwwroot/Files/Photos/";

                    ////second, get file name of the photo "the one that it uploaded with" without your intervention :
                    ////as you may know that photo_form includes (type-size-name) of a file, this enables a method called (FileName):
                    //// another thing to mention, you have to make auto-generation Guid.NewGuid() method to avoid duplicate files:
                    //// note, auto-generation must be before photoName, not after it, because if it puts after, it will affect extension only.
                    //// path.GetFileName() method removes any token names added by browsers and only get the pure uploaded name of the photo :

                    //var photoName = Guid.NewGuid() + Path.GetFileName(param_EmployeeMirror.Photo_Form.FileName);

                    ////third, merge the path with the name to get the total file path, like (/wwwroot/Files/Photos + cats.jpg) :

                    //var totalPhotoPath = photoPath + photoName ;

                    //// if you forget to add (/) or you want to use method to do it for you , you can use :
                    //// var totalPhotoPath = Path.Combine(photoPath , photoName);
                    //// stream means (data over time) like youtube videos, once they received data, once they sent it to you .

                    ////forth and last step is to save the uploaded photo to your specific totalPhotoPath by using stream object in disposable function using() :

                    //using (var stream = new FileStream(totalPhotoPath, FileMode.Create))
                    //{
                    //    param_EmployeeMirror.Photo_Form.CopyTo(stream);
                    //}

                    //#endregion

                    //#region CV upload

                    //var CVPath = Directory.GetCurrentDirectory() + "/wwwroot/Files/Docs";

                    //var CVName = Guid.NewGuid() + Path.GetFileName(param_EmployeeMirror.CV_Form.FileName);

                    ////note that CV put outside Docs because there is a security issue with using Path.Combine();

                    //var totalCVPath = Path.Combine(CVPath + CVName);

                    //using (var stream = new FileStream(totalCVPath, FileMode.Create))
                    //{
                    //    param_EmployeeMirror.CV_Form.CopyTo(stream);
                    //}

                    //#endregion

                    var photoName = FileUploader.UploadFile("/Files/Photos/", param_EmployeeVM.Photo_Form);

                    var CVName = FileUploader.UploadFile("/Files/Docs/", param_EmployeeVM.CV_Form);


                    var convertedToEntity = newIMapper.Map<Employee>(param_EmployeeVM);

                    // note that we need to map any entity we have before creating any row in data and just after taking the conversion of our new mapper as (convertedToEntity) above :
                    // adding map for photo_entity_name to photo_guid_uploaded_name :

                    convertedToEntity.Photo_Name = photoName;

                    // adding map for CV_entity_name to CV_guid_uploaded_name :

                    convertedToEntity.CV_Name = CVName;


                    newEmployeeMirror.Create_Row_Func(convertedToEntity);


                    return RedirectToAction("Index", "Employee");

                }

                return View(param_EmployeeVM);
            }
            catch (Exception ex)
            {
                var allDepartments = newDepartmentMirror1.Read_All_Func();
                var allCountries = newCountryMirror.Read_All_Func();

                // I don't need to do all of these menus as long as I choose the parent menu above (i.e Countries) :
                //var allCities = newCityMirror.Read_All_Func();
                //var allDistricts = newDistrictMirror.Read_All_Func();


                ViewBag.departmentList = new SelectList(allDepartments, "Id", "DepartmentName");
                ViewBag.CountryList = new SelectList(allCountries, "Id", "CountryName");

                // I don't need to do all of these menus as long as I choose the parent menu above (i.e Countries) :
                //ViewBag.CityList = new SelectList(allDepartments, "Id", "CityName");
                //ViewBag.DistrictList = new SelectList(allDepartments, "Id", "DistrictName");
                //ViewBag.CountryList = new SelectList(country.Get(), "Id", "CountryName");

                return View(param_EmployeeVM);
            }

        }
        #endregion

        // let's make (update) Crud operation :

        #region   update operation

        [HttpGet]
        public IActionResult Update(int id)
        {
            var rowDetailsData = newEmployeeMirror.Read_By_Id_Func(id);
            var convertedToVM = newIMapper.Map<EmployeeVM>(rowDetailsData);

            // now, let's get the drop down menu of the departments and let's get the value
            // of the assigned department as (selected) by using convertedToMirror.DepartmentId 
            // as a 4th parameter for the built-in function SelectList :

            var allDepartments = newDepartmentMirror1.Read_All_Func();

            ViewBag.departmentList = new SelectList(allDepartments, "Id", "DepartmentName");
            // you can add (,convertedToMirror.DepartmentId) to the above line but just in case you fix the modal thing in index view.

            return View(convertedToVM);
        }

        [HttpPost]

        public IActionResult Update(EmployeeVM param_EmployeeVM)
        {

            try
            {
                if (ModelState.IsValid == true)
                {

                    var photoName = FileUploader.UploadFile("/Files/Photos/", param_EmployeeVM.Photo_Form);
                    var CVName = FileUploader.UploadFile("/Files/Docs/", param_EmployeeVM.CV_Form);
                    var convertedToEntity = newIMapper.Map<Employee>(param_EmployeeVM);


                    convertedToEntity.Photo_Name = photoName;
                    convertedToEntity.CV_Name = CVName;



                    newEmployeeMirror.Update_Row_Func(convertedToEntity);

                    return RedirectToAction("Index", "Employee");
                }

                return View(param_EmployeeVM);
            }

            catch (Exception arg_execption)
            {

                // you have to return allDepartments in catch, because the view needs values of dep.
                // once the exception done, otherwise it will thourgh exception for not found those
                // values.
                var allDepartments = newDepartmentMirror1.Read_All_Func();

                ViewBag.departmentList = new SelectList(allDepartments, "Id", "DepartmentName");
                // you can add (,convertedToMirror.DepartmentId) to the above line but just in case you fix the modal thing in index view.
                return View(param_EmployeeVM);
            }

        }
        #endregion

        // let's make (Delete) Crud operation :

        #region delete operation

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var rowDetailsData = newEmployeeMirror.Read_By_Id_Func(id);
            var convertedToVM = newIMapper.Map<EmployeeVM>(rowDetailsData);

            // now, let's get the drop down menu of the departments and let's get the value
            // of the assigned department as (selected) by using convertedToMirror.DepartmentId 
            // as a 4th parameter for the built-in function SelectList :

            var allDepartments = newDepartmentMirror1.Read_All_Func();

            ViewBag.departmentList = new SelectList(allDepartments, "Id", "DepartmentName");
            // you can add (,convertedToMirror.DepartmentId) to the above line but just in case you fix the modal thing in index view.

            return View(convertedToVM);
        }



        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteRow(int id)
        {
            try

            {
                var rowDetailsData = newEmployeeMirror.Read_By_Id_Func(id);

                //// this is used to delete photo and cv from database :
                ////if (File.Exists(Directory.GetCurrentDirectory() + FolderName + FileName))
                ////{
                ////    File.Delete(Directory.GetCurrentDirectory() + FolderName + FileName);
                ////}

                //if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/Photos/" + rowDetailsData.Photo_Name))
                //{
                //    System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Files/Photos/" + rowDetailsData.Photo_Name);
                //}

                ////note that we will use the same method but CV is out of Docs folder due to using method Path.Combine(); which results in security issue :

                //if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/" + rowDetailsData.CV_Name))
                //{
                //    System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/Files/" + rowDetailsData.CV_Name);
                //}



                FileUploader.RemoveFile("/Files/Photos/", rowDetailsData.Photo_Name);

                FileUploader.RemoveFile("/Files/Docs/", rowDetailsData.CV_Name);


                newEmployeeMirror.Delete_Row_Func(rowDetailsData);

                return RedirectToAction("Index", "Employee");
            }

            catch (Exception arg_execption)
            {
                var rowDetailsData = newDepartmentMirror1.Read_By_Id_Func(id);
                var convertedToMirror = newIMapper.Map<DepartmentMirror>(rowDetailsData);
                return View(convertedToMirror);
            }

        }


        #endregion



        #endregion

        #region  Ajax Call for Countries and Cities :

        // 1st, get all cities by countryId :
        // note : all Ajax calls must be httppost not httpget to avoid expose your data :
        // note : all Ajax calls are JsonResult function which returns Json or (object of objects) :


        [HttpPost]
        public JsonResult GetCityByCountryId (int param_CountryId)
        {
            var fetchedCity = newCityMirror.Read_All_Func().Where(a => a.CountryId == param_CountryId);

            return Json(fetchedCity);
        }




        // 2nd, get all districts by cityId :
        // note again :::: all Ajax calls must be httppost not httpget to avoid expose your data :


        [HttpPost]
        public JsonResult GetDistrictByCityId(int param_CityId)
        {
            var fetchedDistrict = newDistrictMirror.Read_All_Func().Where(a => a.CitytId == param_CityId);

            return Json(fetchedDistrict);
        }



        #endregion
















    }
}
