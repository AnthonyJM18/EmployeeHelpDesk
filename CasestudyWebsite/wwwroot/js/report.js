$(function () {
    $("#Employeebutton").click(async (e) => {
        try {
            $("#status").text("generating report on server - please wait...");
            let response = await fetch(`api/employeereport`);
            if (!response.ok)
                throw new Error(`Status - ${response.status}, Text = ${response.statusText}`);
            let data = await response.json();
            $("#status").text("Report Generated");
            data.msg == "Report Generated"
                ? window.open("/pdfs/employeelist.pdf")
                : $("#status").text("problem generating report");
        }//try
        catch (error) {
            $("#status").text(error.message);
        }
    });//button click
    $("#Callbutton").click(async (e) => {
        try {
            $("#status").text("generating report on server - please wait...");
            let response = await fetch(`api/callreport`);
            if (!response.ok)
                throw new Error(`Status - ${response.status}, Text = ${response.statusText}`);
            let data = await response.json();
            $("#status").text("Report Generated");
            data.msg == "Report Generated"
                ? window.open("/pdfs/callslist.pdf")
                : $("#status").text("problem generating report");
        }//try
        catch (error) {
            $("#status").text(error.message);
        }
    });//button click
});//JQuery