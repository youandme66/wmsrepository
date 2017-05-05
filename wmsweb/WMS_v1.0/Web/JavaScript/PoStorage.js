var btn_update = document.getElementsByClassName("btn_choose");
for (var i = 0; i < btn_update.length; i++) {
    btn_update[i].index = i;
    btn_update[i].onclick = function () {

        var table = document.getElementById("Line_Table");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("ITEM_name").value = td[2].innerHTML;
        document.getElementById("datecode").value = td[5].innerHTML;
        document.getElementById("Rec_Num").value = td[4].innerHTML;
        document.getElementById("Rec_qty").value = parseInt(td[7].innerHTML) - parseInt(td[9].innerHTML);
        document.getElementById("Accepted_qty").value = td[7].innerHTML;
        document.getElementById("Deliver_qty").value = td[9].innerHTML;
    }
}

//$(document).ready(function () {
//    $.ajax({
//        async: false,
//        type: "POST",
//        url: "WareHouse.ashx",
//        dataType: "json",
//        data: {
//            warehouse: $("#issued_sub_key").val(),
//        },
//        success: function (data) {
//            for (var i = 0; i < data.length; i++) {
//                $("#issued_sub_key").append("<option>" + data[i].Name + "</option>");
//            }
//        }
//    });
//});
//$("#frame_key").attr("style", "height:21px");
////二级联动，选择库别，改变区域选项
//$("#issued_sub_key").change(function () {

//    if ($("#issued_sub_key").find("option:selected").text() != "选择库别") {

//        $.ajax({
//            async: false,
//            type: "POST",
//            url: "subinventoryandframe.ashx",
//            dataType: "json",
//            data: {
//                warehouse_name: $("#issued_sub_key option:selected").text(),
//            },
//            success: function (data) {
//                $("#frame_key").empty();
//                for (var i = 0; i < data.length; i++) {
//                    $("#frame_key").append("<option>" + data[i].Name + "</option>");
//                }
//            }
//        });

//    }
//});
