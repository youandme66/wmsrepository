var zindex = document.getElementById("zindex");

var btn_shipping = document.getElementsByClassName("btn_update");
var div_togger1 = document.getElementById("div_togger1");
for (var i = 0; i < btn_shipping.length; i++) {
    btn_shipping[i].index = i;
    btn_shipping[i].onclick = function () {
        zindex.style.display = "block";
        div_togger1.style.display = "block";
        var table = document.getElementById("GridView1");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("wo_no_id").value = td[3].innerHTML;
    }
}
var btn_cancel = document.getElementById("btn_close");
btn_cancel.onclick = function () {
    document.getElementById("wo_no_id").value = "";
    document.getElementById("shipping_num_id").value = "";
    zindex.style.display = "none";
    div_togger1.style.display = "none";
    div_togger2.style.display = "none";
    div_togger3.style.display = "none";
}
