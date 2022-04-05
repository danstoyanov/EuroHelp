using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Xunit;

using EuroHelp.Data;
using EuroHelp.Data.Models;
using EuroHelp.Services.Users;

namespace EuroHelp.Tests.Services
{
    public class UserServiceTests
    {
        private DbContextOptionsBuilder<EuroHelpDbContext> optionsBuilder;
        private EuroHelpDbContext db;
        private UserService userService;

        public UserServiceTests()
        {
            this.optionsBuilder = new DbContextOptionsBuilder<EuroHelpDbContext>()
                .UseInMemoryDatabase("TestDataBasse");
            this.db = new EuroHelpDbContext(optionsBuilder.Options);
            this.userService = new UserService(this.db);
        }

        [Fact]
        public void EmployeeIsCreatedCorecctlyAndReturnId()
        {
            // Arrange
            this.userService.CreateEmployee(
                "id123",
                "Mincho",
                "08888888");

            // Act
            var createdEmployee = this.db.Employees.FirstOrDefault(e => e.Id == "id123");

            // Assert
            Assert.Equal("id123", createdEmployee.Id);
            Assert.Equal("Mincho", createdEmployee.Name);
            Assert.Equal("08888888", createdEmployee.PhoneNumber);
            Assert.NotEqual("id13", createdEmployee.Id);
        }

        [Fact]
        public void ConsumerIsCreatedCorrectlyAndReturnId()
        {
            // Arrange
            this.userService.CreateConsumer(
                "consumer123",
                "consumer_123",
                "Ivan",
                "Ivanov",
                "Male"
                );

            // Act
            var createConsumer = this.db.Consumers.FirstOrDefault(e => e.Id == "consumer123");

            // Assert
            Assert.Equal("consumer123", createConsumer.Id);
            Assert.Equal("consumer_123", createConsumer.UserName);
            Assert.Equal("Ivan", createConsumer.FirstName);
            Assert.Equal("Ivanov", createConsumer.LastName);
            Assert.Equal("Male", createConsumer.Gender);
            Assert.NotEqual("Female", createConsumer.Gender);
            Assert.NotEqual("consumer", createConsumer.UserName);
        }

        [Fact]
        public void IsConsumerUsernameContainMethodIsReturnCurrectUserName()
        {
            // Arrange
            ClearData();
            FillData();

            var username1 = "consumer1";
            var username2 = "consumer2";
            var username3 = "consumerTest";

            // Act
            var result1 = this.userService.IsUsernameContains(username1);
            var result2 = this.userService.IsUsernameContains(username2);
            var result3 = this.userService.IsUsernameContains(username3);

            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.False(result3);

            ClearData();
        }

        [Fact]
        public void TheEmployeesCountIsCorrectAndReturnListOfEmployees()
        {
            // Arrange
            ClearData();
            FillData();

            // Act
            var employeesCount = this.userService.GetEmployees().Count();

            // Assert
            Assert.Equal(4, employeesCount);
            Assert.NotEqual(5, employeesCount);
            Assert.NotEmpty(this.userService.GetEmployees());
        }

        [Fact]
        public void TheConsumersCountIsCorrectAndReturnListofConsumers()
        {
            // Arrange
            ClearData();
            FillData();
            // Act
            var consumersCount = this.userService.GetConsumers().Count();

            // Assert
            Assert.Equal(3, consumersCount);
            Assert.NotEqual(5, consumersCount);
            Assert.NotEmpty(this.userService.GetConsumers());
        }

        [Fact]
        public void ChangeConsumerStatusToBanned()
        {
            ClearData();
            FillData();

            var testedEmployee = this.db.Employees
                .Where(u => u.Id == "1")
                .FirstOrDefault();

            var status = this.userService.ChangeEmployeeStatus(testedEmployee.Id);

            Assert.Equal("Active", status);
            Assert.NotEqual("Non active", status);

            status = this.userService.ChangeEmployeeStatus(testedEmployee.Id);

            Assert.Equal("Non active", status);
            Assert.NotEqual("Active", status);
        }

        [Fact]
        public void ChangeEmployeeStatusToBennaed()
        {
            ClearData();
            FillData();

            var testedConsumer = this.db.Consumers
                .Where(c => c.Id == "1")
                .FirstOrDefault();

            var status = this.userService.ChangeConsumerStatus(testedConsumer.Id);

            Assert.Equal("Banned", status);
            Assert.NotEqual("Active", status);

            status = this.userService.ChangeConsumerStatus(testedConsumer.Id);

            Assert.Equal("Active", status);
            Assert.NotEqual("Banned", status);
        }

        private void ClearData()
        {
            foreach (var employee in this.db.Employees)
            {
                this.db.Employees.Remove(employee);
            }

            foreach (var consumer in this.db.Consumers)
            {
                this.db.Consumers.Remove(consumer);
            }
        }

        private void FillData()
        {
            AddConsumers();
            AddEmployees();
        }

        private void AddEmployees()
        {
            var employees = new List<Employee>();

            employees.Add(new Employee
            {
                Id = "1",
                UserId = "1",
                Name = "Kornelia Ninova",
                PhoneNumber = "08888382",
            });

            employees.Add(new Employee
            {
                Id = "2",
                UserId = "2",
                Name = "Hristo Ivanov",
                PhoneNumber = "088881282",
            });

            employees.Add(new Employee
            {
                Id = "3",
                UserId = "3",
                Name = "Kiril Petkov",
                PhoneNumber = "08828382",
            });

            employees.Add(new Employee
            {
                Id = "4",
                UserId = "4",
                Name = "Asen Vasilev",
                PhoneNumber = "08881282",
            });

            this.db.Employees.AddRange(employees);
            this.db.SaveChanges();
        }

        private void AddConsumers()
        {
            var consumers = new List<Consumer>();

            consumers.Add(new Consumer
            {
                Id = "1",
                UserId = "1",
                UserName = "consumer1",
                FirstName = "Ivan",
                LastName = "Ivanov",
                Gender = "Male",

            });

            consumers.Add(new Consumer
            {
                Id = "2",
                UserId = "2",
                UserName = "consumer2",
                FirstName = "Georgi",
                LastName = "Georgiev",
                Gender = "Male"
            });

            consumers.Add(new Consumer
            {
                Id = "3",
                UserId = "3",
                UserName = "consumer3",
                FirstName = "Milen",
                LastName = "Milenov",
                Gender = "Male"
            });

            this.db.Consumers.AddRange(consumers);
            this.db.SaveChanges();
        }
    }
}
