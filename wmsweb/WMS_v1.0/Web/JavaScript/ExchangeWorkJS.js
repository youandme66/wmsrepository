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

        var table = document.getElementById("ExchangeLine_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");

        document.getElementById("item_name").value = td[1].innerHTML;
        document.getElementById("in_subinventory").value = td[5].innerHTML;
        //document.getElementById("in_frame_key").value = td[6].innerHTML;
        document.getElementById("out_subinventory").value = td[3].innerHTML;
        //document.getElementById("out_frame_key").value = td[4].innerHTML;
        document.getElementById("exchanged_qty").value = td[8].innerHTML;
        document.getElementById("update_man").value = td[12].innerHTML;
        document.getElementById("exchange_line_id_debit").value = td[0].innerHTML;
        
    }
}