using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Entities;
using WebApplication1.Helpers;



namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly TestDbcontext _testdbcontext;
        public EmployeeController(TestDbcontext testDbcontext)
        {
            _testdbcontext = testDbcontext;
        }

        [HttpPost("Create")]
        public IActionResult AddEmployee([FromBody]EmployeeRequest request)
        {
            try
            {
                var result = new ResponseBase();

                if (string.IsNullOrEmpty(request.FirstName.Trim()))
                {
                    result.ErrorMessage = "Enter a first name.";
                    return Ok(result);
                }
                if (request.FirstName.Trim().Length > 50)
                {
                    result.ErrorMessage = "Please enter first name less than 50 characters.";
                    return Ok(result);
                }
                if (string.IsNullOrEmpty(request.LastName.Trim()))
                {
                    result.ErrorMessage = "Enter a last name.";
                    return Ok(result);
                }
                if (request.LastName.Trim().Length > 50)
                {
                    result.ErrorMessage = "Please enter last name less than 50 characters.";
                    return Ok(result);
                }
                if (request.AnnualSalary <= 0)
                {
                    result.ErrorMessage = "Please enter valid annual salary";
                    return Ok(result);
                }

                Employee employee = new Employee();
                employee.FirstName = request.FirstName;
                employee.LastName = request.LastName;
                employee.DateOfBirth = request.Dob;
                employee.DepartmentId = request.DepartmentId;
                employee.AnnualSalary = request.AnnualSalary;

                _testdbcontext.Employee.Add(employee);
                _testdbcontext.SaveChanges();
                result.SuccessMessage = "insert Successfull";
                return Ok( result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("GetallEmployees")]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var response = from d in _testdbcontext.Department
                               join e in _testdbcontext.Employee
                               on d.DepartmentId equals e.DepartmentId
                               select new EmployeeDetail
                               {

                                   EmployeeId = e.EmployeeId,
                                   FirstName = e.FirstName,
                                   LastName = e.LastName,
                                   DepartmentName = d.DepartmentName,
                                   DateOfBirth = e.DateOfBirth,
                                   AnnualSalary = e.AnnualSalary,
                                   Location = d.Location
                               };

                var getall = response.ToList().OrderBy(x => x.LastName);

                return Ok(getall);

            }
            catch (Exception)
            {

                return BadRequest("sorry employees  is not found");
            }
        }

        [HttpGet("Getbyid/{id}")]
        public IActionResult GetParticularEmployee([FromRoute]int id)
        {
            try
            {

                var response = from d in _testdbcontext.Department
                               join e in _testdbcontext.Employee
                               on d.DepartmentId equals e.DepartmentId
                               where e.EmployeeId == id
                               select new EmployeeDetail
                               {

                                   EmployeeId = e.EmployeeId,
                                   FirstName = e.FirstName,
                                   LastName = e.LastName,
                                   DateOfBirth = e.DateOfBirth,
                                   AnnualSalary = e.AnnualSalary,
                                   DepartmentId = e.DepartmentId,
                                   DepartmentName = d.DepartmentName,
                                   Location = d.Location
                               };

                var employeeDetail = response.FirstOrDefault();
                return Ok(employeeDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Update/{EmployeeId}")]
        public IActionResult UpdateEmployee([FromRoute]int EmployeeId, [FromBody] UpdateEmployeeRequest request)
        {
            try
            {
                var result = new ResponseBase();
                var employees = _testdbcontext.Employee.Where(x => x.EmployeeId == EmployeeId).FirstOrDefault();
                if (employees != null)
                {
                    if (string.IsNullOrEmpty(request.FirstName.Trim()))
                    {
                        result.ErrorMessage = "Enter a first name.";
                        return Ok(result);
                    }
                    if (request.FirstName.Trim().Length > 50)
                    {
                        result.ErrorMessage = "Please enter first name less than 50 characters.";
                        return Ok(result);
                    }
                    if (string.IsNullOrEmpty(request.LastName.Trim()))
                    {
                        result.ErrorMessage = "Enter a last name.";
                        return Ok(result);
                    }
                    if (request.LastName.Trim().Length > 50)
                    {
                        result.ErrorMessage = "Please enter last name less than 50 characters.";
                        return Ok(result);
                    }
                    if (request.AnnualSalary <= 0)
                    {
                        result.ErrorMessage = "Please enter valid annual salary";
                        return Ok(result);
                    }

                    employees.FirstName = request.FirstName;
                    employees.LastName = request.LastName;
                    employees.DateOfBirth = request.Dob;
                    employees.DepartmentId = request.DepartmentId;
                    employees.AnnualSalary = request.AnnualSalary;
                    

                    _testdbcontext.Employee.Update(employees);
                    _testdbcontext.SaveChanges();
                    result.SuccessMessage = "Employees Update Successfull";
                    return Ok(result);
                }

                result.ErrorMessage = "Employees is not found.";
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }

        [HttpDelete("Delete/{EmployeeId}")]
        public IActionResult DeleteEmployee([FromRoute]int EmployeeId)
        {
            try
            {
                var result = new ResponseBase();
                var employee =  _testdbcontext.Employee.Where(x => x.EmployeeId == EmployeeId).FirstOrDefault();
                if (employee != null)
                {
                    _testdbcontext.Employee.Remove(employee);
                    _testdbcontext.SaveChanges();

                    result.SuccessMessage = " Employee deleted successfully.";
                    return Ok(result);

                }

                result.ErrorMessage = "Employee not found.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }

}


