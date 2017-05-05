
//var zindex = document.getElementById("zindex");

//var btn_close = document.getElementsByClassName("btn_close");
//for (var i = 0; i < btn_close.length; i++) {
//    btn_close[i].onclick = function () {
//        zindex.style.display = "none";
//        div_togger1.style.display = "none";
        
//    }
//}

//var btn_insert = document.getElementById("btn_insert");
//var div_togger1 = document.getElementById("div_togger1");
//btn_insert.onclick = function () {
//    zindex.style.display = "block";
//    div_togger1.style.display = "block";
//}
$(document).ready(function () {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "workorder.ashx",
        dataType: "json",
        data: {
            warehouse: $("#item_name").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#item_name").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});