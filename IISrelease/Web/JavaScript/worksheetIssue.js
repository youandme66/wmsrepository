/*关闭弹出框*/
var zindex = document.getElementById("zindex");
var btn_close = document.getElementsByClassName("btn_close");

for (var i = 0; i < btn_close.length; i++) {
    btn_close[i].onclick = function () {
        zindex.style.display = "none";
        div_togger1.style.display = "none";
        
    }
}


var btn_update = document.getElementsByClassName("btn_update");
var div_togger1 = document.getElementById("div_togger1");
for (var i = 0; i < btn_update.length; i++) {
    btn_update[i].index = i;
    btn_update[i].onclick = function () {

        zindex.style.display = "block";
        div_togger1.style.display = "block";

        var table = document.getElementById("Line_Table");
        /*获取行的第一个单元格 */
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        /*获取选择行的所有列*/
        var td = tr.getElementsByTagName("td");

        document.getElementById("simulate").value = td[1].innerHTML;//备料单号
        document.getElementById("item_id1").value = td[2].innerHTML;//料号ID
        document.getElementById("item_name").value = td[3].innerHTML;//料号
    }
}
