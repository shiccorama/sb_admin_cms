using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using sb_light_admin.BL.Interfaces;
using sb_light_admin.BL.Repositories;
using sb_light_admin.view.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sb_light_admin.view.Controllers
{
    public class DepartmentController : Controller
    {
        #region Fields
        //taking instance from Repositories : 

        private readonly IDepartmentMirror newDepartmentMirror1;
        private readonly IStringLocalizer<SharedResource> localizer;


        //private readonly IMapper newIMapper;

        #endregion

        #region Ctor

        //making constructor and dependency injection into startup file :
        public DepartmentController(IDepartmentMirror injected_newDepartmentMirror1, IStringLocalizer<SharedResource> _Localizer)
        {
            this.newDepartmentMirror1 = injected_newDepartmentMirror1;
            this.localizer = _Localizer;
        }

   



        //public DepartmentController(IMapper injected_IMapper)
        //{
        //    this.newIMapper = injected_IMapper;

        //}

        #endregion

        #region  Actions

        [HttpGet]
        public IActionResult DepartmentCard()
        {

            return View();
        }

        // let's make (Read) Crud operation :

        #region Read Operation


        // this function to read (get) all the content of the database :

        [HttpGet]
        public IActionResult Read()
        {
            var allData = newDepartmentMirror1.Read_All_Func();

            ViewBag.TargetMsg = localizer["Try Searching by the search field in table"];

            //var mappedData = newIMapper.Map<IEnumerable<DepartmentMirror>>(allData);

            return View(allData);

            //return View(mappedData);
        }


        //crud operations should have (get-post) functions except (read or retrieve) which has (get) function only.
        // every crud operation should have its own view (even if it will not be a page, and it will only be a function to use)

        #endregion

        // let's make (create) Crud operation :

        #region create operation

        [HttpGet]

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]

        public IActionResult Create(DepartmentMirror arg_DepartmentMirror)
        {

            try
            {
                if (ModelState.IsValid == true)
                {
                    newDepartmentMirror1.Create_Row_Func(arg_DepartmentMirror);

                    return RedirectToAction("Read", "Department");
                }

                return View(arg_DepartmentMirror);
            }

            catch (Exception arg_execption)
            {
                return View(arg_DepartmentMirror);
            }

        }
        #endregion 

        // let's make (Update) Crud operation :

        #region   update operation

        [HttpGet]
        public IActionResult Update(int Id)
        {
            var selectedRow = newDepartmentMirror1.Read_By_Id_Func(Id);

            return View(selectedRow);
        }

        [HttpPost]

        public IActionResult Update(DepartmentMirror arg_DepartmentMirror)
        {

            try
            {
                if (ModelState.IsValid == true)
                {
                    newDepartmentMirror1.Update_Row_Func(arg_DepartmentMirror);

                    return RedirectToAction("Read", "Department");
                }

                return View(arg_DepartmentMirror);
            }

            catch (Exception arg_execption)
            {
                return View(arg_DepartmentMirror);
            }

        }
        #endregion 

        // let's make (Delete) Crud operation :

        # region delete operation

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var selectedRow = newDepartmentMirror1.Read_By_Id_Func(Id);

            return View(selectedRow);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteRow(int Id)
        {
            try

            {
                newDepartmentMirror1.Delete_Row_Func(Id);

                return RedirectToAction("Read", "Department");
            }



            catch (Exception arg_execption)
            {
                var selectedRow = newDepartmentMirror1.Read_By_Id_Func(Id);
                return View(selectedRow);
            }

        }
        #endregion

        // let's make (Details) Crud operation :

        #region details operation

        [HttpGet]
        public IActionResult Details(int id)
        {

            var detailsData = newDepartmentMirror1.Read_By_Id_Func(id);

            return View(detailsData);

        }

        #endregion

        // let's make (search) button :

        #region search operation


        //[HttpGet]
        //public IActionResult Search()
        //{
        //        return View();
        //}

        [HttpPost]
        [ActionName("Search")]
        public IActionResult SearchName(string searchField)
        {
            if (searchField == "" || searchField == null)

            {
                return RedirectToAction("Read", "Department");
            }

            else
            {
                var allData = newDepartmentMirror1.Search_All_Func(searchField);
                return View(allData);
            }

        }

        #endregion



        #endregion


    }
}
