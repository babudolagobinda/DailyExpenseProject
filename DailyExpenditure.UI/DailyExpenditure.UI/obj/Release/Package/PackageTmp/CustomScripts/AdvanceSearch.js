//import { debug } from "util";

$(document).ready(function () {
    BindYear();
    BindMonth();
    $("#btnAdvanceSearch").click(function () {
        debugger;
        if ($("#ddlYear").val() == "Select Year") {
            document.getElementsByClassName("btn dropdown-toggle btn-default")[0].style.borderColor = "red";
        }
        else {
			document.getElementsByClassName("btn dropdown-toggle btn-default")[0].style.borderColor = "";
            if ($("#ddlMonth").val() != "Select Month" && $("#ddlMonth").val() != null) {
                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/User/GetYearAndMonthWiseExpense',
                    data: { year: parseInt($("#ddlYear").val()), month: parseInt($("#ddlMonth").val()) },
                    success: function (res) {
                        var totalExpense = 0;
                        var myArray = [];
                        for (i = 0; i < res.length; i++) {
                            totalExpense += parseFloat(res[i].Amount);
                            var date = ConverJsonDateToSystemDate(res[i].Date);
                            myArray.push({ x: new Date(date.getFullYear(), date.getMonth(), date.getDate()), y: res[i].Amount })
                        }
                        $("#spnTotalExpense").text('Rs.' + totalExpense);
                        var chart = new CanvasJS.Chart("line_chart", {
                            theme: "theme2",
                            animationEnabled: true,
                            title: {
                                text: "Month Expense",
                                fontSize: 25
                            },
                            axisX: {
                                valueFormatString: "DD",
                                interval: 0,
                                intervalType: "day",
                                title: "Expense Month"

                            },
                            //axisY: {
                            //    title: "Expense Amount"
                            //},
                            axisY: {
                                title: "Expense Amount",
                                includeZero: false,
                                //prefix: "₹"
                            },
                            data: [
                                {
                                    type: "splineArea",
                                    name: "Euro",
                                    // markerSize: 0,
                                    xValueType: "dateTime",
                                    xValueFormatString: "DD MMM",
                                    yValueFormatString: "₹#,##0.##",
                                    dataPoints: myArray,
                                }
                            ]
                        });

                        chart.render();
                    }
                })
            }
            else {
                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/User/GetYearWiseExpense',
                    data: { year: parseInt($("#ddlYear").val()) },
                    success: function (res) {
                        debugger;
                        var myArray = [];
                        var totalExpense = 0;
                        for (i = 0; i < res.length; i++) {
                            totalExpense += parseFloat(res[i].Amount);
                            var date = ConverJsonDateToSystemDate(res[i].Date);
                            myArray.push({ x: new Date(date.getFullYear(), date.getMonth()), y: res[i].Amount })
                        }
                        $("#spnTotalExpense").text('Rs.' + totalExpense);
                        var chart = new CanvasJS.Chart("line_chart", {
                            theme: "theme2",
                            animationEnabled: true,
                            title: {
                                text: "Month Expense",
                                fontSize: 25
                            },
                            axisX: {
                                valueFormatString: "MMMM",
                                interval: 0,
                                intervalType: "month",
                                title: "Expense Month"

                            },
                            //axisY: {
                            //    title: "Expense Amount"
                            //},
                            axisY: {
                                title: "Expense Amount",
                                includeZero: false,
                                //prefix: "₹"
                            },
                            data: [
                                {
                                    type: "splineArea",
                                    name: "Euro",
                                    // markerSize: 0,
                                    xValueType: "dateTime",
                                    xValueFormatString: "MMM YYYY",
                                    yValueFormatString: "₹#,##0.##",
                                    dataPoints: myArray,
                                }
                            ]
                        });

                        chart.render();
                    }
                })
            }
        }
    })
})
function ConverJsonDateToSystemDate(date) {
    var nowDate = new Date(parseInt(date.substr(6)));
    //var systemdate = convert(nowDate)
    return nowDate;
}
function convert(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    //return [date.getFullYear(), mnth, day].join("-");
    return date.getFullYear(), mnth, day;
}
function BindYear() {
    var date = new Date();
    var year = date.getFullYear();
    for (y = year; y >= 2000; y--) {
        var optn = document.createElement("OPTION");
        optn.text = y;
        optn.value = y;
        //if (y == year) {
        //    optn.selected = true;
        //}
        document.getElementById('ddlYear').options.add(optn);
    }
}
function BindMonth() {
    debugger;
    var date = new Date();
    var month = date.getMonth();
    var monthArray = new Array();
    monthArray[0] = "January";
    monthArray[1] = "February";
    monthArray[2] = "March";
    monthArray[3] = "April";
    monthArray[4] = "May";
    monthArray[5] = "June";
    monthArray[6] = "July";
    monthArray[7] = "August";
    monthArray[8] = "September";
    monthArray[9] = "October";
    monthArray[10] = "November";
    monthArray[11] = "December";
    for (m = 0; m <= 11; m++) {
        var optn = document.createElement("OPTION");
        optn.text = monthArray[m];
        optn.value = (m + 1);
        //if (m == month) {
        //    optn.selected = true;
        //}
        document.getElementById('ddlMonth').options.add(optn);
    }
}