using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PABP_Projekat2.Models
{
    public class EmployeesByTerritoriesRegions
    {
        public int EmployeeCount { get; set; }
        public string TerritoryId { get; set; }
        public string TerritoryDescription { get; set; }
        public string RegionDescription { get; set; }
    }
}
