var zindex = document.getElementById("zindex");
var cancel = document.getElementsByClassName("btn_close1");
for (var i = 0; i < cancel.length; i++) {
    cancel[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
        div_togger2.style.display = "none";
        div_togger6.style.display = "none";
    }
}
var update1 = document.getElementsByClassName("btn_update1");
var div_togger2 = document.getElementById("div_togger2");
for (var i = 0; i < update1.length; i++) {
    update1[i].index = i;
    update1[i].onclick = function () {
        zindex.style.display = "block";
        div_togger2.style.display = "block";
        var table = document.getElementById("Line_Table1");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("quit_invoice_no2").value=td[1].innerHTML;
        document.getElementById("quit_type2").value=td[2].innerHTML;
        document.getElementById("Subinventory4").value=td[3].innerHTML;
        document.getElementById("status2").value=td[4].innerHTML;
        document.getElementById("quit_wo_no2").value=td[7].innerHTML;
        document.getElementById("remark2").value=td[9].innerHTML;
    }
}

var delete1 = document.getElementsByClassName("btn_delete1");
var div_togger6 = document.getElementById("div_togger6");
for (var i = 0; i < delete1.length; i++) {
    delete1[i].index = i;
    delete1[i].onclick = function () {
        zindex.style.display = "block";
        div_togger6.style.display = "block";
        var table = document.getElementById("Line_Table1");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("lab1").value = td[1].innerHTML;
    }
}

var insert = document.getElementsByClassName("btn_insert1");
var div_togger1 = document.getElementById("div_togger1");
for (var i = 0; i < insert.length; i++) {
    insert[i].index = i;
    insert[i].onclick = function () {
        zindex.style.display = "block";
        div_togger1.style.display = "block";
        var table = document.getElementById("Line_Table1");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("invoice_no").value = td[1].innerHTML;
        document.getElementById("return_wo_no").value = td[7].innerHTML;
        document.getElementById("Text1").value = td[0].innerHTML;
        
    }
}

////生成料号的按钮，点击事件
//var Button_GetItem = document.getElementsByClassName("Button_GetItem");
//var div_togger1 = document.getElementById("div_togger1");
//for (var i = 0; i < Button_GetItem.length; i++) {
//    Button_GetItem[i].index = i;
//    Button_GetItem[i].onclick = function () {
//        zindex.style.display = "block";
//        div_togger1.style.display = "block";
//    }
//}