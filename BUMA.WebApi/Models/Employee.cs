using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BUMA.WebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public DateTime LastModified { get; set; }
    }
}
