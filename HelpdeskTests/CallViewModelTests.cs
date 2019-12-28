
using System.Collections.Generic;
using Xunit;
using HelpdeskViewModels;
using System;
using Xunit.Abstractions;

namespace HelpdeskTests
{
    public class CallViewModelTests
    {
        private readonly ITestOutputHelper output;
        public CallViewModelTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Call_ComprehensiveVMTest()
        {
            CallViewModel cvm =  new CallViewModel();
            EmployeeViewModels evm = new EmployeeViewModels();
            ProblemViewModel pvm = new ProblemViewModel();
            cvm.DateOpened = DateTime.Now;
            cvm.DateClosed = null;
            cvm.OpenStatus = true;
            evm.Email = "am@abc.com";
            evm.GetByEmail();
            cvm.EmployeeId = evm.Id;
            evm.Email = "sc@abc.com";
            evm.GetByEmail();
            cvm.TechId = evm.Id;
            pvm.Description = "Memory Upgrade";
            pvm.GetByDescription();
            cvm.ProblemId = pvm.Id;
            cvm.Notes = "Anthony has bad RAM, Burner to fix it";
            cvm.Add();
            output.WriteLine("new Call; Generated - Id = " + cvm.Id);
            int id = cvm.Id;
            cvm.GetById();
            cvm.Notes += "\n Ordered new RAM!  Sean Do you even read these screenshots?";
            if (cvm.Update() == 1)
            {
                output.WriteLine("Call was updated" + cvm.Notes);
            }
            else
            {
                output.WriteLine("Call was not updated!");
            }
            cvm.Notes = "Another change that shouldnt work.";
            if (cvm.Update() == -2)
            {
                output.WriteLine("Call was not updated data was stale");
            }
            cvm = new CallViewModel();
            cvm.Id = id;
            cvm.GetById();
            if(cvm.Delete() == 1)
            {
                output.WriteLine("Call was deleted!");
            }
            else
            {
                output.WriteLine("Call was not deleted");
            }

            Exception ex = Assert.Throws<NullReferenceException>((cvm.GetById));
            Assert.Equal("Object reference not set to an instance of an object.", ex.Message);
        }

    }
}
