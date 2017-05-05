var list_foot_header_btn = document.getElementsByClassName("list-foot-header-btn");
var list_foot_bottom = document.getElementsByClassName("list-foot-bottom");
for (var i = 0; i < list_foot_header_btn.length; i++) {
    list_foot_header_btn[i].index = i;
    list_foot_header_btn[i].onclick = function () {
        //初始化状态
        for (var i = 0; i < list_foot_header_btn.length; i++) {
            list_foot_bottom[i].style.display = "none";
        }

        list_foot_bottom[this.index].style.display = "block";
    }
}


//var Lists = new Array("001", "002", "003", "004");
$(document).ready(function () {
    //for (var i = 0; i < Lists.length; i++) {
    //    $("#section-top-right-select").append("<option>" + Lists[i] + "</option>");
    //}
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "WareHouse.ashx",
        dataType: "json",
        data: {
            warehouse: $("#Chooses").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#Chooses").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});