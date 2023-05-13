using Microsoft.VisualBasic;

namespace WebApplication1.Dtos
{
    public class UpdateEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public DateTime Dob { get; set; }
        public int DepartmentId { get; set; }   
        public decimal AnnualSalary { get; set; }

    }
}
