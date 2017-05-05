function change_gethand() {
    $.ajax({
        timeout: 3000,
        async: false,
        type: "POST",
        url: "aboutWorkSheetIn.ashx",
        dataType: "json",
        data: {
            wo_no: $("#wo_no").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    $("#item_name").val(data[i].Name);
                }
                if (i == 1) {
                    $("#target_qty").val(data[i].Name);
                }
                if (i == 2) {
                    $("#onhand").val(data[i].Name);
                }
            }
        }
    });
}

$(document).ready(function () {
    $.ajax({
        async: false,
        type: "POST",
        url: "WareHouse.ashx",
        dataType: "json",
        data: {
            warehouse: $("#issued_sub_key").val(),
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#issued_sub_key").append("<option>" + data[i].Name + "</option>");
            }
        }
    });
});
$("#frame_key").attr("style", "height:21px");
//二级联动，选择库别，改变区域选项
$("#issued_sub_key").change(function () {

    if ($("#issued_sub_key").find("option:selected").text() != "选择库别") {

        $.ajax({
            async: false,
            type: "POST",
            url: "subinventoryandframe.ashx",
            dataType: "json",
            data: {
                warehouse_name: $("#issued_sub_key option:selected").text(),
            },
            success: function (data) {
                $("#frame_key").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#frame_key").append("<option>" + data[i].Name + "</option>");
                }
            }
        });

    }
});
