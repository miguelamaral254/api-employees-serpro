using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Enums;

namespace WebAPI.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DepartmentEnum Department { get; set; }
        public bool Active { get; set; }
        public ShiftEnum Shift { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime ModifiedDate { get; set; } = DateTime.Now.ToLocalTime();
    }
}
