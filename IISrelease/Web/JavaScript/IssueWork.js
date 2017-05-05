var zindex = document.getElementById("zindex");

var btn_close = document.getElementsByClassName("btn_close");
for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
    }
}


var btn_debit = document.getElementsByClassName("btn_debit");
var div_togger1 = document.getElementById("div_togger1");
for (var i = 0; i < btn_debit.length; i++) {
    btn_debit[i].index = i;
    btn_debit[i].onclick = function () {

        zindex.style.display = "block";
        div_togger1.style.display = "block";

        var table = document.getElementById("IssueLine_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
       
        document.getElementById("issue_line_id_debit").value = td[0].innerHTML;
        document.getElementById("issued_qty").value = td[9].innerHTML;
        document.getElementById("wo_no").value = td[2].innerHTML;
        document.getElementById("flag_debit").value = td[10].innerHTML;
        document.getElementById("issued_sub").value = td[5].innerHTML;
        //document.getElementById("frame").value = td[6].innerHTML;
        document.getElementById("item_name").value = td[4].innerHTML;   
    }
}