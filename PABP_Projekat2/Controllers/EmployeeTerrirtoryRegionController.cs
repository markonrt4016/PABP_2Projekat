using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index(int? regionId, string? territoryDescription)
        {
            var employeeTerritories = _context.EmployeeTerritories.ToList();
            var Territories =  _context.Territories.ToList();
            var Regions = _context.Regions.ToList();


            var rez = from et in employeeTerritories
                      group et.EmployeeId by et.TerritoryId into g
                      select new EmployeesByTerritoriesRegions { EmployeeCount = g.Count(), TerritoryId = g.Key, TerritoryDescription = Territories.Where(x => x.TerritoryId == g.Key).FirstOrDefault().TerritoryDescription, RegionId = Regions.Where(x => x.RegionId == Territories.Where(x => x.TerritoryId == g.Key).First().RegionId).First().RegionId , RegionDescription = Regions.Where(x => x.RegionId == Territories.Where(x => x.TerritoryId == g.Key).FirstOrDefault().RegionId).FirstOrDefault().RegionDescription };

            ViewBag.Regions = new SelectList(_context.Regions, "RegionId", "RegionDescription");

            if(regionId != null && territoryDescription != null)
            {
                rez = rez.Where(x=> (x.TerritoryDescription != null && x.TerritoryDescription.Contains(territoryDescription)) && x.RegionId == regionId);
                return View(rez);
            }


            return View(rez);
        }
    }
}