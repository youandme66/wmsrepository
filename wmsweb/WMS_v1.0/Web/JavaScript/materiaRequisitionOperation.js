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


var btn_debit = document.getElementsByClassName("btn_debit");
var div_togger1 = document.getElementById("div_togger1");
for (var i = 0; i < btn_debit.length; i++) {
    btn_debit[i].index = i;
    btn_debit[i].onclick = function () {
        zindex.style.display = "block";
        div_togger1.style.display = "block";

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("issue_line_id_debit").value = td[0].innerHTML;
        document.getElementById("item_name_debit").value = td[2].innerHTML;
        document.getElementById("issued_qty_debit").value = td[7].innerHTML;
        document.getElementById("frame_key_debit").value = td[9].innerHTML;
        document.getElementById("issued_sub_key_debit").value = td[10].innerHTML;
        document.getElementById("REQUIRED_QTY2").value = td[6].innerHTML;
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
        document.getElementById("issue_line_id_update").value = td[0].innerHTML;
        document.getElementById("item_name_update").value = td[2].innerHTML;
        document.getElementById("issued_qty_update").value = td[7].innerHTML;
        document.getElementById("REQUIRED_QTY2").value = td[6].innerHTML;
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
        var td = tr.getElementsByTagName("td")[0];
        document.getElementById("issue_line_id_Delet").value = td.innerHTML;
    }
}

$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "getInvoiceNo.ashx",
        dataType: "json",
        data: {
            warehouse: $("#issue_no").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#issue_no").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});
