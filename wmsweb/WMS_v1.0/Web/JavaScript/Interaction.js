﻿var zindex = document.getElementById("zindex");

var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
        div_togger2.style.display = "none";
        div_togger3.style.display = "none";
    }
}

var btn_insert = document.getElementById("btn_insert");
var div_togger1 = document.getElementById("div_togger1");
btn_insert.onclick = function () {
    zindex.style.display = "block";
    div_togger1.style.display = "block";
}


var btn_delete = document.getElementsByClassName("btn_delete");
var div_togger3 = document.getElementById("div_togger3");
for (var i = 0; i < btn_delete.length; i++) {
    btn_delete[i].index = i;
    btn_delete[i].onclick = function () {
        zindex.style.display = "block";
        div_togger3.style.display = "block";

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("Label1").innerHTML = td[1].innerHTML;
        document.getElementById("program_id_Delete").value = td[0].innerHTML;
    }
}


var btn_update = document.getElementsByClassName("btn_update");
var div_togger2 = document.getElementById("div_togger2");
for (var i = 0; i < btn_update.length; i++) {
    btn_update[i].index = i;
    btn_update[i].onclick = function () {
        zindex.style.display = "block";
        div_togger2.style.display = "block";



        var table = document.getElementById("Line_Table");


        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");


        document.getElementById("program_id_Update").value = td[0].innerHTML;
        document.getElementById("program_name_Update").value = td[1].innerHTML;
        document.getElementById("enabled_Update").value = td[6].innerHTML;
        document.getElementById("description_Update").value = td[7].innerHTML;
    }
}


$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "getALLProgram.ashx",
        dataType: "json",
        data: {
            warehouse: $("#program_name_Update").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#program_name_Update").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});

$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "getALLProgram.ashx",
        dataType: "json",
        data: {
            warehouse: $("#program_name_Query").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#program_name_Query").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});