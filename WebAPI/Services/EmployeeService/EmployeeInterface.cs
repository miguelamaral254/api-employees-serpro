using WebAPI.Models;

namespace WebAPI.Services.EmployeeService;

public interface EmployeeInterface
{
    // async method
    Task<ServiceResponse<List<EmployeeModel>>> CreateEmployee(EmployeeModel newEmployee);

    Task<ServiceResponse<EmployeeModel>> ReadEmployee(int id);

    Task<ServiceResponse<List<EmployeeModel>>> UpdateEmployee(EmployeeModel UpdatedEmployee);

    Task<ServiceResponse<List<EmployeeModel>>> DeleteEmployee(int id);

    Task<ServiceResponse<List<EmployeeModel>>> GetEmployees();

    Task<ServiceResponse<List<EmployeeModel>>> ShutDownEmployee(int id);
}