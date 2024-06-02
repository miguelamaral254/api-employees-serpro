using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.Enums;
using WebAPI.Models;
using WebAPI.Services.EmployeeService;
using Xunit;

namespace WebAPI.Application.Tests.Services
{
    public class EmployeeServiceTests : IDisposable
    {
        private readonly EmployeeService _service;
        private readonly ApplicationDbContext _context;

        public EmployeeServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new EmployeeService(_context);
        }

        [Fact]
        public async Task CreateEmployee_ShouldAddEmployee()
        {
            var newEmployee = new EmployeeModel { Id = 1, Name = "John", LastName = "Doe", Department = DepartmentEnum.IT, Active = true, Shift = ShiftEnum.Morning };

            var result = await _service.CreateEmployee(newEmployee);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Contains(result.Data, e => e.Id == newEmployee.Id);
        }

        [Fact]
        public async Task ReadEmployee_ShouldReturnEmployee()
        {
            var newEmployee = new EmployeeModel { Id = 2, Name = "Jane", LastName = "Doe", Department = DepartmentEnum.IT, Active = true, Shift = ShiftEnum.Morning };
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();

            var result = await _service.ReadEmployee(newEmployee.Id);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(newEmployee.Id, result.Data.Id);
        }
        [Fact]
        public async Task UpdateEmployee_ShouldUpdateEmployee()
        {
            // Arrange
            var newEmployee = new EmployeeModel { Id = 3, Name = "Alice", LastName = "Smith", Department = DepartmentEnum.Finance, Active = true, Shift = ShiftEnum.Evening };
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();

            var updatedEmployee = new EmployeeModel { Id = newEmployee.Id, Name = "Alice", LastName = "Brown", Department = DepartmentEnum.Finance, Active = true, Shift = ShiftEnum.Night };

            // Act
            var result = await _service.UpdateEmployee(updatedEmployee);

            // Assert
            Assert.True(result.Success); // Verifica se a operação foi bem-sucedida
            Assert.NotNull(result.Data); // Verifica se os dados de retorno não são nulos

            // Verifica se os dados do funcionário foram atualizados corretamente
            var updated = _context.Employees.FirstOrDefault(e => e.Id == newEmployee.Id);
            Assert.NotNull(updated); // Verifica se o funcionário foi encontrado no banco de dados
            Assert.Equal(updated.LastName, updatedEmployee.LastName); // Verifica se o sobrenome foi atualizado corretamente
            Assert.Equal(updated.Shift, updatedEmployee.Shift); // Verifica se o turno foi atualizado corretamente
        }

        [Fact]
        public async Task DeleteEmployee_ShouldDeleteEmployee()
        {
            var newEmployee = new EmployeeModel { Id = 4, Name = "Tom", LastName = "Jones", Department = DepartmentEnum.RH, Active = true, Shift = ShiftEnum.Night };
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();

            var result = await _service.DeleteEmployee(newEmployee.Id);
            var deleted = _context.Employees.FirstOrDefault(e => e.Id == newEmployee.Id);

            Assert.True(result.Success);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnAllEmployees()
        {
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel { Id = 5, Name = "Michael", LastName = "Johnson", Department = DepartmentEnum.Service, Active = true, Shift = ShiftEnum.Morning },
                new EmployeeModel { Id = 6, Name = "Jessica", LastName = "Davis", Department = DepartmentEnum.IT, Active = true, Shift = ShiftEnum.Evening }
            };

            _context.Employees.AddRange(employees);
            _context.SaveChanges();

            var result = await _service.GetEmployees();

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(employees.Count, result.Data.Count);
        }

        [Fact]
        public async Task ShutDownEmployee_ShouldDeactivateEmployee()
        {
            var newEmployee = new EmployeeModel { Id = 7, Name = "William", LastName = "Wilson", Department = DepartmentEnum.Purchasing, Active = true, Shift = ShiftEnum.Night };
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();

            var result = await _service.ShutDownEmployee(newEmployee.Id);
            var deactivated = _context.Employees.FirstOrDefault(e => e.Id == newEmployee.Id);

            Assert.True(result.Success);
            Assert.NotNull(deactivated);
            Assert.False(deactivated.Active);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
