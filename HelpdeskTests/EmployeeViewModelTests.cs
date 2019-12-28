/* Program Name : EmployeeViewModelTests
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Test Model Functionality
 */

using System.Collections.Generic;
using Xunit;
using HelpdeskViewModels;


namespace HelpdeskTests
{
    public class EmployeeViewModelTests
    {
        [Fact]
        public void Employees_GetByEmailTest()
        {
            EmployeeViewModels vm = new EmployeeViewModels();
            vm.Email = "pp@abc.com";
            vm.GetByEmail();
            Assert.NotNull(vm.Firstname);
            
        }

        [Fact]
        public void Employees_GetByIdTest()
        {
            EmployeeViewModels vm = new EmployeeViewModels();
            vm.Id = 1;
            vm.GetById();
            Assert.NotNull(vm.Firstname);
        }

        [Fact]
        public void Employees_GetAllTest()
        {
            EmployeeViewModels vm = new EmployeeViewModels();
            List<EmployeeViewModels> allEmployees = vm.GetAll();
            Assert.NotNull(allEmployees);
        }

        [Fact]
        public void Employees_AddTest()
        {
            EmployeeViewModels vm = new EmployeeViewModels();
            vm.Title = "Mr.";
            vm.Firstname = "Anthony";
            vm.Lastname = "Merante";
            vm.Phoneno = "(905) 333-4333";
            vm.Email = "a_merante@fanshaweonline.ca";
            vm.DepartmentId = 100;
            vm.Add();
            Assert.True(vm.Id > 1);
        }

        [Fact]
        public void Employees_UpdateTest()
        {
            EmployeeViewModels vm = new EmployeeViewModels();
            vm.Email = "a_merante@fanshaweonline.ca";
            vm.GetByEmail();
            vm.DepartmentId = vm.DepartmentId == 100 ? 200 : 100;
            Assert.True(vm.Update() == 1);
        }

        [Fact]
        public void Employees_DeleteTest()
        {
            EmployeeViewModels vm = new EmployeeViewModels();
            vm.Email = "a_merante@fanshaweonline.ca";
            vm.GetByEmail();
            Assert.True(vm.Delete() == 1);
        }







    }
}
