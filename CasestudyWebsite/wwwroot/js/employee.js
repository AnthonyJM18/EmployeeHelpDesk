$(function () {


    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhone: { maxlength: 15, required: true }
        },
        errorElement: "div",
        onfocusout: false,
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars", maxlength: "required 1-4 chars", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars", maxlength: "required 1-25 chars"
            },
            TextBoxLastname: {
                required: "required 1-25 chars", maxlength: "required 1-25 chars"
            },
            TextBoxPhone: {
                required: "required 1-15 chars", maxlength: "required 1-15 chars"
            },
            TextBoxEmail: {
                required: "required 1-40 chars", maxlength: "required 1-40 chars", email: "need valid email format"
            }
        }
    });

    $.validator.addMethod("validTitle", (value) => {
        return (value === "Mr." || value === "Ms." || value === "Mrs." || value === "Dr.")
    }, "");



    // Method to get all employees information, and then builds the html list
    const getAll = async (msg) => {
        try {
            $("#employeeList").text("Finding Employee Information...");
            let response = await fetch(`api/employee`);
            if (!response.ok) {
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            }
            let data = await response.json();
            buildEmployeeList(data, true);
            msg === "" ?
                $("#status").text("Employees loaded") : $("#status").text(`${msg} - Employees Loaded`);
        }
        catch (error) {
            $("#status").text(error.message);
        }
    };

    // Method to setup the modal for updating an employee
    const setupForUpdate = (id, data) => {
        $("#EmployeeModalForm").validate().resetForm();
        $("#actionButton").val("Update");
        $("#deleteButton").show();
        $("#modaltitle").html("<h4>Update Employee</h4>");
        // Clear the modals fields of any existing data and then add the selected employees information
        clearModalFields();
        data.map(employee => {
            if (employee.id === parseInt(id)) {
                $("#TextBoxTitle").val(employee.title);
                $("#TextBoxFirstname").val(employee.firstname);
                $("#TextBoxLastname").val(employee.lastname);
                $("#TextBoxPhone").val(employee.phoneno);
                $("#TextBoxEmail").val(employee.email);
                $("#ddlDeps").val(employee.departmentId);
                $("#ImageHolder").html(`<img height="120" width="110" src="data:image/png;base64,${employee.staffPicture64}"/>`)
                sessionStorage.setItem("Id", employee.id);
                sessionStorage.setItem("Timer", employee.timer);
                sessionStorage.setItem("Picture", employee.staffPicture64);
                $("#modalstatus").text("update data")
                $("#theModal").modal("toggle");
            }
        });
    };

    // Method to setup the modal for adding an employee
    const setupForAdd = (id, data) => {
        $("#actionButton").val("Add");
        $("#modaltitle").html("<h4>Add Employee</h4>");
        $("#modalstatus").text("Add new employee");
        $("#theModal").modal("toggle");
        $("#deleteButton").hide();
        $("#ddlDeps").val(-1);
        clearModalFields();
        $("#srch") === "" ? null : $("#TextBoxLastname").val($("#srch").val());
    };

    // Method to clear all modal fields
    const clearModalFields = () => {     
        $("#TextBoxTitle").val("");
        $("#TextBoxFirstname").val("");
        $("#TextBoxLastname").val("");
        $("#TextBoxPhone").val("");
        $("#TextBoxEmail").val("");
        $("#uploader").val("");
        $("#ImageHolder").html(`<img height="120" width="110" src=""/>`);
        sessionStorage.removeItem("Id");
        sessionStorage.removeItem("departmentid");
        sessionStorage.removeItem("Timer");
        sessionStorage.removeItem("Picture")
        $("#EmployeeModalForm").validate().resetForm();
    };

    // Method to build the employee list html
    const buildEmployeeList = (data, usealldata) => {
        $("#employeeList").empty();
        // CMain header and column headings
        div = $(`<div class="list-group-item font-weight-bold row d-flex" 
                 style="background-color:rgba(94, 144, 173, 0.65); color:rgba(255, 255, 255); text-shadow:2px 2px rgba(79, 79, 79, 0.50); font-size:24px" id="status">
                 Employee Info</div>
                 <div class="list-group-item row d-flex text-center" style="background-color:rgba(90, 90, 90, 0.25); id="heading">
                 <div class="col-4 h4">Title</div>
                 <div class="col-4 h4">First</div>
                 <div class="col-4 h4">Last</div>
            </div>`);
        div.appendTo($("#employeeList"));
        usealldata ? sessionStorage.setItem("allemployees", JSON.stringify(data)) : null;
        // Employee Information Rows
        var colorSwitch = 0;
        data.map(emp => {
            if (colorSwitch === 0) {
                btn = $(`<button class="list-group-item row d-flex" style="background-color:rgba(196, 229, 252);" id="${emp.id}">`);
                colorSwitch = 1;
            }
            else {
                btn = $(`<button class="list-group-item row d-flex" id="${emp.id}">`);
                colorSwitch = 0;
            }
            btn.html(`<div class="col-4" id="employeetitle${emp.id}">${emp.title}</div>
                      <div class="col-4" id="employeefname${emp.id}">${emp.firstname}</div>
                      <div class="col-4" id="employeelastnam${emp.id}">${emp.lastname}</div>`
            );
            btn.appendTo($("#employeeList"));
        });
        // Add Employee Button
        if (usealldata == false) {
            btn = $(`<button class="list-group-item row d-flex" 
                style="background-color:rgba(197, 250, 197);" id ="0">
                <div class="col-12 text-left">...Click to add employee ${$("#srch").val()}</div></button>`);
        }
        else {
            btn = $(`<button class="list-group-item row d-flex" 
                style="background-color:rgba(197, 250, 197);" id ="0">
                <div class="col-12 text-left ">...Click to add employee</div></button>`);
        }
        btn.appendTo($("#employeeList"));
        // Show the footer (Originally Hidden This will prevent it from appearing near the top of the page before the list constructs)
        $("#footer").show();
    };

    // Method to update an employee
    const update = (async (e) => {
        try {
            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.FirstName = $("#TextBoxFirstname").val();
            emp.LastName = $("#TextBoxLastname").val();
            emp.PhoneNo = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.Id = sessionStorage.getItem("Id");
            emp.DepartmentId = $("#ddlDeps").val();
            emp.Timer = sessionStorage.getItem("Timer");
            sessionStorage.getItem("Picture") ? emp.StaffPicture64 = sessionStorage.getItem("Picture") : null;
            // Employee API Put method called here
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

    // Method to add an employee
    const add = (async (e) => {
        try {
            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.PhoneNo = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.DepartmentId = $("#ddlDeps").val();
            emp.Id = -1;
            emp.Timer = null;
            sessionStorage.getItem("Picture") ? emp.StaffPicture64 = sessionStorage.getItem("Picture") : null;
            // Employee API Post method called here
            let response = await fetch("api/employee", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(emp)
            });

            if (response.ok) {
                let data = await response.json();
                getAll(data.msg);
            } else {
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`)
            }
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }
    });

    // Method to delete an employee
    const _delete = async () => {
        try {
            // Employee API Delete method called here
            let response = await fetch(`api/employee/${sessionStorage.getItem('Id')}`, {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            });
            if (response.ok) {
                let data = await response.json();
                getAll(data.msg);
            } else {
                $('#status').text(`Status - ${response.status}, Problem on delete server side, see server console`)
            }
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }
    }

    // Method to load all departments and add them to the department dropdown list
    const loadDivisionDDL = async () => {
        response = await fetch(`api/department/`);
        if (!response.ok) {
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        }
        let deps = await response.json();
        html = '';
        $('#ddlDeps').empty();
        deps.map(dep => html += `<option value="${dep.id}">${dep.name}</option>`);
        $('#ddlDeps').append(html);

    };

    $("input:file").change(() => {
        const reader = new FileReader();
        const file = $("#uploader")[0].files[0];
        file ? reader.readAsBinaryString(file) : null;
        reader.onload = (readerEvt) => {
            const binarystring = reader.result;
            const encodedstring = btoa(binarystring);
            sessionStorage.setItem('Picture', encodedstring);
            $("#ImageHolder").html(`<img height="120" width="110" src="data:image/png;base64,${encodedstring}"/>`)
        }
    });

    // Action listener that will either add or update the employee depending on what the user specifies
    $("#actionButton").click(() => {
        $("#EmployeeModalForm").validate().resetForm();
        $("#modalstatus").removeClass();
        if ($("#EmployeeModalForm").valid()) {
            $("#modalstatus").attr("class", "badge badge-success");
            $("#modalstatus").text("Data Entered is valid");
        }
        else {
            $("#modalstatus").attr("class", "badge badge-danger");
            $("#modalstatus").text("Fix Errors");
            e.preventDefault = true;
            return;
        }

        $("#actionButton").val() === "Update" ? update() : add();
    });

    // Used to display the conformation window on the delete button
    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });

    // Action listener for the delete button that calls the delete method
    $('#deleteButton').click(() => _delete());

    // Action listener for when the user selects a row
    $("#employeeList").click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === "employeeList" || Id === "") {
            Id = e.target.id;
        }
        if (Id !== "status" && Id !== "heading") {
            let data = JSON.parse(sessionStorage.getItem("allemployees"));
            Id === "0" ? setupForAdd() : setupForUpdate(Id, data);
        }
        else {
            return false;
        }
    });

    $("#srch").keyup(() => {
        let alldata = JSON.parse(sessionStorage.getItem("allemployees"));
        let filtereddata = alldata.filter((emp) => emp.lastname.match(new RegExp($("#srch").val(), 'i')));
        buildEmployeeList(filtereddata, false);
    });

    // On load method calls
    loadDivisionDDL();
    getAll("");

});