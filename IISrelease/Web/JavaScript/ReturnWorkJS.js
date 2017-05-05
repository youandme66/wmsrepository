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

        var table = document.getElementById("ReturnLine_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
     
        document.getElementById("return_line_id_debit").value = td[0].innerHTML;
        document.getElementById("return_qty").value = td[7].innerHTML;
        document.getElementById("flag_debit").value = td[8].innerHTML;
        document.getElementById("return_sub_name").value = td[5].innerHTML;
        //document.getElementById("frame_key").value = td[6].innerHTML;
        document.getElementById("item_name").value = td[4].innerHTML;
        document.getElementById("return_wo_no").value = td[2].innerHTML;
       
    }
}




