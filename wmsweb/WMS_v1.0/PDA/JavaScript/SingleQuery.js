var zindex = document.getElementById("zindex");

var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger3.style.display = "none";
        div_togger5.style.display = "none";
        div_togger4.style.display = "none";
    }
}

var update2 = document.getElementsByClassName("btn_update2");
var div_togger3 = document.getElementById("div_togger1");
for (var i = 0; i < update2.length; i++) {
    update2[i].index = i;
    update2[i].onclick = function () {
        zindex.style.display = "block";
        div_togger3.style.display = "block";
        var table = document.getElementById("Line_Table2");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("return_wo_no2").value = td[2].innerHTML;
        document.getElementById("seq_operation_num2").value = td[3].innerHTML;
        document.getElementById("item_name2").value = td[4].innerHTML;
        document.getElementById("quit_num2").value = td[5].innerHTML;
        document.getElementById("return_line_id2").value = td[11].innerHTML;
    }
}


var delete2 = document.getElementsByClassName("btn_delete2");
var div_togger5 = document.getElementById("div_togger2");
for (var i = 0; i < delete2.length; i++) {
    delete2[i].index = i;
    delete2[i].onclick = function () {
        zindex.style.display = "block";
        div_togger5.style.display = "block";
        var table = document.getElementById("Line_Table2");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("lab2").value = td[2].innerHTML;
        document.getElementById("lab3").value = td[11].innerHTML;
    }
}


var operation = document.getElementsByClassName("btn_operation2");
var div_togger4 = document.getElementById("div_togger3");
for (var i = 0; i < operation.length; i++) {
    operation[i].index = i;
    operation[i].onclick = function () {
        zindex.style.display = "block";
        div_togger4.style.display = "block";
        var table = document.getElementById("Line_Table2");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("return_line_id").value = td[11].innerHTML;
        document.getElementById("singlequery_subinventory").value = td[8].innerHTML;
        document.getElementById("singlequery_region").value = td[7].innerHTML;
        document.getElementById("singlequery_item_name").value = td[4].innerHTML;
        document.getElementById("singlequery_return_qty").value = td[5].innerHTML;
    }
}

