using Microsoft.VisualBasic;

namespace WebApplication1.Dtos
{
    public class EmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Dob { get; set; }
        public decimal AnnualSalary { get; set; }
    }
}
