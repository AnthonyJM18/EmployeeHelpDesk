/* Program Name : Model Test
 * Author       : Anthony Merante
 * Date         : October 23, 2019
 * Purpose      : Model Test Class
 */


using System;
using Xunit;
using HelpdeskDAL;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace HelpdeskTests
{
    public class ModelTests
    {
        private readonly ITestOutputHelper output;
        public ModelTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Employee_GetByEmailTest()
        {
            EmployeeModel model = new EmployeeModel();
            Assert.NotNull(model.GetByEmail("bs@abc.com"));
        }

        [Fact]
        public void Employee_GetByIdTest()
        {
            EmployeeModel model = new EmployeeModel();
            Assert.NotNull(model.GetById(1));
        }

        [Fact]
        public void Employee_GetAllTest()
        {
            EmployeeModel model = new EmployeeModel();
            List<Employees> allEmployees = model.GetAll();
            Assert.NotNull(allEmployees);

        }

        [Fact]
        public void Employee_AddTest()
        {
            EmployeeModel model = new EmployeeModel();
            Employees newEmployee = new Employees();
            newEmployee.Title = "Mr";
            newEmployee.FirstName = "Anthony";
            newEmployee.LastName = "Merante";
            newEmployee.PhoneNo = "(905) 433-3333";
            newEmployee.Email = "hc@work.com";
            newEmployee.DepartmentId = 200;
            newEmployee.IsTech = true;
            model.Add(newEmployee);
            Assert.True(newEmployee.Id > 1);
        }

        [Fact]
        public void Employee_ConcurrencyTest()
        {
            EmployeeModel Employee1 = new EmployeeModel();
            Employees Employee1data = Employee1.GetByEmail("a_merante@fanshaweonline.ca");

            EmployeeModel Employee2 = new EmployeeModel();
            Employees Employee2data = Employee2.GetByEmail("a_merante@fanshaweonline.ca");

            if (Employee1data != null)
            {
                Employee1data.PhoneNo = Employee1data.PhoneNo == "(234)888-9898" ? "(333)222-1233" : "(234)888-9898";
                if (Employee1.Update(Employee1data) == UpdateStatus.Ok)
                {
                    Employee2data.PhoneNo = "123-456-9999";
                    Assert.True(Employee2.Update(Employee2data) == UpdateStatus.Stale);
                }
                else
                {
                    Assert.True(false);
                }
            }
        }

        [Fact]
        public void Employee_DeleteTest()
        {
            EmployeeModel model = new EmployeeModel();
            Employees deleteEmployee = model.GetByEmail("hc@work.com");
            Assert.True(model.Delete(deleteEmployee.Id) == 1);
        }

        [Fact]
        public void Employee_PictureTest()
        {
            DALUtil util = new DALUtil();
            Assert.True(util.AddEmployeePicsToDb());
        }

        [Fact]
        public void Call_ComprehensiveTest()
        {
            CallModel cmodel = new CallModel();
            EmployeeModel emodel = new EmployeeModel();
            ProblemsModel pmodel = new ProblemsModel();
            Calls call = new Calls();
            call.DateOpened = DateTime.Now;
            call.DateClosed = null;
            call.OpenStatus = true;
            call.EmployeeId = emodel.GetByLastName("Merante").Id;
            call.TechId = emodel.GetByLastName("Burner").Id;
            call.ProblemId = pmodel.GetByDescription("Hard Drive Failure").Id;
            call.Notes = "Evan's drive is shot, Burner to fix it";
            int newCallId = cmodel.add(call);
            output.WriteLine("New call generated Id = " + newCallId);
            call = cmodel.GetById(newCallId);
            byte[] oldtimer = call.Timer;
            output.WriteLine("New Call Retrieved");
            call.Notes += "\n Ordered new drive!";

            if (cmodel.Update(call) == UpdateStatus.Ok)
            {
                output.WriteLine("Call was updated" + call.Notes);
            }
            else
            {
                output.WriteLine("Call was not updated!");
            }
            call.Timer = oldtimer;
            call.Notes = "doesnt matter data is stale now";
            if (cmodel.Update(call) == UpdateStatus.Stale)
            {
                output.WriteLine("Call was stale and not updated");
            }
            cmodel = new CallModel();
            call = cmodel.GetById(newCallId);
            if(cmodel.Delete(newCallId) == 1)
            {
                output.WriteLine("Call was deleted");
            }
            else
            {
                output.WriteLine("Call was not deleted");
            }

            Assert.Null(cmodel.GetById(newCallId));

        }
    }
}
