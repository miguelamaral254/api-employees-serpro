using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services.EmployeeService;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeInterface _employeeInterface;

    public EmployeeController(EmployeeInterface employeeInterface)
    {
        _employeeInterface = employeeInterface;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> GetEmployees()
    {
        return Ok(await _employeeInterface.GetEmployees());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<EmployeeModel>>> ReadEmployee(int id)
    {
        ServiceResponse<EmployeeModel> serviceResponse = await _employeeInterface.ReadEmployee(id);

        return Ok(serviceResponse);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> CreateEmployee(EmployeeModel newEmployee)
    {
        return Ok(await _employeeInterface.CreateEmployee(newEmployee));
    }
    
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> UpdateEmployee(EmployeeModel updatedEmployee)
    {
        ServiceResponse<List<EmployeeModel>> serviceResponse = await _employeeInterface.UpdateEmployee(updatedEmployee);
        return Ok(serviceResponse);
    }
    [HttpPut("shutDownEmployee")]

    public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> ShutDownEmployee(int id)
    {
        ServiceResponse<List<EmployeeModel>> serviceResponse = await _employeeInterface.ShutDownEmployee(id);

        return Ok(serviceResponse);
    }

    [HttpDelete]
    public async Task<ActionResult<ServiceResponse<List<EmployeeModel>>>> DeleteEmployee(int id)
    {
        ServiceResponse<List<EmployeeModel>> serviceResponse = await _employeeInterface.DeleteEmployee(id);

        return Ok(serviceResponse);
    }
}
