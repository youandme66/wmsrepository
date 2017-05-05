var zindex = document.getElementById("zindex");

var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
        div_togger2.style.display = "none";
        div_togger3.style.display = "none";
        div_togger4.style.display = "none";
        div_togger5.style.display = "none";
        div_togger6.style.display = "none";
        div_togger7.style.display = "none";
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

        var table = document.getElementById("Top_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("po_no_Update").value = td[1].innerHTML;
        document.getElementById("vendor_key_Update").value = td[2].innerHTML;
    }
}

var btn_delete = document.getElementsByClassName("btn_delete");
var div_togger3 = document.getElementById("div_togger3");
for (var i = 0; i < btn_delete.length; i++) {
    btn_delete[i].index = i;
    btn_delete[i].onclick = function () {
        zindex.style.display = "block";
        div_togger3.style.display = "block";

        var table = document.getElementById("Top_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("po_no_delet").value = td[1].innerHTML;
    }
}


var btn_insert_poLine = document.getElementsByClassName("btn_insert_poLine");
var div_togger4 = document.getElementById("div_togger4");
for (var i = 0; i < btn_insert_poLine.length; i++) {
    btn_insert_poLine[i].index = i;
    btn_insert_poLine[i].onclick = function () {
        zindex.style.display = "block";
        div_togger4.style.display = "block";

        var table = document.getElementById("Top_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("po_header_id_insertPoLine").value = td[0].innerHTML;
        document.getElementById("PO_NO1").value = td[1].innerHTML;
    }
}

var btn_update_poline = document.getElementsByClassName("btn_update_poline");
var div_togger5 = document.getElementById("div_togger5");
for (var i = 0; i < btn_update_poline.length; i++) {
    btn_update_poline[i].index = i;
    btn_update_poline[i].onclick = function () {
        zindex.style.display = "block";
        div_togger5.style.display = "block";

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        //document.getElementById("po_header_id_Update_poLine").value = td[0].innerHTML;
        document.getElementById("PO_NO2").value = td[1].innerHTML;
        document.getElementById("line_num_Update_poLine").value = td[2].innerHTML;
        //document.getElementById("item_id_Update_poLine").value = td[3].innerHTML;
        document.getElementById("request_qty_Update_poLine").value = td[4].innerHTML;
    }
}

var btn_delete__poline = document.getElementsByClassName("btn_delete__poline");
var div_togger6 = document.getElementById("div_togger6");
for (var i = 0; i < btn_delete__poline.length; i++) {
    btn_delete__poline[i].index = i;
    btn_delete__poline[i].onclick = function () {
        zindex.style.display = "block";
        div_togger6.style.display = "block";

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("po_no_Delet_poLine").value = td[1].innerHTML;
        document.getElementById("po_line_id_delet").value = td[6].innerHTML;
        document.getElementById("line_num_delet").value = td[2].innerHTML;
    }
}



//var btn_query_poLine = document.getElementsByClassName("btn_query_poLine");
//var div_togger7 = document.getElementById("div_togger7");
//for (var i = 0; i < btn_query_poLine.length; i++) {
//    btn_query_poLine[i].index = i;
//    btn_query_poLine[i].onclick = function () {
//        //zindex.style.display = "block";
//        //div_togger7.style.display = "block";

//        var table = document.getElementById("Line_Table");
//        var tr = table.getElementsByTagName("tr")[this.index + 1];
//        var td = tr.getElementsByTagName("td")[0];
//        document.getElementById("po_header_id_Query").value = td.innerHTML;
//    }
//}

function showDetial(obj) {



    var Top_Table = document.getElementById("Top_Table");

    var Top_Tr = Top_Table.getElementsByTagName("tr");

    console.log(obj);

    var FinalTr = obj.parentNode;

    var Top_Td = FinalTr.getElementsByTagName("td");

    for (var i = 0; i < Top_Tr.length; i++) {

        Top_Tr[i].setAttribute("class", "");
    }

    var Line_Table = document.getElementById("Line_Table");

    var Line_tr = Line_Table.getElementsByTagName("tr");

    for (var i = 1; i < Line_tr.length; i++) {

        Line_tr[i].setAttribute("class", "tr_hide");
    }

    for (var i = 1; i < Line_tr.length; i++) {

        var Line_Td = Line_tr[i].getElementsByTagName("td");

        if (Line_Td[0].innerHTML == Top_Td[0].innerHTML) {

            Line_tr[i].setAttribute("class", "tr_show");

        }
    }

}


$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "polinesetting.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Item").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Item").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});


$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "polinesetting.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Item1").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Item1").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});



$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "venderkeysetting.ashx",
        dataType: "json",
        data: {
            warehouse: $("#vendor_key_Insert").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#vendor_key_Insert").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});

$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "venderkeysetting.ashx",
        dataType: "json",
        data: {
            warehouse: $("#vendor_key_Update").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#vendor_key_Update").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});
