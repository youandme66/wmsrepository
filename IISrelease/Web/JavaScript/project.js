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

        document.getElementById("Frame_key2").value = td[0].innerHTML;
        document.getElementById("Label2").innerHTML = td[0].innerHTML;

        document.getElementById("Frame_name2").value = td[1].innerHTML;
        document.getElementById("Enabled2").value = td[2].innerHTML;
        document.getElementById("Subinventory2").value = td[3].innerHTML;
        document.getElementById("Region_key2").value = td[4].innerHTML;

        document.getElementById("Create_by2").value = td[5].innerHTML;
        document.getElementById("Label3").innerHTML = td[5].innerHTML;

        document.getElementById("Update_by2").value = td[6].innerHTML;
        document.getElementById("Description2").value = td[7].innerHTML;
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
        document.getElementById("Label1").innerHTML = td.innerHTML;
        document.getElementById("lab").value = td.innerHTML;
    }
}

$("a").bind("focus", function () {
    if (this.blur) {
        this.blur();
    }
});