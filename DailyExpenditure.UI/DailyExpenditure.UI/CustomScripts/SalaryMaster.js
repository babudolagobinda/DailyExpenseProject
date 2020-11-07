$(document).ready(function () {
    $("#pnlContent").hide();
    var name = window.prompt("please Enter Your EmailId to verify your Account.", "");
    debugger;
    if (name != "") {
        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: '/User/CheckUserEmailId',
            data: { EmailId: name},
            success: function (res) {
                debugger;
                if (res > 0) {
                    //window.location.href = '/User/SalaryMaster';
                    $("#pnlContent").show();
                }
                else {
                    alert('Invalid Email..!');
                    window.location.href = '/User/DashBoard';
                }
            } 
        })
    }
    if (name != null) {

    }
    else {
        window.location.href = '/User/DashBoard';
    }
})
