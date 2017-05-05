var btn_update = document.getElementsByClassName("btn_choose");
for (var i = 0; i < btn_update.length; i++) {
    btn_update[i].index = i;
    btn_update[i].onclick = function () {

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("ITEM_name").value = td[2].innerHTML;
        document.getElementById("datecode").value = td[4].innerHTML;
        document.getElementById("Rec_Num").value = td[3].innerHTML;
        document.getElementById("Rec_qty").value = td[5].innerHTML;
    }
}

//$(document).ready(function () {
//    $.ajax({
//        timeout: 3000,
//        async: false,
//        type: "POST",
//        url: "workorder.ashx",
//        dataType: "json",
//        data: {
//            warehouse: $("#ITEM_name2").val(),
//        },
//        success: function (data) {
//            for (var i = 0; i < data.length; i++) {
//                $("#ITEM_name2").append("<option>" + data[i].Name + "</option>");
//            }
//        }
//    });
//});
