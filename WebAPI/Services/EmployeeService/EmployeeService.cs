using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DataContext;
using WebAPI.Enums;
using WebAPI.Models;

namespace WebAPI.Services.EmployeeService
{
    public class EmployeeService : EmployeeInterface
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> CreateEmployee(EmployeeModel newEmployee)
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();

            try
            {
                if (newEmployee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Enter with data!";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                _context.Add(newEmployee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeeModel>> ReadEmployee(int id)
        {
            ServiceResponse<EmployeeModel> serviceResponse = new ServiceResponse<EmployeeModel>();
            try
            {
                EmployeeModel employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "User not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    serviceResponse.Data = employee;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> UpdateEmployee(EmployeeModel updatedEmployee)
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();

            try
            {
                EmployeeModel employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == updatedEmployee.Id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Employee not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    employee.ModifiedDate = DateTime.Now.ToLocalTime();
                    employee.Name = updatedEmployee.Name;
                    employee.LastName = updatedEmployee.LastName;
                    employee.Department = updatedEmployee.Department;
                    employee.Active = updatedEmployee.Active;
                    employee.Shift = updatedEmployee.Shift;

                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Employees.ToList();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> DeleteEmployee(int id)
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();

            try
            {
                EmployeeModel employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Employee not found!";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> GetEmployees()
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();

            try
            {
                serviceResponse.Data = await _context.Employees.ToListAsync();
                serviceResponse.Message = serviceResponse.Data.Count == 0 ? "There are no employees to show yet" : "";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<EmployeeModel>>> ShutDownEmployee(int id)
        {
            ServiceResponse<List<EmployeeModel>> serviceResponse = new ServiceResponse<List<EmployeeModel>>();
            try
            {
                EmployeeModel employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Employee not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    employee.Active = false;
                    employee.ModifiedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Employees.ToList();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
    }
}
