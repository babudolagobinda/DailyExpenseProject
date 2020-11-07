var table;
$(document).ready(function () {
    table = $('#tblExpenseMaster').DataTable();

    $("#divExpenseType").hide();
    $("#divPaymentType").hide();
    $("#btnExpenseType").click(function () {
        $("#divExpenseType").show();
    })
    $("#btnPaymentType").click(function () {
        $("#divPaymentType").show();
    })
    $("#btnExpenseTypeClose").click(function () {
        $("#divExpenseType").hide();
    })
    $("#btnPaymentTypeClose").click(function () {
        $("#divPaymentType").hide();
    })
    $("#btnModalAddExpense").click(function () {
        reset();
    })
    $("#btnAddExpense").click(function () {
        debugger;
        var expenseMasterDto = {
            ExpenseId: parseInt($("#hdnExpenseId").val()),
            NameOfExpense: $("#txtNameOfExpense").val(),
            Amount: $("#txtAmount").val(),
            Date: $("#txtExpenseDate").val(),
            ExpenseTypeId: $("#ddlExpenseType").val(),
            PaymentTypeId: $("#ddlPaymentType").val(),
            Note: $("#txtNote").val(),
        }
        if (ValidateAddExpense()) {
            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/User/SaveExpenseMaster',
                data: { expenseMasterDto: expenseMasterDto },
                success: function (res) {
                    if (res > 0) {
                        window.location.reload();
                    }
                    else {
                        alert('Error Occoured');
                    }
                }
            })
        }
    })
    BindExpenseType();
    BindPaymentType();
    $("#btnExpenseTypeAdd").click(function () {
        if ($("#txtExpenseType").val() != "") {
            document.getElementById("txtExpenseType").style.borderColor = "";
            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/User/AddExpenseType',
                data: { ExpenseType: $("#txtExpenseType").val() },
                success: function (res) {
                    if (res > 0) {
                        BindExpenseType();
                        $("#divExpenseType").hide();
                        $("#txtExpenseType").val("");
                    }
                    else {

                    }
                }
            })
        }
        else {
            document.getElementById("txtExpenseType").style.borderColor = "red";
        }
    })
    $("#btnPaymentTypeAdd").click(function () {
        if ($("#txtPaymentType").val() != "") {
            document.getElementById("txtPaymentType").style.borderColor = "";
            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/User/AddPaymentType',
                data: { PaymentType: $("#txtPaymentType").val() },
                success: function (res) {
                    if (res > 0) {
                        BindPaymentType();
                        $("#divPaymentType").hide();
                        $("#txtPaymentType").val("");
                    }
                    else {

                    }
                }
            })
        }
        else {
            document.getElementById("txtPaymentType").style.borderColor = "red";
        }
    })
})

$('body').on('click', '#btnEditExpense', function () {
    debugger;
    var row = $(this).parents('tr')[0];
    console.log(table.row(row).data());

});
function BindExpenseType() {
    $.ajax({
        type: 'GET',
        dataType: 'JSON',
        url: '/User/GetAllExpenseType',
        data: {},
        success: function (res) {
            debugger;
            var appendData = "";
            $("#ddlExpenseType").empty();
            $("#ddlExpenseType").append($("<option>").attr("value", "0").text("Select ExpenseType"));
            if (res.length > 0) {
                $.each(res, function (index, value) {
                    $('#ddlExpenseType').append('<option value="' + value.ExpenseTypeId + '">' + value.ExpenseType + '</option>');
                });
                $('#ddlExpenseType').selectpicker('refresh');
            }
        }
    })
}
function BindPaymentType() {
    $.ajax({
        type: 'GET',
        dataType: 'JSON',
        url: '/User/GetAllPaymentType',
        data: {},
        success: function (res) {
            debugger;
            var appendData = "";
            $("#ddlPaymentType").empty();
            $("#ddlPaymentType").append($("<option>").attr("value", "0").text("Select PaymentType"));
            if (res.length > 0) {
                $.each(res, function (index, value) {
                    $('#ddlPaymentType').append('<option value="' + value.PaymentTypeId + '">' + value.PaymentType + '</option>');
                });
                $('#ddlPaymentType').selectpicker('refresh');
            }
        }
    })
}

function ValidateAddExpense() {
    debugger;
    var addExpense = false;
    if ($("#txtNameOfExpense").val() == "") {
        document.getElementById("txtNameOfExpense").style.borderColor = "red";
        document.getElementById("lblNameOfExpense").style.color = "red";
        addExpense = false;
    }
    else {
        document.getElementById("txtNameOfExpense").style.borderColor = "";
        document.getElementById("lblNameOfExpense").style.color = "";
        addExpense = true;
    }
    if ($("#txtAmount").val() == "") {
        document.getElementById("txtAmount").style.borderColor = "red";
        document.getElementById("lblAmount").style.color = "red";
        addExpense = false;
    }
    else {
        document.getElementById("txtAmount").style.borderColor = "";
        document.getElementById("lblAmount").style.color = "";
        addExpense = true;
    }
    if ($("#txtExpenseDate").val() == "") {
        document.getElementById("txtExpenseDate").style.borderColor = "red";
        document.getElementById("lblDate").style.color = "red";
        addExpense = false;
    }
    else {
        document.getElementById("txtExpenseDate").style.borderColor = "";
        document.getElementById("lblDate").style.color = "";
        addExpense = true;
    }
    if ($("#ddlExpenseType").val() == "0" || $("#ddlExpenseType").val() == null) {
        document.getElementsByClassName("btn dropdown-toggle btn-default")[0].style.borderColor = "red";
        document.getElementById("lblExpenseType").style.color = "red";
        addExpense = false;
    }
    else {
        document.getElementsByClassName("btn dropdown-toggle btn-default")[0].style.borderColor = "";
        document.getElementById("lblExpenseType").style.color = "";
        addExpense = true;
    }
    if ($("#ddlPaymentType").val() == "0" || $("#ddlPaymentType").val() == null) {
        document.getElementsByClassName("btn dropdown-toggle btn-default")[1].style.borderColor = "red";
        document.getElementById("lblPaymentType").style.color = "red";
        addExpense = false;
    }
    else {
        document.getElementsByClassName("btn dropdown-toggle btn-default")[1].style.borderColor = "";
        document.getElementById("lblPaymentType").style.color = "";
        addExpense = true;
    }
    if ($("#txtNote").val() == "") {
        document.getElementById("txtNote").style.borderColor = "red";
        document.getElementById("lblNote").style.color = "red";
        addExpense = false;
    }
    else {
        document.getElementById("txtNote").style.borderColor = "";
        document.getElementById("lblNote").style.color = "";
        addExpense = true;
    }
    return addExpense;
}
$(function () {
    var customers = new Array();
    //Add the data rows.
    for (var i = 0; i < customers.length; i++) {
        AddRow(customers[i][0], customers[i][1], customers[i][2], customers[i][3], customers[i][4], customers[i][5]);
    }
});
function Add() {
    AddRow($("#txtNameOfExpense").val(), $("#txtAmount").val(), $("#txtDate").val(), $("#txtExpenseType").val(), $("#txtPaymentType").val(), $("#txtNote").val());
    $("#txtNameOfExpense").val("");
    $("#txtAmount").val("");
    $("#txtDate").val("");
    $("#txtExpenseType").val("");
    $("#txtPaymentType").val("");
    $("#txtNote").val("");
};

function AddRow(nameOfExpense, amount, date, expenseType, paymentType, note) {
    var tBody = $("#tblUserExpense > TBODY")[0];

    //Add Row.
    row = tBody.insertRow(-1);

    //Add Column cell.
    var cell = $(row.insertCell(-1));
    cell.html(nameOfExpense);

    cell = $(row.insertCell(-1));
    cell.html(amount);

    cell = $(row.insertCell(-1));
    cell.html(date);

    cell = $(row.insertCell(-1));
    cell.html(expenseType);

    cell = $(row.insertCell(-1));
    cell.html(paymentType);

    cell = $(row.insertCell(-1));
    cell.html(note);

    //Add Button cell.
    cell = $(row.insertCell(-1));

    var btnRemove = $("<input />");
    btnRemove.attr("type", "button");
    btnRemove.attr("onclick", "Remove(this);");
    btnRemove.val("Remove");
    cell.append(btnRemove);
};

function Remove(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");
    var name = $("TD", row).eq(0).html();
    if (confirm("Do you want to delete: " + name)) {
        //Get the reference of the Table.
        var table = $("#tblUserExpense")[0];
        //Delete the Table row using it's Index.
        table.deleteRow(row[0].rowIndex);
    }
};
function editExpense(expenseId) {
    debugger;
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/User/EditExpense',
        data: { expenseId: parseInt(expenseId) },
        success: function (res) {
            debugger;
            if (res.ExpenseId > 0) {
                $("#hdnExpenseId").val(res.ExpenseId);
                $("#txtNameOfExpense").val(res.NameOfExpense);
                $("#txtAmount").val(res.Amount);
                $("#txtExpenseDate").val(ConvertJsonDateToSystemDate(res.Date));
                var ExpenseType = $("select[name=ddlExpenseType] option[value='" + res.ExpenseTypeId + "']").text();
                $('.ExpenseType .filter-option').text(ExpenseType);
                $('select[name=ddlExpenseType]').val(res.ExpenseTypeId);
                var PaymentType = $("select[name=ddlPaymentType] option[value='" + res.PaymentTypeId + "']").text();
                $('.PaymentType .filter-option').text(PaymentType);
                $('select[name=ddlPaymentType]').val(res.PaymentTypeId);
                $("#txtNote").val(res.Note);
                $("#expenseModal").modal('show');
            }
        }
    })
}
function ConvertJsonDateToSystemDate(date) {
    var nowDate = new Date(parseInt(date.substr(6)));
    var systemdate = convert(nowDate);
    return systemdate;
}
function convert(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    //return [date.getFullYear(), mnth, day].join("/");
    return [mnth, day, date.getFullYear()].join("/")
}
function reset() {
    debugger;
    $("#hdnExpenseId").val("");
    $("#txtNameOfExpense").val("");
    $("#txtAmount").val("");
    $("#txtExpenseDate").val("");

    var ExpenseType = $("select[name=ddlExpenseType] option[value='" + 0 + "']").text();
    $('.ExpenseType .filter-option').text(ExpenseType);
    $('select[name=ddlExpenseType]').val("");

    var PaymentType = $("select[name=ddlPaymentType] option[value='" + 0 + "']").text();
    $('.PaymentType .filter-option').text(PaymentType);
    $('select[name=ddlPaymentType]').val("");

    $("#txtNote").val("");
}
function deleteExpense(expenseId) {
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel plz!",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/User/DeleteExpense',
                data: { expenseId: expenseId },
                success: function (res) {
                    if (res > 0) {
                        swal("Deleted!", "Your imaginary file has been deleted.", "success");
                    }
                    else {
                        swal("Not Deleted!", "Your imaginary file has not been deleted.", "error");
                    }
                }
            })

        } else {
            swal("Cancelled", "Your imaginary file is safe :)", "error");
        }
    });
};

