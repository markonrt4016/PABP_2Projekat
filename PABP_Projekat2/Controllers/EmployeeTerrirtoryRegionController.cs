using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PABP_Projekat2.Models;

namespace PABP_Projekat2.Controllers
{
    public class EmployeeTerritoryRegionController : Controller
    {
        private readonly NorthwindContext _context;

        public EmployeeTerritoryRegionController(NorthwindContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employeeTerritories = _context.EmployeeTerritories.ToList();
            var Territories =  _context.Territories.ToList();
            var Regions = _context.Regions.ToList();


            var rez = from et in employeeTerritories
                      group et.EmployeeId by et.TerritoryId into g
                      select new EmployeesByTerritoriesRegions { EmployeeCount = g.Count(), TerritoryId = g.Key, TerritoryDescription = Territories.Where(x => x.TerritoryId == g.Key).FirstOrDefault().TerritoryDescription, RegionDescription = Regions.Where(x => x.RegionId == Territories.Where(x => x.TerritoryId == g.Key).FirstOrDefault().RegionId).FirstOrDefault().RegionDescription };




            return View(rez);
        }
    }
}