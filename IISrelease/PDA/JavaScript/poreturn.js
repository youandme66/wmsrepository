
$(document).ready(function () {
    //绑定库别
    $.ajax({
        async: false,
        timeout: 3000,
        type: "POST",
        url: "getSubinventory.ashx",
        dataType: "json",
        data: {
            name: $("#select1").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#select1").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});
//二级联动，选择库别，改变区域选项
document.getElementById("select1").onchange = function () {
    if ($("#select1").find("option:selected").text() != "选择库别") {
       
        getRegion();
    }
}
//通过ajax获取区域列表
function getRegion() {
    $.ajax({
        async: false,
        timeout: 3000,
        type: "POST",
        url: "getRegion_by_Sub.ashx",
        dataType: "json",
        data: {
            name: $("#select1").find("option:selected").text(),
        },
        success: function (data) {
           // $("#select2").html("<option>选择区域</option>");//清空下拉框
            $("#select2").html("");
            for (var i = 0; i < data.length; i++) {
                $("#select2").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
}

var zindex = document.getElementById("zindex");
var btn_return = document.getElementsByClassName("btn_update");
var div_togger1 = document.getElementById("div_togger1");
for (var i = 0; i < btn_return.length; i++) {
    btn_return[i].index = i;
    btn_return[i].onclick = function () {
        zindex.style.display = "block";
        div_togger1.style.display = "block";
        var table = document.getElementById("GridView1");
        var tr = table.getElementsByTagName("tr")[this.index + 1];
        var td = tr.getElementsByTagName("td");
        document.getElementById("receipt_no_id").value = td[2].innerHTML;
    }
}
var btn_cancel = document.getElementById("btn_close");
btn_cancel.onclick = function () {
    document.getElementById("receipt_no_id").value = "";
    document.getElementById("return_num_id").value = "";
    zindex.style.display = "none";
    div_togger1.style.display = "none";
}

