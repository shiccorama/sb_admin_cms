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
    public class CountryController : Controller
    {


        #region  Fields
        private readonly IMapper NewIMapper;
        private readonly ICountryMirror NewCountryMirror;
        private readonly ICityMirror NewCityMirror;
        private readonly IDistrictMirror NewDistrictMirror;


        #endregion


        #region  Ctor

        public CountryController(IMapper injected_newIMapper, ICountryMirror injected_newCountryMirror, ICityMirror injected_newCityMirror, IDistrictMirror injected_newDistrictMirror)
        {
            this.NewIMapper = injected_newIMapper;
            this.NewCountryMirror = injected_newCountryMirror;
            this.NewCityMirror = injected_newCityMirror;
            this.NewDistrictMirror = injected_newDistrictMirror;
        }


        #endregion


        #region Actions


        public IActionResult Index()
        {
            return View();
        }


        // let's make (create) Crud operation :

        #region create country operation

        [HttpGet]

        public IActionResult CreateCountry()
        {
            var allCountries = NewCountryMirror.Read_All_Func();
            ViewBag.CountryList = new SelectList(allCountries, "Id", "CountryName");
            return View();
        }

        [HttpPost]

        public IActionResult CreateCountry(CountryVM P_CountryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var convertedToEntity = NewIMapper.Map<Country>(P_CountryVM);
                    NewCountryMirror.Create_Row_Func(convertedToEntity);
                    return RedirectToAction("Index", "Country");

                }

                return View(P_CountryVM);
            }
            catch (Exception ex)
            {
                var allCountries = NewCountryMirror.Read_All_Func();
                ViewBag.CountryList = new SelectList(allCountries, "Id", "CountryName");
                return View(P_CountryVM);
            }

        }
        #endregion


        #region create city operation

        [HttpGet]

        public IActionResult CreateCity()
        {
            var allCities = NewCityMirror.Read_All_Func();
            ViewBag.CityList = new SelectList(allCities, "Id", "CityName");
            return View();
        }

        [HttpPost]

        public IActionResult CreateCity(CityVM P_CityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var convertedToEntity = NewIMapper.Map<City>(P_CityVM);
                    NewCityMirror.Create_Row_Func(convertedToEntity);
                    return RedirectToAction("Index", "Country");

                }

                return View(P_CityVM);
            }
            catch (Exception ex)
            {
                var allCities = NewCityMirror.Read_All_Func();
                ViewBag.CityList = new SelectList(allCities, "Id", "CityName");
                return View(P_CityVM);
            }

        }
        #endregion


        #region create District operation

        [HttpGet]

        public IActionResult CreateDistrict()
        {

        
            var allDistricts = NewDistrictMirror.Read_All_Func();
            ViewBag.DistrictList = new SelectList(allDistricts, "Id", "DistrictName");

            return View();
        }

        [HttpPost]

        public IActionResult CreateDistrict(DistrictVM P_DistrictVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var convertedToEntity = NewIMapper.Map<District>(P_DistrictVM);
                    NewDistrictMirror.Create_Row_Func(convertedToEntity);
                    return RedirectToAction("Index", "Country");

                }

                return View(P_DistrictVM);
            }
            catch (Exception ex)
            {
                var allDistricts = NewDistrictMirror.Read_All_Func();
                ViewBag.DistrictList = new SelectList(allDistricts, "Id", "DistrictName");
                return View(P_DistrictVM);
            }

        }
        #endregion




        #endregion




    }
}
