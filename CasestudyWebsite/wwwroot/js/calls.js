$(function () {

    // Validation
    $("#CallModalForm").validate({
        rules: {
            ddlEmp: { required: true },
            ddlProbs: { required: true },
            ddlTech: { required: true },
            TextBoxNotes: { maxlength: 250, required: true},
        },
        errorElement: "div",
        onfocusout: false,
        messages: {
            ddlEmp: {
                required: "required"
            },
            ddlProbs: {
                required: "required"
            },
            ddlTech: {
                required: "required"
            },
            TextBoxNotes: {
                required: "required 1-250 chars", maxlength: "required 1-250 chars"
            },
        }
    });

    // All call list functionality
    const getAll = async (msg) => {
        try {
            $("#CallList").text("Finding Call Information...");
            let response = await fetch(`api/call/`);
            if (!response.ok) {
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            }
            let data = await response.json();
            buildCallList(data, true);
            msg === "" ?
                $("#status").text("Calls loaded") : $("#status").text(`${msg} - Calls Loaded`);
        }
        catch (error) {
            $("#status").text(error.message);
        }
    };

    const buildCallList = (data, usealldata) => {
        $("#CallList").empty();
        div = $(`<div class="list-group-item font-weight-bold row d-flex" 
                 style="background-color:rgba(94, 144, 173, 0.65); color:rgba(255, 255, 255); text-shadow:2px 2px rgba(79, 79, 79, 0.50); font-size:24px" id="status">
                 Call Info</div>
                 <div class="list-group-item row d-flex text-center" style="background-color:rgba(90, 90, 90, 0.25); id="heading">
                 <div class="col-4 h4">Date</div>
                 <div class="col-4 h4">For</div>
                 <div class="col-4 h4">Problem</div>
            </div>`);
        div.appendTo($("#CallList"));
        usealldata ? sessionStorage.setItem("allCalls", JSON.stringify(data)) : null;
        var colorSwitch = 0;
        data.map(call => {
            if (colorSwitch === 0) {
                btn = $(`<button class="list-group-item row d-flex" style="background-color:rgba(196, 229, 252);" id="${call.id}">`);
                colorSwitch = 1;
            }
            else {
                btn = $(`<button class="list-group-item row d-flex" id="${call.id}">`);
                colorSwitch = 0;
            }
            btn.html(`<div class="col-4" id="callOpen${call.id}">${formatDate(call.dateOpened)}</div>
                      <div class="col-4" id="employeelastname${call.id}">${call.employeeName}</div>
                      <div class="col-4" id="problem${call.id}">${call.problemDescription}</div>`
            );
            btn.appendTo($("#CallList"));
        });
        btn = $(`<button class="list-group-item row d-flex" 
            style="background-color:rgba(197, 250, 197);" id ="-1">
            <div class="col-12 text-left">...Click to add new call</div></button>`);
        btn.appendTo($("#CallList"));
        $("#footer").show();
    };

    // All add functionality
    const setupForAdd = (id, data) => {
        clearModalFields();
        $("#actionButton").val("Add");
        $("#modaltitle").html("<h4>Add Call</h4>");
        $("#modalstatus").text("Add new call");
        $("#theModal").modal("toggle");
        $("#deleteButton").hide();
        $("#closeRow").hide();
        $("#dateClosedRow").hide();
        $("#dateOpened").val(formatDate());
    };

    const add = (async (e) => {
        try {
            call = new Object();
            call.employeeId = $("#ddlEmp").val();
            call.techId = $("#ddlTech").val();
            call.problemId = $("#ddlProbs").val();
            call.dateOpened = $("#dateOpened").val();
            call.dateClosed = null;
            call.openStatus = true;
            call.notes = $("#TextBoxNotes").val();
            call.Timer = null;
            call.id = -1

            // Employee API Post method called here
            let response = await fetch("api/call/", {
                method: "POST",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(call)
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

    // Update functionality
    const setupForUpdate = (id, data) => {
        clearModalFields();
        $("#actionButton").val("Update");
        $("#modaltitle").html("<h4>Update Call</h4>");
        // Clear the modals fields of any existing data and then add the selected employees information
        data.map(call => {
            if (call.id === parseInt(id)) {
                $("#ddlProbs").val(call.problemId);
                $("#ddlEmp").val(call.employeeId);
                $("#ddlTech").val(call.techId);
                $("#dateOpened").val(formatDate(call.dateOpened));
                if (call.dateClosed !== null) {
                    $("#dateClosed").val(formatDate(call.dateClosed));
                }
                if (call.openStatus == false) {
                    $("#closeCB").prop('checked', true);
                    $("#closeCB").prop('disabled', true);
                    $("#ddlProbs").prop('disabled', true);
                    $("#ddlEmp").prop('disabled', true);
                    $("#ddlTech").prop('disabled', true);
                    $("#TextBoxNotes").prop('disabled', true);
                    $("#actionButton").hide();
                    $("#deleteButton").show();
                }
                $("#TextBoxNotes").val(call.notes)
                sessionStorage.setItem("Id", call.id);
                sessionStorage.setItem("Timer", call.timer);
                $("#modalstatus").text("update data")
                $("#theModal").modal("toggle");
            }
        });
    };

    const update = (async (e) => {
        try {
            call = new Object();
            call.problemId = $("#ddlProbs").val();
            call.employeeId = $("#ddlEmp").val();
            call.techId = $("#ddlTech").val();
            call.dateOpened = $("#dateOpened").val();
            call.dateClosed = $("#dateClosed").val();
            call.notes = $("#TextBoxNotes").val()
            if ($("#closeCB").is(":checked")) {
                call.openStatus = false;
            }
            else {
                call.openStatus = true;
            }
            call.id = sessionStorage.getItem("Id");
            call.timer = sessionStorage.getItem("Timer");
            // Employee API Put method called here
            let response = await fetch("/api/call", {
                method: "Put",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(call)
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

    // Clear functionality 
    const clearModalFields = () => {
        $("#TextBoxNotes").val("");
        $("#ddlProbs").val(-1);
        $("#ddlEmp").val(-1);
        $("#ddlTech").val(-1);
        $("#dateClosed").val("");
        $("#closeCB").prop('checked', false);
        $("#closeCB").prop('disabled', false);
        $("#ddlProbs").prop('disabled', false);
        $("#ddlEmp").prop('disabled', false);
        $("#ddlTech").prop('disabled', false);
        $("#TextBoxNotes").prop('disabled', false);
        $("#actionButton").show();
        $("#dateClosedRow").show();
        $("#deleteButton").hide();
        $("#closeRow").show();
        sessionStorage.removeItem("Id");
        sessionStorage.removeItem("Timer");
        $("#CallModalForm").validate().resetForm();
    };

    // Load DDL's
    const loadProblemDDL = async () => {
        response = await fetch(`api/problem/`);
        if (!response.ok) {
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        }
        let probs = await response.json();
        html = '';
        $('#ddlProbs').empty();
        probs.map(prob => html += `<option value="${prob.id}">${prob.description}</option>`);
        $('#ddlProbs').append(html);
    };

    const loadEmployeeDDL = async () => {
        response = await fetch(`api/employee/`);
        if (!response.ok) {
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        }
        let emps = await response.json();
        html = '';
        $('#ddlEmp').empty();
        emps.map(emp => html += `<option value="${emp.id}">${emp.lastname}</option>`);
        $('#ddlEmp').append(html);
        html = '';
        emps.map(emp => {
            if (emp.isTech === true) {
                html += `<option value="${emp.id}">${emp.lastname}</option>`;
            }
        });
        $("#ddlTech").append(html);
    };

    // Delete Functionality 
    const _delete = async () => {
        try {
            // Employee API Delete method called here
            let response = await fetch(`api/call/${sessionStorage.getItem('Id')}`, {
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

    // Utilities
    const formatDate = (date) => {
        let d;
        date === (undefined) ? d = new Date() : d = new Date(Date.parse(date));
        let _day = d.getDate();
        let _month = d.getMonth() + 1;
        let _year = d.getFullYear();
        let _hour = d.getHours();
        let _min = d.getMinutes();
        if (_min < 10) { _min = "0" + _min; }
        if (_year > 2030) return "";
        return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
    };

    // Action listeners
    $("#actionButton").click(() => {
        $("#CallModalForm").validate().resetForm();
        $("#modalstatus").removeClass();
        if ($("#CallModalForm").valid()) {
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

    $("#CallList").click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === "CallList" || Id === "") {
            Id = e.target.id;
        }
        if (Id !== "status" && Id !== "heading") {
            let data = JSON.parse(sessionStorage.getItem("allCalls"));
            Id === "-1" ? setupForAdd() : setupForUpdate(Id, data);
        }
        else {
            return false;
        }
    });

    $("#closeCB").click(() => {
        if ($("#closeCB").is(":checked")) {
            $("#dateClosed").val(formatDate());
        }
        else {
            $("#dateClosed").val("");
        }
    });

    $('#deleteButton').click(() => _delete());

    $("#srch").keyup(() => {
        let alldata = JSON.parse(sessionStorage.getItem("allCalls"));
        let filtereddata = alldata.filter((call) => call.employeeName.match(new RegExp($("#srch").val(), 'i')));
        buildCallList(filtereddata, false);
    });

    // On load method call
    getAll("");
    loadProblemDDL();
    loadEmployeeDDL();
});