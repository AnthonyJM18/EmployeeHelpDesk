$(function () {
    // Method to get all the employees
    const getAll = async (msg) => {
        try {
            $("#employeeList").text("Finding Employee Information...");
            let response = await fetch(`api/employee`);
            if (!response.ok) {
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            }
            let data = await response.json();
            buildEmployeesList(data);
            msg === "" ?
                $("#status").text("Employees loaded") : $("#status").text(`${msg} - Employees Loaded`);
        }
        catch (error) {
            $("#status").text(error.message);
        }
    };
    // Method to clear the modal
    const clearModalFields = () => {
        $("#TextBoxTitle").val("");
        $("#TextBoxFirstname").val("");
        $("#TextBoxLastname").val("");
        $("#TextBoxPhone").val("");
        $("#TextBoxEmail").val("");
        sessionStorage.removeItem("Id");
        sessionStorage.removeItem("DepartmentId");
        sessionStorage.removeItem("Timer");
    };
    // Action listener for when an employee row is clicked
    $("#employeeList").click((e) => {
        clearModalFields();
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === "employeeList" || Id === "") {
            Id = e.target.id;
        }
        if (Id !== "status" && Id !== "heading") {
            let data = JSON.parse(sessionStorage.getItem("allemployees"));
            data.map(employee => {
                if (employee.id === parseInt(Id)) {
                    $("#TextBoxTitle").val(employee.title);
                    $("#TextBoxFirstname").val(employee.firstname);
                    $("#TextBoxLastname").val(employee.lastname);
                    $("#TextBoxPhone").val(employee.phoneno);
                    $("#TextBoxEmail").val(employee.email);
                    sessionStorage.setItem("Id", employee.id);
                    sessionStorage.setItem("departmentid", employee.departmentId);
                    sessionStorage.setItem("Timer", employee.timer);
                    $("#modalstatus").text("update data")
                    $("#theModal").modal("toggle");
                }
            });
        } else {
            return false;
        }
    });

    // Action listener for the update button, when clicked it updates the employee with the information in the modal
    $("#updateButton").click(async (e) => {
        try {
            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.FirstName = $("#TextBoxFirstname").val();
            emp.LastName = $("#TextBoxLastname").val();
            emp.PhoneNo = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.Id = sessionStorage.getItem("Id");
            emp.DepartmentId = sessionStorage.getItem("departmentid");
            emp.Timer = sessionStorage.getItem("Timer");
            emp.Picture64 = null;

            let response = await fetch("/api/employee", {
                method: "Put",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(emp)
            });

            if (response.ok) {
                let data = await response.json();
                getAll(data.msg);
            } else {
                throw new Error(`Status - ${response.status}, Problem server side, see server console`)
            }
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }
    });

    // Method which builds the list of employees
    const buildEmployeesList = (data) => {
        $("#employeeList").empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Employees Info</div>
                 <div class="list-group-item row d-flex text-center" id="heading">
                 <div class="col-4 h4">Title</div>
                 <div class="col-4 h4">First</div>
                 <div class="col-4 h4">Last</div>
            </div>`);
        div.appendTo($("#employeeList"));
        sessionStorage.setItem("allemployees", JSON.stringify(data));
        data.map(emp => {
            btn = $(`<button class="list-group-item row d-flex" id="${emp.id}">`);
            btn.html(`<div class="col-4" id="employeetitle${emp.id}">${emp.title}</div>
                      <div class="col-4" id="employeefname${emp.id}">${emp.firstname}</div>
                      <div class="col-4" id="employeelastnam${emp.id}">${emp.lastname}</div>`
            );
            btn.appendTo($("#employeeList"));
        });
    };

    // On load method call
    getAll("");
});