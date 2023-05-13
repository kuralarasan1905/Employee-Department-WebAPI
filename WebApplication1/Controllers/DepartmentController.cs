using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Entities;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly TestDbcontext _dbcontext;
        public DepartmentController(TestDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("Getall")]
        public IActionResult Getall()
        {
            try
            {
                var department = _dbcontext.Department.ToList();
                return Ok(department);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{DepartmentId}")]
        public IActionResult GetParticularDepartment(int DepartmentId)
        {
            try
            {
                var result = new ResponseBase();
                var department = _dbcontext.Department.FirstOrDefault(x => x.DepartmentId == DepartmentId);
                if (department != null)
                {
                    return Ok(department);
                }
                else
                {
                    result.ErrorMessage = "Department is not found";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody]DepartmentRequest request)
        {
            try
            {
                var result = new ResponseBase();
                if (string.IsNullOrEmpty(request.DepartmentName))
                {
                    result.ErrorMessage = "Enter a DepartmentName..";
                    return Ok(result);
                }
                if (string.IsNullOrEmpty(request.Location))
                {
                    result.ErrorMessage = "Enter a Location...";
                    return Ok(result);
                }

                Department department = new Department();
                department.DepartmentName = request.DepartmentName;
                department.Location = request.Location;

                _dbcontext.Department.Add(department);
                _dbcontext.SaveChanges();

                result.SuccessMessage = "Department created successfully.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Update/{DepartmentId}")]
        public IActionResult UpdateDepartment([FromBody]DepartmentRequest request, [FromRoute]int DepartmentId)
        {
            try
            {
                var result = new ResponseBase();
                var department = _dbcontext.Department.Where(x => x.DepartmentId == DepartmentId).FirstOrDefault();
                if (department != null)
                {
                    if (string.IsNullOrEmpty(request.DepartmentName))
                    {
                        result.ErrorMessage = "Enter a DepartmentName";
                        return Ok(result);
                    }
                    if (request.DepartmentName.Length > 50)
                    {
                    result.ErrorMessage = "Please enter DepartmentName less than 50 characters.";
                    }
                    if (string.IsNullOrEmpty(request.Location))
                    {
                        result.ErrorMessage = "Enter a Location";
                        return Ok(result);
                    }
                    department.DepartmentName = request.DepartmentName;
                    department.Location = request.Location;

                    _dbcontext.Department.Update(department);
                    _dbcontext.SaveChanges();

                    result.SuccessMessage = "Update is successfull...";
                    return Ok(result);
                }

                result.ErrorMessage = "Department is not found.";
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Delete/{DepartmentId}")]
        public IActionResult DeleteDepartment([FromRoute]int DepartmentId)
        {
            try
            {
                var result = new ResponseBase();
                var employees = _dbcontext.Employee.Where(x => x.DepartmentId == DepartmentId).ToList();
                if(employees.Count > 0)
                {
                    result.ErrorMessage = "Some Employees are working in this department. Please move them into another department and then try to delete.";
                    return Ok(result);                    
                }
                else
                {
                    var department = _dbcontext.Department.Where(x => x.DepartmentId == DepartmentId).FirstOrDefault();
                    if (department != null)
                    {
                        _dbcontext.Department.Remove(department);
                        _dbcontext.SaveChanges();

                        result.SuccessMessage = "Department Deleted Successfully.";
                        return Ok(result);
                    }

                    result.ErrorMessage = "Department is not found.";
                    return Ok(result);
                }               

            }

            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }
    }


}
