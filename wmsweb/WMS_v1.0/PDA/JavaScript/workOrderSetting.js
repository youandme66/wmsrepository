var zindex = document.getElementById("zindex");

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
        document.getElementById("Part_no_update").value = td[1].innerHTML;
        document.getElementById("Wo_no_update").value = td[2].innerHTML;
        document.getElementById("Status_update").value = td[3].innerHTML;
        document.getElementById("Target_qty_update").value = td[4].innerHTML;
    }
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
        var td = tr.getElementsByTagName("td")[2];
        document.getElementById("Wo_no_Delet").value = td.innerHTML;
    }
}

$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "workorder.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Part_No_1").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Part_No_1").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});

$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "workorder.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Part_no").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Part_no").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});

$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "workorder.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Part_no_update").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Part_no_update").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});



$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "wo_no.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Wo_No_1").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Wo_No_1").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});