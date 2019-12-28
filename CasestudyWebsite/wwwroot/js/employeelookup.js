$(function () {

    // Method which gets an employee based on email
    $("#getButton").click(async (e) => {
        try {
            let email = $("#TextBoxEmail").val();
            $("#status").text("please wait....")
            let response = await fetch(`/api/employee/${email}`);
            if (!response.ok) {
                throw new Error(`Status - ${response.status}, Problem server side, see console`);
            }
            let data = await response.json();
            if (data.email !== "not found") {
                $("#last").text(data.lastname);
                $("#title").text(data.title);
                $("#firstname").text(data.firstname);
                $("#phone").text(data.phoneno);
                $("#status").text("Employee found");
            } else {
                $("#firstname").text("not found");
                $("#email").text("");
                $("#title").text("");
                $("#phone").text("");
                $("#status").text("no such employee");
            }
        } catch (error) {
            $("#status").text(error.message);
        }

    })
});